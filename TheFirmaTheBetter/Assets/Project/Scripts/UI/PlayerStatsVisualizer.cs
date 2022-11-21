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


        private void Awake()
        {
            Channels.OnPlayerSpawned += InitializePlayerStats;
            Channels.OnPlayerDespawned += HidePlayerStats;
            Channels.OnPlayerRespawned += ShowPlayerStats;
            Channels.KingOfTheHill.OnKingOfTheHillPlayerRespawn += ShowPlayerStats;

            foreach (FloatingStatsPanel statPanel in statPanels)
            {
                statPanel.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            Channels.OnPlayerSpawned -= InitializePlayerStats;
            Channels.OnPlayerDespawned -= HidePlayerStats;
            Channels.OnPlayerRespawned -= ShowPlayerStats;
        }

        public void InitializePlayerStats(GameObject player, int playerIndex)
        {
            // Instantiate the stats panel
            FloatingStatsPanel statPanel = statPanels[playerIndex];
            statPanel.ObjectToFollow = player;
            statPanel.gameObject.SetActive(true);

            statPanel.SpecialCooldownShower.PlayerIndex = playerIndex;
            statPanel.CrownShower.PlayerIndex = playerIndex;

            // Assign the player index to all stat bars
            foreach (ShipStatBar statBar in statPanel.StatBars)
            {
                statBar.PlayerIndex = playerIndex;
                statBar.gameObject.SetActive(true);
            }

            Channels.OnPlayerBarsLoaded(player.GetComponent<ShipBuilder>());
        }

        private void HidePlayerStats(int playerIndex)
        {
            FloatingStatsPanel statPanel = statPanels[playerIndex];
            statPanel.gameObject.SetActive(false);
        }

        private void ShowPlayerStats(int playerIndex)
        {
            FloatingStatsPanel statPanel = statPanels[playerIndex];
            statPanel.gameObject.SetActive(true);
        }

        private void ShowPlayerStats(ShipBuilder builder)
        {
            ShowPlayerStats(builder.PlayerNumber);
        }
    }
}