using EventSystem;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class KOTHPlayerRespawnManager : MonoBehaviour
    {
        [SerializeField]
        List<RespawnIndicator> respawnIndicators = new List<RespawnIndicator>();
        private void OnEnable()
        {
            Channels.OnPlayerBecomesDeath += KillShip;
            Channels.KingOfTheHill.OnKingOfTheHillPlayerRespawn += DisableRespawnIndicator;
            Channels.KingOfTheHill.OnKingOfTheHillEnd += SendShipToResults;
        }

        private void SendShipToResults()
        {
            foreach (ShipBuilder ship in ShipBuildManager.Instance.ShipBuilders)
            {
                ship.transform.parent = null;
                DontDestroyOnLoad(ship);
            }
        }

        private void DisableRespawnIndicator(ShipBuilder shipsThatNeedsToRespawn)
        {
            int playerNumberDeadShip = shipsThatNeedsToRespawn.PlayerNumber;
            respawnIndicators[playerNumberDeadShip].gameObject.SetActive(false);
        }

        private void KillShip(ShipBuilder shipBuilderThatNeedsDying, int playerIndexOfKiller)
        {
            int playerNumberDeadShip = shipBuilderThatNeedsDying.PlayerNumber;
            shipBuilderThatNeedsDying.gameObject.SetActive(false);

            respawnIndicators[playerNumberDeadShip].gameObject.SetActive(true);
            respawnIndicators[playerNumberDeadShip].SetOwnerOfIndicator(shipBuilderThatNeedsDying);

            Channels.KingOfTheHill.OnKingOfTheHillPlayerDied?.Invoke(playerNumberDeadShip);
            Channels.OnPlayerDespawned?.Invoke(playerNumberDeadShip);
        }

        private void OnDisable()
        {
            Channels.OnPlayerBecomesDeath -= KillShip;
            Channels.KingOfTheHill.OnKingOfTheHillPlayerRespawn -= DisableRespawnIndicator;
            Channels.KingOfTheHill.OnKingOfTheHillEnd -= SendShipToResults;
        }


    }
}
