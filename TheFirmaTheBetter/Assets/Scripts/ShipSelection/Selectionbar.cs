using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.ShipSelection
{
    public class Selectionbar : MonoBehaviour
    {
        private PartsCollectionManager collectionManager;

        private List<SelectableCollection> selectionCollections = new List<SelectableCollection>();

        private int currentSelectedCollectionIndex = 0;

        private void Awake()
        {
            Channels.OnManagerInitialized.AddListener(OnManagerInitialize);
        }

        private void Start()
        {
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.EngineList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.WeaponList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.SpecialList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.CoreList));
        }

        private void OnManagerInitialize(Manager manager)
        {
            if (manager.GetType() != typeof(PartsCollectionManager))
                return;

            collectionManager = manager as PartsCollectionManager;
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
