using EventSystem;
using ShipParts.Ship;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DEBUG_Inputs : MonoBehaviour
{
#if UNITY_EDITOR
    private const int _debugPlayerIndex = 99;

    [SerializeField]
    private ShipSpawner spawner;

    private ShipBuilder[] _playerShips = new ShipBuilder[4];

    private void OnEnable()
    {
        Channels.OnControllerShemeShowing += OnControllerSchemeShowing;
    }

    private void OnDisable()
    {
        Channels.OnControllerShemeShowing -= OnControllerSchemeShowing;
    }

    private void Update()
    {
        CallMethodForPlayer(Keyboard.current.kKey, DEBUG_KillPlayer);
        CallMethodForPlayer(Keyboard.current.iKey, DEBUG_ReloadPlayer);
    }

    private void OnControllerSchemeShowing()
    {
        int length = spawner.transform.childCount < _playerShips.Length ? spawner.transform.childCount : _playerShips.Length;
        for (int i = 0; i < length; i++)
        {
            ShipBuilder child = spawner.transform.GetChild(i).GetComponentInChildren<ShipBuilder>();
            if (child == null)
            { continue; }
            _playerShips[i] = child;
        }
    }

    /// <summary>
    /// Calls the passed method if the given <paramref name="SpecialKey"/> is held and 1,2,3 or 4 is pressed
    /// </summary>
    /// <param name="SpecialKey">The key that has to be held for the method to be called</param>
    /// <param name="methodToCall">method with int parameter that is used for the player's index.(zero-indexed)</param>
    private void CallMethodForPlayer(UnityEngine.InputSystem.Controls.KeyControl SpecialKey, Action<int> methodToCall)
    {
        if (SpecialKey.isPressed)
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                methodToCall(0);
            }
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                methodToCall(1);
            }
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                methodToCall(2);
            }
            else if (Keyboard.current.digit4Key.wasPressedThisFrame)
            {
                methodToCall(3);
            }
        }
    }

    private void DEBUG_KillPlayer(int index)
    {
        if (_playerShips[index] == null)
        { return; }
        Debug.Log("[DEBUG]Kill player " + _playerShips[index].PlayerNumber);
        Channels.OnPlayerBecomesDeath.Invoke(_playerShips[index], _debugPlayerIndex);
    }

    private void DEBUG_ReloadPlayer(int index)
    {
        if (_playerShips[index] == null)
        { return; }
        Debug.Log("[DEBUG]Reload player " + _playerShips[index].PlayerNumber);
        Channels.OnRefillEnergy.Invoke(index, int.MaxValue);
    }
#endif
}