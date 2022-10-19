using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardUIListItem : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text playerNumberTextField;
        [SerializeField]
        private TMP_Text scoreTextField;

        public void UpdateScoreEntry(LeaderboardScoreEntry leaderboardScoreEntry)
        {
            playerNumberTextField.SetText($"{leaderboardScoreEntry.PlayerNumber + 1}");
            scoreTextField.SetText($"{leaderboardScoreEntry.Score}");
        }
    }
}
