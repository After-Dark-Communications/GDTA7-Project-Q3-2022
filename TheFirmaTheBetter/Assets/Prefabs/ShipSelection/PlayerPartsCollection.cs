using Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace  ShipSelection
{
    public class PlayerPartsCollection : MonoBehaviour
    {
        [SerializeField]
        private Selectionbar selectionBar;

        [SerializeField]
        private PlayerSelectionScreen selectionScreen;

        public void OnSelectPart()
        {
            Part currentSelectedPart = selectionBar.GetCurrentSelectedPart();

            Channels.OnShipPartSelected.Invoke(currentSelectedPart, selectionScreen.PlayerNumber);
        }
    }
}
