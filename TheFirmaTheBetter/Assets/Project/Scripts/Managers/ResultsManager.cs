using EventSystem;
using ShipSelection.ShipBuilders;
using System.Collections.Generic;
using UnityEngine;
namespace Managers
{
    public class ResultsManager : Manager
    {
        public static ResultsManager Instance;

        [SerializeField]
        private List<PlayerResult> results;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

        }

        private void OnEnable()
        {
            SetupResults(ShipBuildManager.Instance.AmountOfPlayersJoined);
            Channels.OnPlayerSpawned += AddPlayerResult;
        }
        private void OnDisable()
        {
            Channels.OnPlayerSpawned -= AddPlayerResult;
        }

        public void SetupResults(int numberOfPlayers)
        {
            results = new List<PlayerResult>();
        }

        private void AddPlayerResult(GameObject spawnedShipBuilderObject, int playerNumber)
        {
            PlayerResult playerResult = spawnedShipBuilderObject.GetComponent<PlayerResult>();
            results.Add(playerResult);
        }

        public PlayerResult[] Results
        {
            get
            {
                results.Sort();
                return results.ToArray();
            }
        }
    }
}