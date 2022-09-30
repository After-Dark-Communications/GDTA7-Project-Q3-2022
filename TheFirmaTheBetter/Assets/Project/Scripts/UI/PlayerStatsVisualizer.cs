using EventSystem;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PlayerStatsVisualizer : MonoBehaviour
    {
        [SerializeField]
        private List<ShipStatBar> healthBars = new(4);

        [SerializeField]
        private GameObject statsPanelPrefab;

        private void OnEnable()
        {
            Channels.OnPlayerSpawned += InitializePlayerStats;
        }

        private void OnDisable()
        {
            Channels.OnPlayerSpawned -= InitializePlayerStats;
        }

        private void Awake()
        {
            // Hide all healthbars
            foreach (ShipStatBar healthBar in healthBars)
            {
                healthBar.gameObject.SetActive(false);
            }
        }

        public void InitializePlayerStats(GameObject player, int playerIndex)
        {
            // Get the healthbar
            List<ShipStatBar> playerStatBars = new()
            {
                healthBars[playerIndex - 1]
            };

            // Instantiate the stats panel
            FloatingStatsPanel statsPanel = Instantiate(statsPanelPrefab, transform).GetComponent<FloatingStatsPanel>();
            playerStatBars.AddRange(statsPanel.StatBars);
            statsPanel.ObjectToFollow = player;

            // Assign the player index to all stat bars
            foreach (ShipStatBar statBar in playerStatBars)
            {
                statBar.PlayerIndex = playerIndex;
                statBar.gameObject.SetActive(true);
            }
        }
    }
}