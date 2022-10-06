using EventSystem;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
using UnityEngine;

namespace Managers
{
    public class PlayerDeathManager : MonoBehaviour
    {
        private int playersAlive;

        private ShipBuildManager shipBuildManager;

        [SerializeField]
        private BattleTimer battleTimer;

        private void Awake()
        {
            Channels.OnPlayerBecomesDeath += OnPlayerDeath;
            Channels.OnManagerInitialized += OnManagerInitialized;
        }

        private void OnManagerInitialized(Manager manager)
        {
            if (manager is not ShipBuildManager)
                return;

            shipBuildManager = manager as ShipBuildManager;
            playersAlive = shipBuildManager.AmountOfPlayersJoined;
            ResultsManager.Instance.SetupResults(playersAlive);
        }

        private void OnPlayerDeath(ShipBuilder shipBuilderThatDied, int killerIndex)
        {
            PlayerStatistics playerStatistics = shipBuilderThatDied.GetComponent<PlayerStatistics>();
            RemoveShipFromScene(shipBuilderThatDied);
            SetKillStats(playerStatistics);
            ResultsManager.Instance.AddResult(playerStatistics);
            
            if (playersAlive <= 1)
            {
                foreach (ShipBuilder ship in shipBuildManager.ShipBuilders)
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