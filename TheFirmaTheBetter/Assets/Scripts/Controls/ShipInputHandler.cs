using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/*
 * Steef is right :)
 * -Luc
 */
public class ShipInputHandler : MonoBehaviour
{
    [SerializeField]
    private Part SpecialPart, ChasisPart, EnginePart, WeaponPart;

    public UnityVector2Event OnPlayerMove = new UnityVector2Event();
    public UnityFloatEvent OnPlayerForwardBackward = new UnityFloatEvent();
    public UnityFloatEvent OnPlayerLeftRight = new UnityFloatEvent();
    public UnityFloatEvent OnPlayerAim = new UnityFloatEvent();
    public UnityBoolsEvent OnPlayerShoot = new UnityBoolsEvent();
    public UnityBoolsEvent OnPlayerPause = new UnityBoolsEvent();
    public UnityBoolsEvent OnPlayerSpecial = new UnityBoolsEvent();
    public UnityBoolsEvent OnPlayerMoveUp = new UnityBoolsEvent();
    public UnityBoolsEvent OnPlayerMoveDown = new UnityBoolsEvent();

    private Vector2 _MoveValues;
    private bool _MoveActive;
    private void Update()
    {
        if (_MoveActive)
        {
            //OnPlayerForwardBackward.Invoke(_MoveValues.y);
            //OnPlayerLeftRight.Invoke(_MoveValues.x);
            OnPlayerMove.Invoke(_MoveValues);
        }
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
        OnPlayerShoot.Invoke(new bool[] { ctx.started, ctx.performed, ctx.canceled });
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        OnPlayerPause.Invoke(new bool[] { ctx.started, ctx.performed, ctx.canceled });
    }

    public void OnSpecial(InputAction.CallbackContext ctx)
    {
        OnPlayerSpecial.Invoke(new bool[] { ctx.started, ctx.performed, ctx.canceled });
    }

    public void OnMoveUp(InputAction.CallbackContext ctx)
    {
        OnPlayerMoveUp.Invoke(new bool[] { ctx.started, ctx.performed, ctx.canceled });
    }

    public void OnMoveDown(InputAction.CallbackContext ctx)
    {
        OnPlayerMoveDown.Invoke(new bool[] { ctx.started, ctx.performed, ctx.canceled });
    }
}


