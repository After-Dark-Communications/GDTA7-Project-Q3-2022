using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardPanelListManager : MonoBehaviour
    {
        [SerializeField]
        private List<LeaderboardUIListItem> leaderboardUIListItems = new List<LeaderboardUIListItem>();

        private List<LeaderboardScoreEntry> leaderboardScoreEntries = new List<LeaderboardScoreEntry>();

        private void Awake()
        {
            Channels.OnPlayerSpawned += OnPlayerSpawned;
            Channels.KingOfTheHill.OnKingOfTheHillScore += OnPlayerScore;
        }

        private void OnPlayerScore(int playerNumber, int scoreToAdd)
        {
            int index = leaderboardScoreEntries.FindIndex(se => se.PlayerNumber == playerNumber);

            leaderboardScoreEntries[index].Score += scoreToAdd;

            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            List<LeaderboardScoreEntry> leaderboardScoreEntries = this.leaderboardScoreEntries.OrderByDescending(se => se.Score).ToList();

            for (int i = 0; i < leaderboardScoreEntries.Count; i++)
            {
                leaderboardUIListItems[i].UpdateScoreEntry(leaderboardScoreEntries[i]);
            }
        }

        private void OnPlayerSpawned(GameObject spawnedShipBuilderObject, int playerNumber)
        {
            ShipBuilder spawnedBuilder = spawnedShipBuilderObject.GetComponent<ShipBuilder>();

            LeaderboardScoreEntry scoreEntry = new LeaderboardScoreEntry()
            {
                PlayerNumber = spawnedBuilder.PlayerNumber,
                Score = 0
            };

            int index = leaderboardScoreEntries.FindIndex(se => se.PlayerNumber == scoreEntry.PlayerNumber);

            if (index >= 0)
                return;

            leaderboardScoreEntries.Add(scoreEntry);
            leaderboardUIListItems[leaderboardScoreEntries.Count - 1].gameObject.SetActive(true);
            leaderboardUIListItems[leaderboardScoreEntries.Count - 1].UpdateScoreEntry(scoreEntry);
        }

        private void OnDestroy()
        {
            Channels.OnPlayerSpawned -= OnPlayerSpawned;
            Channels.KingOfTheHill.OnKingOfTheHillScore -= OnPlayerScore;
        }
    }
}
