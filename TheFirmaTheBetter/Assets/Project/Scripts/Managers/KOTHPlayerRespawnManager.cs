using EventSystem;
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
            Channels.OnPlayerRespawned += OnPlayerRespawned;
        }

        private void OnDisable()
        {
            
        }

        private void OnPlayerRespawned(GameObject respawnedShipBuilderObject, int playerNumber)
        {
            
        }
    }
}
