using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeBackend.Models;
using TicTacToeBackend.Services;

namespace TicTacToeBackend.Tests
{
    public class GameServiceTests
    {
  
        private IGameService GetGameService()
        {
            var computerAI = new ComputerAIService();
            return new GameService(computerAI);
        }
        [Fact]
        public void CreateGame_ShouldInitializeCorrectly()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            Assert.Equal(GameStatus.InProgress, game.Status);
            Assert.Equal('X', game.CurrentPlayer);
            Assert.Empty(game.MoveHistory);
        }
        [Fact]
        public void MakeMove_ValidMove_ShouldPlacePiece()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            game = service.MakeMove(game.Id, 0);

            Assert.Equal('X', game.Board[0]);
            Assert.Equal('O', game.CurrentPlayer);
            Assert.Single(game.MoveHistory);
        }
        [Fact]
        public void MakeMove_OccupiedCell_ShouldThrow()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            service.MakeMove(game.Id, 0);

            Assert.Throws<InvalidOperationException>(() =>
                service.MakeMove(game.Id, 0));
        }
        [Fact]
        public void CheckWin_RowWin_ShouldDetect()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            service.MakeMove(game.Id, 0); // X at 0
            service.MakeMove(game.Id, 3); // O at 3
            service.MakeMove(game.Id, 1); // X at 1
            service.MakeMove(game.Id, 4); // O at 4
            game = service.MakeMove(game.Id, 2); // X at 2 -> X wins row 0

            Assert.Equal(GameStatus.Won, game.Status);
            Assert.Equal('X', game.Winner);
            Assert.Equal(new[] { 0, 1, 2 }, game.WinningCells);
        }
        [Fact]
        public void CheckWin_ColumnWin_ShouldDetect()
        {
            // Similar pattern to row win
            // Move sequence: X at 0, O at 1, X at 3, O at 2, X at 6 -> X wins column 0
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            service.MakeMove(game.Id, 0);  // X at 0
            service.MakeMove(game.Id, 1);  // O at 1
            service.MakeMove(game.Id, 3);  // X at 3
            service.MakeMove(game.Id, 2);  // O at 2
            game = service.MakeMove(game.Id, 6);  // X at 6 -> X wins column 0 

            Assert.Equal(GameStatus.Won, game.Status);
            Assert.Equal('X', game.Winner);
            Assert.Equal(new[] { 0, 3, 6 }, game.WinningCells);
        }
        [Fact]
        public void CheckWin_DiagonalWin_ShouldDetect()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            service.MakeMove(game.Id, 0); // X at 0
            service.MakeMove(game.Id, 1); // O at 1
            service.MakeMove(game.Id, 4); // X at 4
            service.MakeMove(game.Id, 2); // O at 2
            game = service.MakeMove(game.Id, 8); // X at 8 -> X wins diagonal

            Assert.Equal(GameStatus.Won, game.Status);
            Assert.Equal('X', game.Winner);
            Assert.Equal(new[] { 0, 4, 8 }, game.WinningCells);
        }
        [Fact]
        public void CheckDraw_FullBoard_ShouldDetect()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            // Board layout:
            // X O X
            // X O O
            // O X X
            var moves = new[] { 0, 1, 2, 4, 3, 5, 7, 6, 8 };

            // Make first 8 moves
            foreach (var move in moves[..8])
                game = service.MakeMove(game.Id, move);

            // Make final move (fills board)
            game = service.MakeMove(game.Id, moves[8]);

            Assert.Equal(GameStatus.Draw, game.Status);
        }
        [Fact]
        public void ResetGame_ShouldClearState()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            service.MakeMove(game.Id, 0);
            service.MakeMove(game.Id, 1);
            game = service.ResetGame(game.Id);

            Assert.All(game.Board, cell => Assert.Equal(' ', cell));
            Assert.Equal('X', game.CurrentPlayer);
            Assert.Empty(game.MoveHistory);
            Assert.Equal(GameStatus.InProgress, game.Status);
        }
        [Fact]
        public void UndoLastMove_TwoPlayerMode_ShouldRemoveOneMove()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            service.MakeMove(game.Id, 0); // X at 0
            game = service.MakeMove(game.Id, 1); // O at 1

            game = service.UndoLastMove(game.Id);

            Assert.Equal(' ', game.Board[1]); // O's move undone
            Assert.Equal('O', game.CurrentPlayer); // Back to O's turn
            Assert.Equal(1, game.MoveHistory.Count); // One move remains
        }
        [Fact]
        public void UndoLastMove_ComputerMode_ShouldRemoveTwoMoves()
        {
            // Arrange
            var service = GetGameService();
            var game = service.CreateGame(GameMode.Computer);

            // Act
            game = service.MakeMove(game.Id, 0); // Player X moves, computer responds

            game = service.UndoLastMove(game.Id);

            // Assert
            Assert.Equal(' ', game.Board[0]);

            // Verify board is empty
            Assert.All(game.Board, cell => Assert.Equal(' ', cell));

            Assert.Equal('X', game.CurrentPlayer);
            Assert.Empty(game.MoveHistory);
        }
        [Fact]
        public void UndoLastMove_CompletedGame_ShouldThrow()
        {
            var service = GetGameService();
            var game = service.CreateGame(GameMode.TwoPlayer);

            // Play until X wins
            service.MakeMove(game.Id, 0);
            service.MakeMove(game.Id, 3);
            service.MakeMove(game.Id, 1);
            service.MakeMove(game.Id, 4);
            game = service.MakeMove(game.Id, 2);

            // Try to undo
            Assert.Throws<InvalidOperationException>(() =>
                service.UndoLastMove(game.Id));
        }
    }
}
