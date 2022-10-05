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
        private ResultsManager resultsManager;

        private void Awake()
        {
            Channels.OnPlayerBecomesDeath += OnPlayerDeath;
            Channels.OnManagerInitialized += OnManagerInitialized;

            resultsManager = ResultsManager.Instance;
        }

        private void OnManagerInitialized(Manager manager)
        {
            if (manager is not ShipBuildManager)
                return;

            shipBuildManager = manager as ShipBuildManager;
            playersAlive = shipBuildManager.AmountOfPlayersJoined;

        }

        private void OnPlayerDeath(ShipBuilder shipBuilderThatDied)
        {
            shipBuilderThatDied.gameObject.SetActive(false);
            shipBuilderThatDied.transform.parent = null;
            DontDestroyOnLoad(shipBuilderThatDied);
            playersAlive--;
            resultsManager.AddResult(shipBuilderThatDied);

            if (playersAlive <= 1)
            {
                foreach (ShipBuilder ship in shipBuildManager.ShipBuilders)
                {
                    if (ship.gameObject.activeInHierarchy)
                    {
                        resultsManager.AddResult(ship);
                        shipBuilderThatDied.transform.parent = null;
                        DontDestroyOnLoad(shipBuilderThatDied);
                    }
                }
                SceneSwitchManager.SwitchToNextScene();
            }
        }
    }
}