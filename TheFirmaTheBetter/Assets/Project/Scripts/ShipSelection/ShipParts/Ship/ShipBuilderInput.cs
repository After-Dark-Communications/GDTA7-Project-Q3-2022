using EventSystem;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipBuilderInput
{
    private InputDevice playerDevice;

    private int playerNumber;

    public ShipBuilderInput(InputDevice playerDevice, int playerNumber)
    {
        this.playerDevice = playerDevice;
        this.playerNumber = playerNumber;

        SubScribeToEvents();
    }

    public void UnSubscribeToEvents()
    {
        Channels.OnPlayerSpawned -= DisableInput;
        Channels.OnRoundOver -= DisableInput;

        Channels.OnCountdownDone -= EnableInput;
        Channels.OnGameOver -= EnableInput;
    }

    private void DisableInput(GameObject spawnedShipBuilderObject, int playerNumber)
    {
        DisableInput();
    }

    private void SubScribeToEvents()
    {
        Channels.OnPlayerSpawned += DisableInput;
        Channels.OnRoundOver += DisableInput;

        Channels.OnCountdownDone += EnableInput;
        Channels.OnGameOver += EnableInput;
    }

    private void DisableInput(int roundIndex, int winnerIndex)
    {
        DisableInput();
    }

    private void DisableInput()
    {
        InputSystem.DisableDevice(playerDevice);
    }

    private void EnableInput()
    {
        InputSystem.EnableDevice(playerDevice);
    }

    private void DisableInput(int playerNumber)
    {
       DisableInput();
    }

    private void EnableInput(int playerNumber)
    {
        if (this.playerNumber != playerNumber)
            return;

        EnableInput();
    }
}