using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pickups;
using EventSystem;

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

    [SerializeField]
    private int maximumSpawnCount;

    private int currentSpawnedCount;


    private void Awake()
    {
        center = this.gameObject.transform.position;

        timeTrack = new TimeTracker(spawnInterval);
        SetSpawnCount(0);

        Channels.OnPickupDestroyed += AdjustSpawnedCoun;
    }
    private void OnDisable()
    {
        Channels.OnPickupDestroyed -= AdjustSpawnedCoun;
    }

    private void AdjustSpawnedCoun()
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
        timeTrack.TimeReset();

    }

    private void Spawn()
    {
        int randomIndex = Random.Range(0, pickups.Length);

        Vector3 randomSpawnPosition = center + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));

        if (!IsSpawnCount())
        {
            return;
        }
        while (!CanSpawnAtPosition(randomSpawnPosition))
        {
            randomSpawnPosition = center + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));
        }
        var instantiatedObject = Instantiate(pickups[randomIndex], randomSpawnPosition, Quaternion.identity);
        instantiatedObject.transform.parent = gameObject.transform;

        SetSpawnCount(currentSpawnedCount + 1);
    }


    private bool CanSpawnAtPosition(Vector3 newPosition)
    {
        if (Physics.CheckBox(newPosition, new Vector3(2.0f, 2.0f, 2.0f), Quaternion.identity))
        {

            return false;
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
