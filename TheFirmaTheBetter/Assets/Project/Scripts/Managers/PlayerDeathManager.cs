using EventSystem;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
using System;
using UnityEngine;
using EZCameraShake;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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

        [Header("Camera shake settings")]
        [SerializeField]
        private float camShakeMagnitude;
        [SerializeField]
        private float camShakeRoughness;


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
            Channels.OnRoundStarted -= ReviveShips;
        }

        private void OnPlayerDeath(ShipBuilder shipBuilderThatDied, int killerIndex)
        {
            PlayerResult playerResult = shipBuilderThatDied.GetComponent<PlayerResult>();
            KillShip(shipBuilderThatDied, playerResult);

            if (playersAlive <= 1)
            {
                ShipBuilder winner = shipBuilderThatDied;

                if (killerIndex >= 0 && killerIndex < totalNumberOfPlayers)
                {
                    winner = ShipBuildManager.Instance.ShipBuilders[killerIndex];
                }
                else
                {
                    foreach (ShipBuilder ship in ShipBuildManager.Instance.ShipBuilders)
                    {
                        if (ship.gameObject.activeInHierarchy)
                        {
                            winner = ship;
                        }
                    }
                }

                if (winner.PlayerNumber != shipBuilderThatDied.PlayerNumber)
                {
                    playerResult = winner.GetComponent<PlayerResult>();
                    KillShip(winner, playerResult);
                }

                Channels.OnRoundOver?.Invoke(roundManager.CurrentRoundIndex, winner.PlayerNumber);
            }
            else
            {
                Channels.Announcer.OnPlayPlayerEliminated?.Invoke();
                CameraShaker.Instance.ShakeOnce(camShakeMagnitude, camShakeRoughness, 1f, 1f);
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
            Channels.OnPlayerDespawned?.Invoke(ship.PlayerNumber);
        }

        private void RemoveShipFromScene(ShipBuilder ship)
        {
            ship.transform.parent = null;
            DontDestroyOnLoad(ship);
        }

        private void ReviveShips(int roundIndex, int numberofRounds)
        {
            foreach (ShipBuilder ship in ShipBuildManager.Instance.ShipBuilders)
            {
                ship.gameObject.SetActive(true);
            }
            playersAlive = totalNumberOfPlayers;
        }
    }
}