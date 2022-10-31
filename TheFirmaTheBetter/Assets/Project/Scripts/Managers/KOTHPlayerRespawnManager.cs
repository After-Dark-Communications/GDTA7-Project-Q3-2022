using EventSystem;
using ShipParts.Ship;
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

        }

        private void OnDisable()
        {
            Channels.OnPlayerBecomesDeath -= KillShip;
        }


    }
}
