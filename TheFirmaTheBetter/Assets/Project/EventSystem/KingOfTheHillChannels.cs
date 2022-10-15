using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public class KingOfTheHillChannel
    {
        /// <summary>
        /// Gives a player score
        /// </summary>
        /// <param name="playerNumber">The player number to give points</param>
        /// <param name="score">Amount of points to give</param>
        public delegate void KingOfTheHillScore(int playerNumber, int score);
        public delegate void KingOfTheHillEnterScoreZone(int playerNumber);
        public delegate void KingOfTheHillLeaveScoreZone(int playerNumber);
        /// <summary>
        /// Call when starting a KOTH game
        /// </summary>
        /// <param name="playerNumbers">The player numbers in game</param>
        public delegate void KingOfTheHillStart(List<int> playerNumbers);

        public KingOfTheHillScore OnKingOfTheHillScore;
        public KingOfTheHillStart OnKingOfTheHillStart;
        public KingOfTheHillEnterScoreZone OnKingOfTheHillEnterZone;
        public KingOfTheHillLeaveScoreZone OnKingOfTheHillLeaveZone;
    }
}

