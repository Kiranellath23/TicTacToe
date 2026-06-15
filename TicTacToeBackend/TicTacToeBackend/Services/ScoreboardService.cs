using TicTacToeBackend.Models;

namespace TicTacToeBackend.Services
{

     public interface IScoreboardService
    {
        Scoreboard GetScoreboard();
        void RecordWin(char player);
        void RecordDraw();
        void ResetScoreboard();
    }

    public class ScoreboardService : IScoreboardService
    {
        private Scoreboard _scoreboard = new();

        public Scoreboard GetScoreboard() => _scoreboard;

        public void RecordWin(char player)
        {
            if (player == 'X')
                _scoreboard.XWins++;
            else if (player == 'O')
                _scoreboard.OWins++;
        }

        public void RecordDraw()
        {
            _scoreboard.Draws++;
        }

        public void ResetScoreboard()
        {
            _scoreboard = new();
        }
    }
}



