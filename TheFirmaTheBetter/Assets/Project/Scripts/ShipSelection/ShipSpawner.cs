using EventSystem;
using ShipParts;
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
        private List<GameObject> playerShipObjects;

        [SerializeField]
        private GameObject joinprefab;

        [SerializeField]
        [Tooltip("Required only for King of the Hill")]
        private GameObject respawnIndicatorPrefab;

        private void OnEnable()
        {
            Channels.OnRoundStarted += OnRoundStared;
            Channels.KingOfTheHill.OnKingOfTheHillPlayerRespawn += RespawnShip;
        }

        private void OnDisable()
        {
            Channels.OnRoundStarted -= OnRoundStared;
            Channels.KingOfTheHill.OnKingOfTheHillPlayerRespawn -= RespawnShip;
        }

        private void OnRoundStared(int roundIndex, int numberOfRounds)
        {
            if (roundIndex == 1)
            {
                SpawnShips();
                DestroyUnusedShips();
            }
            else if (roundIndex > 1)
            {
                RespawnShips();
            }
        }

        private void SpawnShips()
        {
            foreach (ShipBuilder shipBuilder in ShipBuildManager.Instance.ShipBuilders)
            {
                int playerIndex = shipBuilder.PlayerNumber;

                Transform spawnPointTransform = playerSpawnPoints[playerIndex];
                GameObject playerShipObject = playerShipObjects[playerIndex];
                if (spawnPointTransform == null || playerShipObject == null)
                    continue;

                playerShipObject.SetActive(true);

                SpawnShip(playerShipObject, spawnPointTransform);

                shipBuilder.transform.parent = playerShipObject.transform;
                shipBuilder.transform.localPosition = Vector3.zero;
                shipBuilder.transform.localRotation = Quaternion.Euler(0, -90, 0);

                PlayerInputManager.instance.playerPrefab = joinprefab;
                PlayerInput inp = PlayerInputManager.instance.JoinPlayer(playerIndex, -1, null, shipBuilder.PlayerDevice);
                inp.transform.parent = playerShipObject.transform;

                Channels.OnPlayerSpawned?.Invoke(shipBuilder.gameObject, playerIndex);
                Channels.OnChangeFireMode?.Invoke(true, shipBuilder.PlayerNumber);
            }
        }

        private void RespawnShips()
        {
            foreach (ShipBuilder shipBuilder in ShipBuildManager.Instance.ShipBuilders)
            {
                RespawnShip(shipBuilder);
            }
        }

        public void RespawnShip(ShipBuilder shipBuilder)
        {
            int playerIndex = shipBuilder.PlayerNumber;

            Transform spawnPointTransform = playerSpawnPoints[playerIndex];
            GameObject playerShipObject = playerShipObjects[playerIndex];
            if (spawnPointTransform == null || playerShipObject == null)
                return;

            shipBuilder.gameObject.SetActive(true);
            SpawnShip(playerShipObject, spawnPointTransform);
            Channels.OnPlayerRespawned?.Invoke(playerIndex);
        
        }

        private void SpawnShip(GameObject playerShip, Transform spawnPoint)
        {
            playerShip.transform.position = spawnPoint.position;
            playerShip.transform.rotation = spawnPoint.rotation;
            playerShip.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        private void DestroyUnusedShips()
        {
            foreach (GameObject playerShip in playerShipObjects)
            {
                if (!playerShip.GetComponentInChildren<ShipBuilder>())
                {
                    Destroy(playerShip);
                }
            }
        }

    }
}