using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pickups;
using EventSystem;

public class PickupZoneSpawnManager: MonoBehaviour
{
    TimeTracker timeTrack;
    public Vector3 center;
    private int currentSpawnedCount;

    [SerializeField]
    private GameObject[] pickups;
    [SerializeField]
    private Vector3 spawnAreaSize;
    [SerializeField]
    [Range(1, 20)]
    private float minSpawnInterval;
    [SerializeField]
    [Range(1, 20)]
    private float maxSpawnInterval;
    [SerializeField]
    private int maximumSpawnCount;

    private void Awake()
    {
        center = this.gameObject.transform.position;

        timeTrack = new TimeTracker(SetRandomSpawnInterval());
        SetSpawnCount(0);

       // Channels.OnPickupDestroyed += AdjustSpawnedCoun;
    }
    private void OnDisable()
    {
       // Channels.OnPickupDestroyed -= AdjustSpawnedCoun;
    }

    public void AdjustSpawnedCount()
    {
        SetSpawnCount(currentSpawnedCount - 1);
    }

    // Update is called once per frame
    void Update()
    {

        if (!timeTrack.TimerComplete())
        {
            return;
        }

        Spawn();
    }

    private void Spawn()
    {
        int randomIndex = Random.Range(0, pickups.Length);

        Vector3 randomSpawnPosition = center + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));

        if (!IsSpawnCount())
        {
            return;
        }
        for(int i = 0; i < 1000; i++)
        {
            if (!CanSpawnAtPosition(randomSpawnPosition))
                continue;

            randomSpawnPosition = center + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));
        }
        var instantiatedObject = Instantiate(pickups[randomIndex], randomSpawnPosition, Quaternion.identity);
        instantiatedObject.transform.parent = gameObject.transform;

        SetSpawnCount(currentSpawnedCount + 1);
        timeTrack.ResetTimeTracker(SetRandomSpawnInterval());
    }

    /// <summary>
    /// Set a spawn time interval at random between a max and min time intervals given from the Serialize fields
    /// </summary>
    /// <returns></returns>
    private float SetRandomSpawnInterval()
    {
        float randSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        //Debug.Log("Next spawn in: " + randSpawnInterval);
        return randSpawnInterval;
      
    }

    private bool CanSpawnAtPosition(Vector3 newPosition)
    {
        // Check box checks if the pickup has collided with anything //walls
        if (Physics.CheckBox(newPosition, new Vector3(2.0f, 2.0f, 2.0f), Quaternion.identity))
        {
            return false;
        }

        // Overlap Box to check if the collision is with anotherPickup
        Collider[] hitColliders = Physics.OverlapBox(newPosition, new Vector3(7.0f, 7.0f, 7.0f), Quaternion.identity);
        for (int i = 0; i < hitColliders.Length; i++)
        {

            if (hitColliders[i].GetComponentInParent<Pickup>() != null)
            {
               
                return false;
            }
         
        }
               return true;
    }

    private bool IsSpawnCount()
    {
        if (currentSpawnedCount >= maximumSpawnCount)
        {
            return false;
        }
        return true;
    }

    private void SetSpawnCount(int num)
    {
        if (num < 0)
            return;
        currentSpawnedCount = num;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

}
