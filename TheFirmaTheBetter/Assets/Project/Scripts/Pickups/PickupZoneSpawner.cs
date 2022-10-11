using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZoneSpawner : MonoBehaviour
{
    public Vector3 center;
    [SerializeField]
    private GameObject[] pickups;
    [SerializeField]
    private Vector3 spawnAreaSize;

    private void Awake()
    {
        center = this.gameObject.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //For debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomIndex = Random.Range(0, pickups.Length);
            Vector3 randomSpawnPosition = center + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), 0, Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2));

            Instantiate(pickups[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
