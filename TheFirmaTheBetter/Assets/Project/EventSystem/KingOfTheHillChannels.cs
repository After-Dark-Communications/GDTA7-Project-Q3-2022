using ShipParts.Ship;
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
        /// <param name="scoreToAdd">Amount of points to give</param>
        public delegate void KingOfTheHillScore(int playerNumber, int scoreToAdd);
        /// <summary>
        /// Call when a player enters the scoring zone
        /// </summary>
        /// <param name="playerNumber">Player entering the score zone</param>
        public delegate void KingOfTheHillEnterScoreZone(int playerNumber);
        /// <summary>
        /// Call when a player leaves the score zone
        /// </summary>
        /// <param name="playerNumber">Player leaving the score zone</param>
        public delegate void KingOfTheHillLeaveScoreZone(int playerNumber);
        /// <summary>
        /// Call when starting a KOTH game
        /// </summary>
        /// <param name="playerNumbers">The player numbers in game</param>
        public delegate void KingOfTheHillStart(List<int> playerNumbers);
        /// <summary>
        /// Call when ending a KOTH game
        /// </summary>
        public delegate void KingOfTheHillEnd();

        public delegate void KingOfTheHillPlayerRespawn(ShipBuilder shipsThatNeedsToRespawn);

        public KingOfTheHillScore OnKingOfTheHillScore;
        public KingOfTheHillStart OnKingOfTheHillStart;
        public KingOfTheHillEnd OnKingOfTheHillEnd;
        public KingOfTheHillEnterScoreZone OnKingOfTheHillEnterZone;
        public KingOfTheHillLeaveScoreZone OnKingOfTheHillLeaveZone;
        public KingOfTheHillPlayerRespawn OnKingOfTheHillPlayerRespawn;

    }
}

