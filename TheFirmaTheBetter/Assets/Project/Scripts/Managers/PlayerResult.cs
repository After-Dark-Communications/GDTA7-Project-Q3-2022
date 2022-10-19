using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerResult : MonoBehaviour, IComparable<PlayerResult>
    {
        private int playerIndex;
        private float timeSurvived;
        private int playersKilled;
        private float distanceTravelled;
        private List<int> roundsWonIndices;

        private void Awake()
        {
            Setup();
        }

        private void OnEnable()
        {
            Channels.OnPlayerBecomesDeath += OnPlayerKilled;
            Channels.OnRoundOver += OnRoundWon;
        }

        private void OnDisable()
        {
            Channels.OnPlayerBecomesDeath -= OnPlayerKilled;
            Channels.OnRoundOver -= OnRoundWon;
        }

        private void Setup()
        {
            playerIndex = GetComponent<ShipBuilder>().PlayerNumber;
            playersKilled = 0;
            timeSurvived = 0;
            distanceTravelled = 0;
            roundsWonIndices = new List<int>();
        }

        private void OnPlayerKilled(ShipBuilder shipThatDied, int killerIndex)
        {
            if (killerIndex == playerIndex)
            {
                playersKilled++;
            }
        }

        private void OnRoundWon(int roundIndex, int winnerIndex)
        {
            if (winnerIndex == playerIndex)
            {
                roundsWonIndices.Add(roundIndex);
            }
        }

        public int CompareTo(PlayerResult other)
        {
            if (PlayersKilled != other.PlayersKilled)
            {
                return PlayersKilled.CompareTo(other.PlayersKilled) * -1;
            }

            if (RoundsWon != other.RoundsWon)
            {
                return RoundsWon.CompareTo(other.RoundsWon) * -1;
            }

            return LastWonRoundIndex.CompareTo(other.LastWonRoundIndex) * -1;

        }

        public float TimeSurvived
        {
            get { return timeSurvived; }
            set { timeSurvived = value; }
        }

        public int PlayersKilled
        {
            get { return playersKilled; }
        }

        public float DistanceTravelled
        {
            get { return distanceTravelled; }
            set { distanceTravelled = value; }
        }

        public int RoundsWon
        {
            get { return roundsWonIndices.Count; }
        }

        public int LastWonRoundIndex
        {
            get
            {
                if (roundsWonIndices.Count > 0)
                {
                    return roundsWonIndices[roundsWonIndices.Count - 1];
                }
                return -1;
            }
        }
    }
}