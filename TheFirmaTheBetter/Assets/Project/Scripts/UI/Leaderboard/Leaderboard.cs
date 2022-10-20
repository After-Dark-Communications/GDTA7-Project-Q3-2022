using EventSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField]
        private List<LeaderBoardPanel> panels = new List<LeaderBoardPanel>();

        private void OnEnable()
        {
            Channels.KingOfTheHill.OnKingOfTheHillScore += UpdateScore;
            Channels.OnPlayerSpawned += InitializeLeaderBoard;
        }

        private void OnDisable()
        {
            Channels.KingOfTheHill.OnKingOfTheHillScore -= UpdateScore;
            Channels.OnPlayerSpawned -= InitializeLeaderBoard;
        }

        public void InitializeLeaderBoard(GameObject player, int playerindex)
        {
            LeaderBoardPanel playerLeadboardPanel = panels[playerindex];
            playerLeadboardPanel.SetPlayerIndex(playerindex);
            playerLeadboardPanel.gameObject.SetActive(true);
        }

        public void UpdateScore(int playerNumber, int score)
        {
            LeaderBoardPanel leaderBoardPanel = panels.FirstOrDefault(p => p.PlayerIndex == playerNumber);
            leaderBoardPanel.UpdatePoints(score);
            UpdatePanel();
        }

        public void UpdatePanel()
        {
            List<LeaderBoardPanel> lbp = new List<LeaderBoardPanel>();
            lbp = GetHighestScore().ToList();

            for (int i = 0; i < panels.Count; i++)
            {
                int orderedPlayerIndex = lbp[i].PlayerIndex;
                int orderedScore = lbp[i].PlayerPoints;

                panels[i].UpdateStats(orderedPlayerIndex, orderedScore);
            }
        }
        public IEnumerable<LeaderBoardPanel> GetHighestScore()
        {
            return panels.OrderByDescending(x => x.PlayerPoints);

        }
    }
}
