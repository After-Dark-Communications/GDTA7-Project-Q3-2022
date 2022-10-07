using EventSystem;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
using UnityEngine;

namespace Managers
{
    public class PlayerDeathManager : MonoBehaviour
    {
        private int playersAlive;

        [SerializeField]
        private BattleTimer battleTimer;

        private void OnEnable()
        {
            Channels.OnPlayerBecomesDeath += OnPlayerDeath;

            playersAlive = ShipBuildManager.Instance.AmountOfPlayersJoined;
        }

        private void OnDisable()
        {
            Channels.OnPlayerBecomesDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath(ShipBuilder shipBuilderThatDied, int killerIndex)
        {
            PlayerStatistics playerStatistics = shipBuilderThatDied.GetComponent<PlayerStatistics>();
            RemoveShipFromScene(shipBuilderThatDied);
            SetKillStats(playerStatistics);
            ResultsManager.Instance.AddResult(playerStatistics);
            
            if (playersAlive <= 1)
            {
                foreach (ShipBuilder ship in ShipBuildManager.Instance.ShipBuilders)
                {
                    playerStatistics = ship.GetComponent<PlayerStatistics>();
                    if (playerStatistics.IsAlive)
                    {
                        RemoveShipFromScene(ship);
                        SetKillStats(playerStatistics);
                        ResultsManager.Instance.AddResult(playerStatistics);
                    }
                }

                SceneSwitchManager.SwitchToNextScene();
            }
        }

        private void RemoveShipFromScene(ShipBuilder ship)
        {
            ship.gameObject.SetActive(false);
            ship.transform.parent = null;
            DontDestroyOnLoad(ship);
        }

        private void SetKillStats(PlayerStatistics playerStatistics)
        {
            playerStatistics.TimeSurvived = battleTimer.TimeSinceStart;
            playerStatistics.IsAlive = false;
            playersAlive--;
        }
    }
}