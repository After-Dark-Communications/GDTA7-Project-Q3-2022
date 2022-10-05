using EventSystem;
using ShipParts;
using UnityEngine;

namespace ShipSelection
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
