using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard
{
    public class LeaderboardScoreEntry
    {
        private int playerNumber;
        private int score;

        public int PlayerNumber { get => playerNumber; set => playerNumber = value; }
        public int Score { get => score; set => score = value; }
    }
}
