using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class HealthBarSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Healthbar> AllHealthbars = new List<Healthbar>();

        private void Awake()
        {
            Channels.OnPlayerSpawned += OnPlayerSpawned;
        }

        private void OnEnable()
        {
            Channels.OnPlayerSpawned -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned(GameObject spawnedObject, int playerNumber)
        {
            foreach (Healthbar healthbar in AllHealthbars)
            {
                if (playerNumber != healthbar.PlayerIndex)
                    continue;

                healthbar.gameObject.SetActive(true);
            }
        }
    }
}