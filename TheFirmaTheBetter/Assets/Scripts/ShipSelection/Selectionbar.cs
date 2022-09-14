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
        }

        private void Start()
        {
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.EngineList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.WeaponList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.SpecialList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(collectionManager.CoreList));
        }

        public void OnNavigate_Up()
        {
            currentSelectedCollectionIndex = ListLooper.SelectPrevious(selectionCollections, currentSelectedCollectionIndex);
        }

        public void OnNavigate_Down()
        {
            currentSelectedCollectionIndex = ListLooper.SelectNext(selectionCollections, currentSelectedCollectionIndex);
        }

        public void OnNavigate_Left()
        {
            selectionCollections[currentSelectedCollectionIndex].SelectPreviousSelectable();
        }

        public void OnNavigate_Right()
        {
            selectionCollections[currentSelectedCollectionIndex].SelectNextSelectable();
        }

        private void OnManagerInitialize(object sender, Manager manager)
        {
            if (manager.GetType() != typeof(PartsCollectionManager))
                return;

            collectionManager = manager as PartsCollectionManager;
        }

        public SelectableCollection CurrentSelectedCollection => selectionCollections[currentSelectedCollectionIndex];
        public Selectable CurrentSelectable => selectionCollections[currentSelectedCollectionIndex].CurrentSelectedOption;
    }
}
