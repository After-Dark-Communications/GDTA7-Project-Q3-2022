using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace MovingObjects
{
    public class EnergyZoneSpawner : MonoBehaviour
    {
        [SerializeField]
        private float spawnInterval;

        private float timeSinceLastSpawned;

        private ObjectMover objectMover;

        private void Start()
        {
            objectMover = GetComponentInChildren<ObjectMover>();
        }

        private void Update()
        {
            timeSinceLastSpawned += Time.deltaTime;

            if (timeSinceLastSpawned < spawnInterval)
                return;

            timeSinceLastSpawned = 0;
            objectMover.StartCorountineMovePoint();
        }
    }
}