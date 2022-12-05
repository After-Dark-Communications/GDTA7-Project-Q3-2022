using EventSystem;
using Managers;
using ShipParts.Ship;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ShipSelection.ShipBuilders
{
    public class ShipBuildManager : Manager
    {
        #region Singleton
        public static ShipBuildManager Instance;

        void Awake()
        {
            if (Instance != null)
            {
                Instance._shipBuilders.Clear();
                Instance._firstPlayerReady = null;
                Destroy(gameObject);
                return;
            }

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
            Channels.OnShipCompleted += OnShipCompleted;
            Channels.OnPlayerJoined += OnPlayerJoined;
        }
        #endregion

        private List<ShipBuilder> _shipBuilders = new List<ShipBuilder>();

        private int _amountOfPlayersJoined = 0;
        private ShipBuilder _firstPlayerReady;

        public override void Start() { }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Channels.OnManagerInitialized?.Invoke(this);
        }

        private void OnShipCompleted(ShipBuilder shipBuilder)
        {
            int index = GetIndexOfShipBuilderInList(shipBuilder);

            if (index != -1)
                _shipBuilders.RemoveAt(index);

            _shipBuilders.Add(shipBuilder);

#if UNITY_EDITOR
            if (_shipBuilders.Count == 1 && _amountOfPlayersJoined == 1)
            {
                PlayerJoinManager FakeJoiner = FindObjectOfType<PlayerJoinManager>();
                InputDevice fakeDevice = InputSystem.AddDevice("Gamepad");
                PlayerInput fakeInput = PlayerInput.Instantiate(FakeJoiner.PrefabPlayerShipSelection, pairWithDevice: fakeDevice);
                FakeJoiner?.OnPlayerJoin(fakeInput, true);
                SetPlayerOne();
                Channels.OnEveryPlayerReady?.Invoke(_amountOfPlayersJoined);
                return;
            }

#endif
            if (_shipBuilders.Count < 2)
            { return; }
            if (_shipBuilders.Count == _amountOfPlayersJoined)
            {
                SetPlayerOne();
                Channels.OnEveryPlayerReady?.Invoke(_amountOfPlayersJoined);
            }
            void SetPlayerOne()
            {
                if (_firstPlayerReady != null)
                { return; }
                _firstPlayerReady = _shipBuilders[0];
            }
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Channels.OnShipCompleted -= OnShipCompleted;
            Channels.OnPlayerJoined -= OnPlayerJoined;
        }

        private int GetIndexOfShipBuilderInList(ShipBuilder shipBuilder)
        {
            return _shipBuilders.FindIndex(sb => sb.PlayerNumber == shipBuilder.PlayerNumber);
        }

        private void OnPlayerJoined(int playerNumber, InputDevice inputDevice)
        {
            _amountOfPlayersJoined = playerNumber + 1;
        }

        public ShipBuilder GetShipBuilder(int playerIndex)
        {
            foreach (ShipBuilder shipBuilder in _shipBuilders)
            {
                if (shipBuilder.PlayerNumber == playerIndex)
                {
                    return shipBuilder;
                }
            }
            return null;
        }

        public List<ShipBuilder> ShipBuilders => _shipBuilders;

        public int AmountOfPlayersJoined => _amountOfPlayersJoined;

        public ShipBuilder FirstPlayerReady => _firstPlayerReady;
    }
}