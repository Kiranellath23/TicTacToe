using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToeBackend.DTOs;
using TicTacToeBackend.Models;
using TicTacToeBackend.Services;

namespace TicTacToeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreboardController : ControllerBase
    {
         private readonly IScoreboardService _scoreboardService;

        public ScoreboardController(IScoreboardService scoreboardService)
        {
            _scoreboardService = scoreboardService;
        }

        [HttpGet]
        public ActionResult<Response<Scoreboard>> GetScoreboard()
        {
            var scoreboard = _scoreboardService.GetScoreboard();
            return Ok(Response<Scoreboard>.Ok(scoreboard));
        }

        [HttpPost("reset")]
        public ActionResult<Response<Scoreboard>> ResetScoreboard()
        {
            _scoreboardService.ResetScoreboard();
            var scoreboard = _scoreboardService.GetScoreboard();
            return Ok(Response<Scoreboard>.Ok(scoreboard, "Scoreboard reset"));
        }
    }
}
