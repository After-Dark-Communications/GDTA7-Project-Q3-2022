using EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSelection
{
    public class PlayerInputCatcher : MonoBehaviour
    {
        [SerializeField]
        private Selectionbar selectionBar;

        [SerializeField]
        private PlayerSelectionScreen playerSelectionScreen;

        public void OnNavigate(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started == false)
                return;

            Vector2 moveVector = callbackContext.ReadValue<Vector2>();

            if (moveVector == Vector2.up)
            {
                selectionBar.OnNavigate_Up();
            }
            if (moveVector == Vector2.down)
            {
                selectionBar.OnNavigate_Down();
            }
            if (moveVector == Vector2.left)
            {
                selectionBar.OnNavigate_Left();
            }
            if (moveVector == Vector2.right)
            {
                selectionBar.OnNavigate_Right();
            }

            int selectionBarIndex = selectionBar.CurrentSelectedIndex;
            int playerNumber = playerSelectionScreen.PlayerNumber;

            Channels.Input.OnSelectionBarUpAndDownNaviagtedInput?.Invoke(playerNumber, selectionBarIndex);
        }

        public void OnInputConfirmShip(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                int playerNumber = playerSelectionScreen.PlayerNumber;
                Channels.Input.OnShipCompletedInput?.Invoke(playerNumber);
            }
        }
    }
}