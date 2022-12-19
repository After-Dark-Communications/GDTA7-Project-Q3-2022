using EventSystem;
using ShipSelection.ShipBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class PlayerReadyManager : MonoBehaviour
    {
        private const int _readyTime = 1;
        private Dictionary<int, float> _readyingShipPlayers = new Dictionary<int, float>();
        private List<int> _readyingPlayerNumbers = new List<int>();
        private void OnEnable()
        {
            Channels.Input.OnShipCompletedInputStarted += OnStartReadyTimer;
            Channels.Input.OnShipCompletedInputEnded += OnEndReadyTimer;
        }


        private void OnDisable()
        {
            Channels.Input.OnShipCompletedInputStarted -= OnStartReadyTimer;
            Channels.Input.OnShipCompletedInputEnded -= OnEndReadyTimer;
        }

        private void Update()
        {
            _readyingPlayerNumbers = _readyingShipPlayers.Keys.ToList();
            foreach (int player in _readyingPlayerNumbers)
            {
                if (ShipBuildManager.Instance.GetShipBuilder(player) == null)
                {
                    _readyingShipPlayers[player] += Time.deltaTime;
                    Debug.Log($"player {player}: {_readyingShipPlayers[player]}");
                    if (_readyingShipPlayers[player] >= _readyTime)
                    {
                        Channels.Input.OnShipCompletedInput?.Invoke(player);
                    }
                }
            }
        }

        private void OnStartReadyTimer(int playerNumber)
        {
            if (ShipBuildManager.Instance.GetShipBuilder(playerNumber) == null)
            {
                _readyingShipPlayers.Add(playerNumber, 0f);
            }
        }
        private void OnEndReadyTimer(int playerNumber)
        {
            if (ShipBuildManager.Instance.GetShipBuilder(playerNumber) == null)
            {
                _readyingShipPlayers.Remove(playerNumber);
            }
        }
    }
}
