using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.ShipSelection;

public class PlayerInputCatcher : MonoBehaviour
{
    [SerializeField]
    private Selectionbar selectionBar;

    public void OnNavigate(InputAction.CallbackContext callbackContext)
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
    }
}
