using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.ShipSelection;

public class PlayerInputCatcher : MonoBehaviour
{
    [SerializeField]
    private Selectionbar selectionBar;

    public void OnNavigate(InputAction.CallbackContext callbackContext, PlayerInput playerInput)
    {
        Vector2 moveVector = callbackContext.ReadValue<Vector2>();

        if (moveVector == Vector2.up)
        {
            selectionBar.OnNavigate_Up();
        }
        else if (moveVector == Vector2.down)
        {
            selectionBar.OnNavigate_Down();
        }
        else if (moveVector == Vector2.left)
        {
            selectionBar.OnNavigate_Left();
        }
        else if (moveVector == Vector2.right)
        {
            selectionBar.OnNavigate_Right();
        }
    }
}
