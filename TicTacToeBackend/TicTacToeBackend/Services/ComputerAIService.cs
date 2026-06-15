namespace TicTacToeBackend.Services;


public interface IComputerAIService
{
    int GetComputerMove(char[] board);
}

public class ComputerAIService : IComputerAIService
{
    public int GetComputerMove(char[] board)
    {
        var availableCells = GetAvailableCells(board);

        if (availableCells.Count == 0)
            throw new InvalidOperationException("No available cells");

        // Priority 1: Can O win?
        foreach (var cell in availableCells)
        {
            var testBoard = (char[])board.Clone();
            testBoard[cell] = 'O';
            if (CheckWin(testBoard, 'O'))
            {
                Console.WriteLine($"AI: Winning move at {cell}");
                return cell;
            }
        }

        // Priority 2: Must block X?
        foreach (var cell in availableCells)
        {
            var testBoard = (char[])board.Clone();
            testBoard[cell] = 'X';
            if (CheckWin(testBoard, 'X'))
            {
                Console.WriteLine($"AI: Blocking X at {cell}");
                return cell;
            }
        }

        // Priority 3: Center?
        if (availableCells.Contains(4))
        {
            Console.WriteLine($"AI: Taking center 4");
            return 4;
        }

        // Priority 4: Corner?
        var corners = new[] { 0, 2, 6, 8 };
        var cornerCell = availableCells.FirstOrDefault(c => corners.Contains(c));
        if (cornerCell != -1)
        {
            Console.WriteLine($"AI: Taking corner {cornerCell}");
            return cornerCell;
        }

        // Priority 5: Any available
        Console.WriteLine($"AI: Taking any available {availableCells[0]}");
        return availableCells[0];
    }

    private List<int> GetAvailableCells(char[] board)
    {
        var available = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == ' ')
                available.Add(i);
        }
        Console.WriteLine($"Available cells: [{string.Join(", ", available)}]");
        return available;
    }

    private bool CheckWin(char[] board, char player)
    {
        int[][] patterns = new[]
        {
                new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7, 8 },
                new[] { 0, 3, 6 }, new[] { 1, 4, 7 }, new[] { 2, 5, 8 },
                new[] { 0, 4, 8 }, new[] { 2, 4, 6 }
            };

        return patterns.Any(pattern =>
            board[pattern[0]] == player &&
            board[pattern[1]] == player &&
            board[pattern[2]] == player);
    }
}

