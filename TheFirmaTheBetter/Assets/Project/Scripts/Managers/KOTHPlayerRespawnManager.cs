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
        private void OnEnable()
        {
            Channels.OnPlayerBecomesDeath += KillShip;
        }

        private void KillShip(ShipBuilder shipBuilderThatNeedsDying, int playerIndexOfKiller)
        {
            Channels.KingOfTheHill.OnKingOfTheHillPlayerRespawn.Invoke(shipBuilderThatNeedsDying);
        }

        private void OnDisable()
        {
            Channels.OnPlayerBecomesDeath -= KillShip;
        }


    }
}
