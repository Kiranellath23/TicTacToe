using TicTacToeBackend.DTOs;
using TicTacToeBackend.Models;

namespace TicTacToeBackend.DTOs;

public class CreateGameRequest
{
      public GameMode Mode { get; set; } = GameMode.TwoPlayer;
}
