using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pickups;


    public class PickupZoneSpawnManager: MonoBehaviour
    {
        //private float instantiateTime;
        public Vector3 center;
        [SerializeField]
        private GameObject[] pickups;
        [SerializeField]
        private Vector3 spawnAreaSize;

        TimeTracker timeTrack;

        [SerializeField]
        private float spawnInterval;

        private void Awake()
        {
            center = this.gameObject.transform.position;

            timeTrack = new TimeTracker(spawnInterval);
        }

        // Update is called once per frame
        void Update()
        {

            if (!timeTrack.TimerComplete())
            {
                return;
            }

            Spawn();
            timeTrack.TimeReset();

        }
        private void Spawn()
        {
            int randomIndex = Random.Range(0, pickups.Length);
            
            Vector3 randomSpawnPosition = center + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));

            while (!CanSpawnPickup(randomSpawnPosition))
        {
            randomSpawnPosition = center + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));
        }
            var instantiatedObject = Instantiate(pickups[randomIndex], randomSpawnPosition, Quaternion.identity);
            instantiatedObject.transform.parent = gameObject.transform;
        }

        private bool CanSpawnPickup(Vector3 newPosition)
        {
            if (Physics.CheckBox(newPosition, new Vector3(2.0f, 2.0f, 2.0f), Quaternion.identity))
            {
            
                return false;
            }   

            return true;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, spawnAreaSize);
        }
    
    }
