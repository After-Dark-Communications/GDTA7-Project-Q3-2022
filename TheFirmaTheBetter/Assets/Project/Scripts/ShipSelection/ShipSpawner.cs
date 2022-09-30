using EventSystem;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            foreach (ShipBuilder shipBuilder in shipBuildManager.ShipBuilders)
            {
                int playerIndex = shipBuilder.PlayerNumber;

                Transform spawnPointTransform = playerSpawnPoints[playerIndex];

                spawnPointTransform.gameObject.SetActive(true);

                shipBuilder.transform.position = spawnPointTransform.position;
                shipBuilder.transform.rotation = Quaternion.Euler(0, spawnPointTransform.eulerAngles.y - 90, 0);//TODO: set rotation relative to parent, not to world
                shipBuilder.transform.parent = spawnPointTransform;

                PlayerInputManager.instance.playerPrefab = joinprefab;
                PlayerInput inp = PlayerInputManager.instance.JoinPlayer(playerIndex, -1, null, shipBuilder.PlayerDevice);
                inp.transform.parent = spawnPointTransform;
                Channels.OnPlayerSpawned.Invoke(shipBuilder.gameObject, shipBuilder.PlayerNumber);
            }
        }
    }
}