using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToeBackend.DTOs;
using TicTacToeBackend.Models;
using TicTacToeBackend.Services;

namespace TicTacToeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
      private readonly IGameService _gameService;
        private readonly IScoreboardService _scoreboardService;

        public GamesController(IGameService gameService, IScoreboardService scoreboardService)
        {
            _gameService = gameService;
            _scoreboardService = scoreboardService;
        }

        [HttpPost]
        public ActionResult<Response<GameStateResponse>> CreateGame([FromBody] CreateGameRequest request)
        {
            try
            {
                var game = _gameService.CreateGame(request.Mode);
                var response = MapToResponse(game);
                return Ok(Response<GameStateResponse>.Ok(response, "Game created"));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<GameStateResponse>.Error(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Response<GameStateResponse>> GetGame(Guid id)
        {
            try
            {
                var game = _gameService.GetGame(id);
                var response = MapToResponse(game);
                return Ok(Response<GameStateResponse>.Ok(response));
            }
            catch (Exception ex)
            {
                return NotFound(Response<GameStateResponse>.Error(ex.Message));
            }
        }

        [HttpPost("{id}/moves")]
        public ActionResult<Response<GameStateResponse>> MakeMove(Guid id, [FromBody] MakeMoveRequest request)
        {
            try
            {
                int cellIndex = request.CellIndex;
                if (request.Row.HasValue && request.Column.HasValue)
                    cellIndex = request.Row.Value * 3 + request.Column.Value;

                var game = _gameService.MakeMove(id, cellIndex);

                // Record in scoreboard if game is completed
                if (game.Status == GameStatus.Won)
                    _scoreboardService.RecordWin(game.Winner.Value);
                else if (game.Status == GameStatus.Draw)
                    _scoreboardService.RecordDraw();

                var response = MapToResponse(game);
                return Ok(Response<GameStateResponse>.Ok(response, "Move made"));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<GameStateResponse>.Error(ex.Message));
            }
        }

        [HttpPost("{id}/undo")]
        public ActionResult<Response<GameStateResponse>> UndoMove(Guid id)
        {
            try
            {
                var game = _gameService.UndoLastMove(id);
                var response = MapToResponse(game);
                return Ok(Response<GameStateResponse>.Ok(response, "Move undone"));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<GameStateResponse>.Error(ex.Message));
            }
        }

        [HttpPost("{id}/reset")]
        public ActionResult<Response<GameStateResponse>> ResetGame(Guid id)
        {
            try
            {
                var game = _gameService.ResetGame(id);
                var response = MapToResponse(game);
                return Ok(Response<GameStateResponse>.Ok(response, "Game reset"));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<GameStateResponse>.Error(ex.Message));
            }
        }

        private GameStateResponse MapToResponse(GameSession game)
        {
            return new GameStateResponse
            {
                GameId = game.Id,
                Board = game.Board,
                CurrentPlayer = game.CurrentPlayer,
                Mode = game.Mode,
                Status = game.Status,
                Winner = game.Winner,
                WinningCells = game.WinningCells,
                MoveHistory = game.MoveHistory,
                Scoreboard = null // Fetch separately via ScoreboardController
            };
        }
    


    }
}
