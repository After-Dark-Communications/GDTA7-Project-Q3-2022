using Parts;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Util;

/*
 * Steef is right :)
 * -Luc
 */
public class ShipInputHandler : MonoBehaviour
{
    public UnityVector2Event OnPlayerMove = new UnityVector2Event();
    public UnityFloatEvent OnPlayerAim = new UnityFloatEvent();
    public UnityButtonStateEvent OnPlayerShoot = new UnityButtonStateEvent();
    public UnityButtonStateEvent OnPlayerPause = new UnityButtonStateEvent();
    public UnityButtonStateEvent OnPlayerSpecial = new UnityButtonStateEvent();
    public UnityButtonStateEvent OnPlayerMoveUp = new UnityButtonStateEvent();
    public UnityButtonStateEvent OnPlayerMoveDown = new UnityButtonStateEvent();

    private Vector2 _MoveValues;
    private bool _MoveActive;

    private void Start()
    {
        //TODO: change this if the inputhandler knows what parts it has already
        Part[] parts = GetComponentsInChildren<Part>();
        if (parts.Length > 0)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].Setup();
                Debug.Log($"set events for {parts[i].name}");
            }
        }
    }


    private void Update()
    {
        //OnPlayerForwardBackward.Invoke(_MoveValues.y);
        //OnPlayerLeftRight.Invoke(_MoveValues.x);
        OnPlayerMove.Invoke(_MoveValues);
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        _MoveValues = ctx.ReadValue<Vector2>();
        _MoveActive = ctx.started || ctx.performed;
        if (ctx.canceled == true)
        {
            _MoveActive = false;
        }
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        OnPlayerAim.Invoke(ctx.ReadValue<Vector2>().x);

    }

    public void OnFire(InputAction.CallbackContext ctx)
    {

        OnPlayerShoot.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        OnPlayerPause.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
    }

    public void OnSpecial(InputAction.CallbackContext ctx)
    {
        OnPlayerSpecial.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
    }

    public void OnMoveUp(InputAction.CallbackContext ctx)
    {
        OnPlayerMoveUp.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
    }

    public void OnMoveDown(InputAction.CallbackContext ctx)
    {
        OnPlayerMoveDown.Invoke(ButtonStatesHandler.ConvertBoolsToState(ctx.started, ctx.performed, ctx.canceled));
    }


}


