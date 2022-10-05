using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class EnergyBarsSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<FloatingStatsPanel> floatingStatsPanels = new List<FloatingStatsPanel>();

        private void Awake()
        {
            Channels.OnPlayerSpawned += OnPlayerSpawned;
        }

        private void OnPlayerSpawned(GameObject spawnedObject, int playerNumber)
        {
            foreach (FloatingStatsPanel panel in floatingStatsPanels)
            {
                if (panel.EnergyBar.PlayerIndex != playerNumber)
                    continue;

                panel.gameObject.SetActive(true);
                panel.ObjectToFollow = spawnedObject.transform.parent.gameObject;
                return;
            }
        }
    }
}