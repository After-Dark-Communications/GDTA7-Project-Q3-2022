using EventSystem;
using System;
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
        Channels.OnControllerShemeShowing -= DisableInput;
        Channels.OnControllerShemeHidden -= EnableInput;
    }

    private void SubScribeToEvents()
    {
        Channels.OnControllerShemeShowing += DisableInput;
        Channels.OnControllerShemeHidden += EnableInput;
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
        if (this.playerNumber != playerNumber)
            return;

       DisableInput();
    }

    private void EnableInput(int playerNumber)
    {
        if (this.playerNumber != playerNumber)
            return;

        EnableInput();
    }
}