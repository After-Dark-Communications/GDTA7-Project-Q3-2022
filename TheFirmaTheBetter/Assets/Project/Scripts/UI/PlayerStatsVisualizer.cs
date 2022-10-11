using EventSystem;
using ShipParts.Ship;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PlayerStatsVisualizer : MonoBehaviour
    {
        [SerializeField]
        private List<FloatingStatsPanel> statPanels;

        private void OnEnable()
        {
            Channels.OnPlayerSpawned += InitializePlayerStats;
            Channels.OnPlayerBecomesDeath += HidePlayerStats;
        }

        private void OnDisable()
        {
            Channels.OnPlayerSpawned -= InitializePlayerStats;
            Channels.OnPlayerBecomesDeath -= HidePlayerStats;
        }

        public void InitializePlayerStats(GameObject player, int playerIndex)
        {
            // Instantiate the stats panel
            FloatingStatsPanel statPanel = statPanels[playerIndex];
            statPanel.ObjectToFollow = player;
            statPanel.gameObject.SetActive(true);

            // Assign the player index to all stat bars
            foreach (ShipStatBar statBar in statPanel.StatBars)
            {
                statBar.PlayerIndex = playerIndex;
                statBar.gameObject.SetActive(true);
            }
        }

        private void HidePlayerStats(ShipBuilder ship, int killerIndex)
        {
            // Get the stats panel
            FloatingStatsPanel statPanel = statPanels[ship.PlayerNumber];
            statPanel.ObjectToFollow = null;
            statPanel.gameObject.SetActive(false);

            // Remove the player index from all stat bars
            foreach (ShipStatBar statBar in statPanel.StatBars)
            {
                statBar.PlayerIndex = -1;
                statBar.gameObject.SetActive(false);
            }
        }
    }
}