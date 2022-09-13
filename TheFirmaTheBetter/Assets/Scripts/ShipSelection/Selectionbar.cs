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
            Channels.OnManagerInitialized += OnManagerInitialize;

            Channels.Movement.OnNavigateUI_Up += OnNavigate_Up;
            Channels.Movement.OnNavigateUI_Down += OnNavigate_Down;
            Channels.Movement.OnNavigateUI_Right += OnNavigate_Right;
            Channels.Movement.OnNavigateUI_Left += OnNavigate_Left;
        }

        private void Start()
        {
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.EngineList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.WeaponList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.SpecialList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.CoreList));
        }

        private void OnManagerInitialize(object sender, Manager manager)
        {
            if (manager.GetType() != typeof(PartsCollectionManager))
                return;

            collectionManager = manager as PartsCollectionManager;
        }

        public void OnNavigate_Up(object sender, Vector2 movementVector)
        {
            currentSelectedCollectionIndex = ListLooper.SelectPrevious(selectionCollections, currentSelectedCollectionIndex);
        }

        public void OnNavigate_Down(object sender, Vector2 movementVector)
        {
            currentSelectedCollectionIndex = ListLooper.SelectNext(selectionCollections, currentSelectedCollectionIndex);
        }

        public void OnNavigate_Left(object sender, Vector2 movementVector)
        {
            selectionCollections[currentSelectedCollectionIndex].SelectPreviousSelectable();
        }

        public void OnNavigate_Right(object sender, Vector2 movementVector)
        {
            selectionCollections[currentSelectedCollectionIndex].SelectNextSelectable();
        }

        public SelectableCollection CurrentSelectedCollection => selectionCollections[currentSelectedCollectionIndex];
        public Selectable CurrentSelectable => selectionCollections[currentSelectedCollectionIndex].CurrentSelectedOption;
    }
}
