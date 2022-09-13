using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.ShipSelection
{
    public class Selectionbar : MonoBehaviour
    {
        [SerializeField]
        private PlayerNumber playerNumber;

        private List<SelectableCollection> selectionCollections = new List<SelectableCollection>();

        private int currentSelectedCollectionIndex = 0;

        private void Start()
        {
            //TODO:
        }

        public void OnNavigate(InputAction.CallbackContext callbackContext)
        {
            Vector2 moveVector = callbackContext.ReadValue<Vector2>();

            if (moveVector == Vector2.up)
            {
                SelectPreviousCollection();
            }
            else if (moveVector == Vector2.down)
            {
                SelectNextCollection();
            }
            else if (moveVector == Vector2.left)
            {
                SelectPreviousSelectable();
            }
            else if (moveVector == Vector2.right)
            {
                SelectNextSelectable();
            }
        }

        private void SelectNextCollection()
        {
            currentSelectedCollectionIndex = ListLooper.SelectNext(selectionCollections, currentSelectedCollectionIndex);
        }

        private void SelectPreviousCollection()
        {
            currentSelectedCollectionIndex = ListLooper.SelectPrevious(selectionCollections, currentSelectedCollectionIndex);
        }

        private void SelectNextSelectable()
        {
            selectionCollections[currentSelectedCollectionIndex].SelectNextSelectable();
        }

        private void SelectPreviousSelectable()
        {
            selectionCollections[currentSelectedCollectionIndex].SelectPreviousSelectable();
        }

        public SelectableCollection CurrentSelectedCollection => selectionCollections[currentSelectedCollectionIndex];
        public Selectable CurrentSelectable => selectionCollections[currentSelectedCollectionIndex].CurrentSelectedOption;
    }
}
