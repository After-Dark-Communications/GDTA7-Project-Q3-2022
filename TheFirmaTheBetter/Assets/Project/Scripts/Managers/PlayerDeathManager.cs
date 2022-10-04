using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerDeathManager : MonoBehaviour
    {
        private void Awake()
        {
            Channels.OnPlayerBecomesDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath(ShipBuilder shipBuilderThatDied)
        {
            shipBuilderThatDied.gameObject.SetActive(false);
            shipBuilderThatDied.transform.parent = null;
            DontDestroyOnLoad(shipBuilderThatDied);
        }

        private IEnumerator WaitForRestart()
        {
            yield return new WaitForSeconds(1.5f);
            SceneSwitchManager.LoadFirstScene();
        }
    }
}