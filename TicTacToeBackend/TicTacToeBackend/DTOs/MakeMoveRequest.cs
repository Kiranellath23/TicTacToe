namespace TicTacToeBackend.DTOs;

public class MakeMoveRequest
{
    public int CellIndex { get; set; } // 0-8, or
        public int? Row { get; set; }       // alternative format
        public int? Column { get; set; }
}
