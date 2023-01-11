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


        private void Start()
        {
            foreach (SelectableCollection collection in selectionBar.SelectionCollections)
            {
                Part currentSelectedPartInCollection = collection.Selectables[collection.CurrentSelectedIndex].Part;

                Channels.OnShipPartSelected?.Invoke(currentSelectedPartInCollection, selectionScreen.PlayerNumber);
            }
        }

        public void OnSelectPart()
        {
            Part currentSelectedPart = selectionBar.GetCurrentSelectedPart();

            Channels.OnShipPartSelected?.Invoke(currentSelectedPart, selectionScreen.PlayerNumber);
        }
    }
}
