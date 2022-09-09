using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Steef is right :)
 * -Luc
 */
public class InputHandler : MonoBehaviour
{
    public PlayerNumber ThisPlayerNumber = PlayerNumber.PLAYER_ONE;

    //[SerializeField]
    //private Part SpecialPart,ChasisPart,EnginePart,WeaponPart;

    public static event EventHandler OnPlayerInputMove;
    public static event EventHandler OnPlayerInputAim;
    public static event EventHandler OnPlayerInputShoot;
    public static event EventHandler OnPlayerInputPause;
    public static event EventHandler OnPlayerInputSpecial;
    public static event EventHandler OnPlayerInputMoveUp;
    public static event EventHandler OnPlayerInputMoveDown;

    private void OnMoveAction(InputAction.CallbackContext ctx)
    {
        OnPlayerInputMove.Invoke(this, new InputDirectionalArgs(ThisPlayerNumber, ctx.ReadValue<Vector2>()));
    }

    private void OnAimAction(InputAction.CallbackContext ctx)
    {
        OnPlayerInputAim.Invoke(this, new InputDirectionalArgs(ThisPlayerNumber, ctx.ReadValue<Vector2>()));
    }

    private void OnShootAction(InputAction.CallbackContext ctx)
    {
        OnPlayerInputShoot.Invoke(this, new InputButtonArgs(ThisPlayerNumber, ctx.ReadValueAsButton()));
    }
    private void OnPauseAction(InputAction.CallbackContext ctx)
    {
        OnPlayerInputPause.Invoke(this, new InputButtonArgs(ThisPlayerNumber, ctx.ReadValueAsButton()));
    }
    private void OnSpecialAction(InputAction.CallbackContext ctx)
    {
        OnPlayerInputSpecial.Invoke(this, new InputButtonArgs(ThisPlayerNumber, ctx.ReadValueAsButton()));
    }
    private void OnMoveUpAction(InputAction.CallbackContext ctx)
    {
        OnPlayerInputMoveUp.Invoke(this, new InputButtonArgs(ThisPlayerNumber, ctx.ReadValueAsButton()));
    }
    private void OnMoveDownAction(InputAction.CallbackContext ctx)
    {
        OnPlayerInputMoveDown.Invoke(this, new InputButtonArgs(ThisPlayerNumber, ctx.ReadValueAsButton()));
    }
}

public abstract class InputEventArgs : EventArgs
{
    public PlayerNumber playerNumber { get; protected set; }
}

public class InputDirectionalArgs : InputEventArgs
{
    public readonly Vector2 Value;
    public InputDirectionalArgs(PlayerNumber number, Vector2 Value)
    {
        playerNumber = number;
        this.Value = Value;
    }
}

public class InputButtonArgs : InputEventArgs
{
    public readonly bool Value;
    public InputButtonArgs(PlayerNumber number, bool Value)
    {
        playerNumber = number;
        this.Value = Value;
    }
}

