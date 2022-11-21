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
        EnableAllInputsHard();
    }

    public void UnSubscribeToEvents()
    {
        Channels.OnPlayerSpawned -= DisableInput;
        Channels.OnRoundOver -= DisableInput;
        Channels.Input.OnShipCompletedInput -= DisableInput;

        Channels.OnEveryPlayerReady -= EnableInputHard;
        Channels.OnCountdownDone -= EnableAllInputsHard;
        Channels.OnGameOver -= EnableAllInputsHard;
        Channels.OnQuitGame -= Quit;
    }

    private void DisableInput(GameObject spawnedShipBuilderObject, int playerNumber)
    {
        DisableInputHard();
    }

    private void SubScribeToEvents()
    {
        Channels.OnPlayerSpawned += DisableInput;
        Channels.OnRoundOver += DisableInput;
        Channels.Input.OnShipCompletedInput += DisableInput;

        Channels.OnEveryPlayerReady += EnableInputHard;
        Channels.OnCountdownDone += EnableAllInputsHard;
        Channels.OnGameOver += EnableAllInputsHard;
        Channels.OnQuitGame += Quit;
    }

    void Quit()
    {
        EnableAllInputsHard();
    }

    private void DisableInput(int roundIndex, int winnerIndex)
    {
        DisableInputHard();
    }

    private void DisableInputHard()
    {
        InputSystem.DisableDevice(playerDevice);
    }
    private void DisableInputHard(int number)
    {
        InputSystem.DisableDevice(playerDevice);
    }

    private void EnableInputHard(int number)
    {
        InputSystem.EnableDevice(playerDevice);
    }

    private void DisableInput(int playerNumber)
    {
        if (this.playerNumber != playerNumber)
            return;

        DisableInputHard();
    }

    private void EnableInput(int playerNumber)
    {
        if (this.playerNumber != playerNumber)
            return;

        EnableAllInputsHard();
    }

    public static void EnableAllInputsHard()
    {
        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            InputSystem.EnableDevice(InputSystem.devices[i]);
        }
    }
}