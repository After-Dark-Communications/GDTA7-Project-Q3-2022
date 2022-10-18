using EventSystem;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
using System;
using UnityEngine;

namespace Managers
{
    public class PlayerDeathManager : MonoBehaviour
    {
        private int totalNumberOfPlayers;
        private int playersAlive;

        [SerializeField]
        private BattleTimer battleTimer;

        [SerializeField]
        private RoundManager roundManager;

        private void OnEnable()
        {
            Channels.OnPlayerBecomesDeath += OnPlayerDeath;
            Channels.OnRoundStarted += ReviveShips;

            totalNumberOfPlayers = ShipBuildManager.Instance.AmountOfPlayersJoined;
            playersAlive = totalNumberOfPlayers;
        }

        private void OnDisable()
        {
            Channels.OnPlayerBecomesDeath -= OnPlayerDeath;
            Channels.OnRoundStarted += ReviveShips;
        }

        private void OnPlayerDeath(ShipBuilder shipBuilderThatDied, int killerIndex)
        {
            PlayerResult playerResult = shipBuilderThatDied.GetComponent<PlayerResult>();
            KillShip(shipBuilderThatDied, playerResult);

            if (playersAlive <= 1)
            {
                ShipBuilder lastShip = ShipBuildManager.Instance.ShipBuilders[killerIndex];
                playerResult = lastShip.GetComponent<PlayerResult>();
                KillShip(lastShip, playerResult);

                Channels.OnRoundOver?.Invoke(roundManager.CurrentRoundIndex, lastShip.PlayerNumber);
            }
        }


        private void KillShip(ShipBuilder ship, PlayerResult playerResult)
        {
            ship.gameObject.SetActive(false);
            playerResult.TimeSurvived += battleTimer.TimeSinceStart;
            playersAlive--;
            if (roundManager.IsLastRound)
            {
                RemoveShipFromScene(ship);
            }
        }

        private void RemoveShipFromScene(ShipBuilder ship)
        {
            ship.gameObject.SetActive(false);
            ship.transform.parent = null;
            DontDestroyOnLoad(ship);
        }

        private void ReviveShips(int roundIndex)
        {
            foreach (ShipBuilder ship in ShipBuildManager.Instance.ShipBuilders)
            {
                ship.gameObject.SetActive(true);
            }
            playersAlive = totalNumberOfPlayers;
        }
    }
}