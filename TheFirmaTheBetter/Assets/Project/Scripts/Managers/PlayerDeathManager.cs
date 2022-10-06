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
            RemoveShip(shipBuilderThatDied);
            playersAlive--;
            ResultsManager.Instance.AddResult(new EndStatsData(shipBuilderThatDied.PlayerNumber, shipBuilderThatDied.gameObject, battleTimer.TimeSinceStart, 0));
            
            if (playersAlive <= 1)
            {
                foreach (ShipBuilder ship in shipBuildManager.ShipBuilders)
                {
                    if (ship.gameObject.activeInHierarchy)
                    {
                        RemoveShip(ship);
                        ResultsManager.Instance.AddResult(new EndStatsData(shipBuilderThatDied.PlayerNumber, ship.gameObject, battleTimer.TimeSinceStart, 0));
                    }
                }
                SceneSwitchManager.SwitchToNextScene();
            }
        }

        private void RemoveShip(ShipBuilder ship)
        {
            ship.gameObject.SetActive(false);
            ship.transform.parent = null;
            DontDestroyOnLoad(ship);
        }
    }
}