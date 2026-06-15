namespace TicTacToeBackend.Models
{
    // Enums at the top of the file
    public enum GameMode
    {
        TwoPlayer,
        Computer
    }

    public enum GameStatus
    {
        InProgress,
        Won,
        Draw
    }

    public class GameSession
    {

        public Guid Id { get; set; }

        // Board state: 0-8 indices, ' ' for empty, 'X' or 'O' for filled
        public char[] Board { get; set; } = new char[9];

        public char CurrentPlayer { get; set; } = 'X';
        public GameMode Mode { get; set; }
        public GameStatus Status { get; set; } = GameStatus.InProgress;

        public char? Winner { get; set; }
        public int[] WinningCells { get; set; }

        public List<MoveRecord> MoveHistory { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;

        public GameSession()
        {
            for (int i = 0; i < 9; i++)
                Board[i] = ' ';
        }
        public GameSession(GameSession other)
        {
            Id = other.Id;
            Board = (char[])other.Board.Clone();
            CurrentPlayer = other.CurrentPlayer;
            Mode = other.Mode;
            Status = other.Status;
            Winner = other.Winner;
            WinningCells = other.WinningCells?.ToArray();
            MoveHistory = new List<MoveRecord>(other.MoveHistory);
            CreatedAt = other.CreatedAt;
            LastModifiedAt = DateTime.UtcNow;
        }

    }
}
