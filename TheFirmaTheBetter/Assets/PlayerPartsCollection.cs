using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.ShipSelection
{
    public class PlayerPartsCollection : MonoBehaviour
    {
        [SerializeField]
        private Selectionbar selectionBar;

        [SerializeField]
        private PlayerSelectionScreen selectionScreen;

        public void OnSelectPart(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.phase != InputActionPhase.Started)
                return;
            
            Part currentSelectedPart = selectionBar.CurrentSelectable.Part;

            Channels.OnShipPartSelected.Invoke(currentSelectedPart, selectionScreen.PlayerNumber);
        }
    }
}
