namespace TicTacToeBackend.Models
{

  public class MoveRecord
  {
    public int MoveNumber { get; set; }
    public char Player { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public int CellIndex { get; set; } // 0-8

    public string Position => $"Row {Row + 1}, Column {Column + 1}";

  }
}
