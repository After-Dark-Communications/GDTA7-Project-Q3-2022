using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZoneSpawner : MonoBehaviour
{
    private float instantiateTime;
    public Vector3 center;
    [SerializeField]
    private GameObject[] pickups;
    [SerializeField]
    private Vector3 spawnAreaSize;

    [SerializeField]
    private float spawnInterval;

    private void Awake()
    {
        center = this.gameObject.transform.position;
        instantiateTime = spawnInterval;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //For debug
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            
        //}

        if(!isSpawnTime())
        {
            return;
        }

        Spawn();
        TimeReset();

    }
    private bool isSpawnTime()
    {
        instantiateTime -= Time.deltaTime;
        if(instantiateTime <=0)
        {
            return true;
        }
        return false;
    }
    private void TimeReset()
    {
        instantiateTime = spawnInterval;
    }
    private void Spawn()
    {
        int randomIndex = Random.Range(0, pickups.Length);
        Vector3 randomSpawnPosition = center + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), 0, Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));

        Instantiate(pickups[randomIndex], randomSpawnPosition, Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
