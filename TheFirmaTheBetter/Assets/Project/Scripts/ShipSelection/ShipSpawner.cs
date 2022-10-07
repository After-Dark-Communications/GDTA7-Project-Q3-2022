using EventSystem;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ShipSelection
{
    public class ShipSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Important is that the order of the list is also the player number order")]
        private List<Transform> playerSpawnPoints;

        [SerializeField]
        private GameObject joinprefab;


        private ShipBuildManager shipBuildManager;

        private void Start()
        {
            SpawnShips();
        }

        private void SpawnShips()
        {
            foreach (ShipBuilder shipBuilder in ShipBuildManager.Instance.ShipBuilders)
            {
                int playerIndex = shipBuilder.PlayerNumber;

                Transform spawnPointTransform = playerSpawnPoints[playerIndex];

                if (spawnPointTransform == null)
                    continue;

                spawnPointTransform.gameObject.SetActive(true);

                shipBuilder.transform.position = spawnPointTransform.position;
                shipBuilder.transform.rotation = Quaternion.Euler(0, spawnPointTransform.eulerAngles.y - 90, 0);//TODO: set rotation relative to parent, not to world
                shipBuilder.transform.parent = spawnPointTransform;

                PlayerInputManager.instance.playerPrefab = joinprefab;
                PlayerInput inp = PlayerInputManager.instance.JoinPlayer(playerIndex, -1, null, shipBuilder.PlayerDevice);
                inp.transform.parent = spawnPointTransform;
                Channels.OnPlayerSpawned?.Invoke(shipBuilder.gameObject, shipBuilder.PlayerNumber);
            }
        }
    }
}