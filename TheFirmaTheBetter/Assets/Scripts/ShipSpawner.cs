using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Important is that the order of the list is also the player number order")]
    private List<Transform> playerSpawnPoints;

    private ShipBuildManager shipBuildManager;

    private void Awake()
    {
        Channels.OnManagerInitialized += OnManagerInitialized;
    }

    private void OnManagerInitialized(Manager manager)
    {
        if (manager is not ShipBuildManager)
            return;

        shipBuildManager = manager as ShipBuildManager;

        SpawnShips();
    }

    private void SpawnShips()
    {
        foreach(ShipBuilder shipBuilder in shipBuildManager.ShipBuilders)
        {
            int playerIndex = shipBuilder.PlayerNumber;

            playerSpawnPoints[playerIndex].gameObject.SetActive(true);

            shipBuilder.transform.position = playerSpawnPoints[playerIndex].position;
            shipBuilder.transform.parent = playerSpawnPoints[playerIndex];
        }
    }
}
