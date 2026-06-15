using TicTacToeBackend.Models;

namespace TicTacToeBackend.DTOs
{



    public class GameStateResponse
    {
        public Guid GameId { get; set; }
        public char[] Board { get; set; }
        public char CurrentPlayer { get; set; }
        public GameMode Mode { get; set; }
        public GameStatus Status { get; set; }
        public char? Winner { get; set; }
        public int[] WinningCells { get; set; }
        public List<MoveRecord> MoveHistory { get; set; }
        public Scoreboard Scoreboard { get; set; }
    }
}
