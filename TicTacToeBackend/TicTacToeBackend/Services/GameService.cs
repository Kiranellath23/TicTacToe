using TicTacToeBackend.Models;
using TicTacToeBackend.Services;

namespace TicTacToeBackend.Services;


public interface IGameService
{
    GameSession CreateGame(GameMode mode);
    GameSession GetGame(Guid gameId);
    GameSession MakeMove(Guid gameId, int cellIndex);
    GameSession UndoLastMove(Guid gameId);
    GameSession ResetGame(Guid gameId);
    void DeleteGame(Guid gameId);
}

public class GameService : IGameService
{
    private readonly Dictionary<Guid, GameSession> _games = new();
    private readonly IComputerAIService _computerAI;

    public GameService(IComputerAIService computerAI)
    {
        _computerAI = computerAI;
    }

    public GameSession CreateGame(GameMode mode)
    {
        var game = new GameSession { Id = Guid.NewGuid(), Mode = mode };
        _games[game.Id] = game;
        return game;
    }

    public GameSession GetGame(Guid gameId)
    {
        if (!_games.TryGetValue(gameId, out var game))
            throw new Exception($"Game {gameId} not found");
        return game;
    }

    public GameSession MakeMove(Guid gameId, int cellIndex)
    {
        var game = GetGame(gameId);

        // Validation
        if (game.Status != GameStatus.InProgress)
            throw new InvalidOperationException("Game is already completed");

        if (cellIndex < 0 || cellIndex >= 9)
            throw new ArgumentException("Invalid cell index");

        if (game.Board[cellIndex] != ' ')
            throw new InvalidOperationException("Cell is already occupied");

        // Make the move
        game.Board[cellIndex] = game.CurrentPlayer;
        var (row, col) = IndexToRowCol(cellIndex);

        game.MoveHistory.Add(new MoveRecord
        {
            MoveNumber = game.MoveHistory.Count + 1,
            Player = game.CurrentPlayer,
            Row = row,
            Column = col,
            CellIndex = cellIndex
        });

        // Check for win/draw
        var winningCells = CheckWin(game.Board, game.CurrentPlayer);
        if (winningCells != null)
        {
            game.Status = GameStatus.Won;
            game.Winner = game.CurrentPlayer;
            game.WinningCells = winningCells;
        }
        else if (IsBoardFull(game.Board))
        {
            game.Status = GameStatus.Draw;
        }
        else
        {
            // Switch player
            game.CurrentPlayer = game.CurrentPlayer == 'X' ? 'O' : 'X';

            // If computer mode and it's computer's turn, make computer move
            if (game.Mode == GameMode.Computer && game.CurrentPlayer == 'O')
            {
                var computerMove = _computerAI.GetComputerMove(game.Board);
                return MakeMove(gameId, computerMove);
            }
        }

        game.LastModifiedAt = DateTime.UtcNow;
        return game;
    }

    public GameSession UndoLastMove(Guid gameId)
    {
        var game = GetGame(gameId);

        if (game.MoveHistory.Count == 0)
            throw new InvalidOperationException("No moves to undo");

        // Option A: Disable undo after completion
        if (game.Status != GameStatus.InProgress)
            throw new InvalidOperationException("Cannot undo a completed game");

        int movesToRemove = 1;

        // In computer mode, remove computer move + human move (2 moves)
        if (game.Mode == GameMode.Computer && game.MoveHistory.Count >= 2)
        {
            var lastMove = game.MoveHistory[^1];
            var secondLastMove = game.MoveHistory[^2];

            // Check if last move is computer (O) and second-last is human (X)
            if (lastMove.Player == 'O' && secondLastMove.Player == 'X')
                movesToRemove = 2;
        }

        // Revert the board and move history
        for (int i = 0; i < movesToRemove && game.MoveHistory.Count > 0; i++)
        {
            var moveToRemove = game.MoveHistory[^1];
            game.Board[moveToRemove.CellIndex] = ' ';
            game.MoveHistory.RemoveAt(game.MoveHistory.Count - 1);
        }

        // Reset game status
        game.Status = GameStatus.InProgress;
        game.Winner = null;
        game.WinningCells = null;

        // Determine whose turn it is (based on move count)
        game.CurrentPlayer = game.MoveHistory.Count % 2 == 0 ? 'X' : 'O';

        game.LastModifiedAt = DateTime.UtcNow;
        return game;
    }

    public GameSession ResetGame(Guid gameId)
    {
        var game = GetGame(gameId);

        // Clear board and game state, keep scoreboard unchanged
        for (int i = 0; i < 9; i++)
            game.Board[i] = ' ';

        game.CurrentPlayer = 'X';
        game.Status = GameStatus.InProgress;
        game.Winner = null;
        game.WinningCells = null;
        game.MoveHistory.Clear();
        game.LastModifiedAt = DateTime.UtcNow;

        return game;
    }

    public void DeleteGame(Guid gameId)
    {
        _games.Remove(gameId);
    }

    // ===== PRIVATE HELPERS =====

    private int[] CheckWin(char[] board, char player)
    {
        // Win patterns: rows, columns, diagonals
        int[][] patterns = new[]
        {
                new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7, 8 }, // rows
                new[] { 0, 3, 6 }, new[] { 1, 4, 7 }, new[] { 2, 5, 8 }, // columns
                new[] { 0, 4, 8 }, new[] { 2, 4, 6 }                      // diagonals
            };

        foreach (var pattern in patterns)
        {
            if (board[pattern[0]] == player &&
                board[pattern[1]] == player &&
                board[pattern[2]] == player)
            {
                return pattern;
            }
        }

        return null;
    }

    private bool IsBoardFull(char[] board)
    {
        return board.All(c => c != ' ');
    }

    private (int row, int col) IndexToRowCol(int index)
    {
        return (index / 3, index % 3);
    }
}
