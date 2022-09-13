using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCatcher : MonoBehaviour
{
    public void OnNavigate(InputAction.CallbackContext callbackContext)
    {
        Vector2 moveVector = callbackContext.ReadValue<Vector2>();

        if (moveVector == Vector2.up)
        {
            Channels.Movement.OnNavigateUI_Up.Invoke(gameObject, moveVector);
        }
        else if (moveVector == Vector2.down)
        {
            Channels.Movement.OnNavigateUI_Down.Invoke(gameObject, moveVector);
        }
        else if (moveVector == Vector2.left)
        {
            Channels.Movement.OnNavigateUI_Left.Invoke(gameObject, moveVector);
        }
        else if (moveVector == Vector2.right)
        {
            Channels.Movement.OnNavigateUI_Right.Invoke(gameObject, moveVector);
        }
    }
}
