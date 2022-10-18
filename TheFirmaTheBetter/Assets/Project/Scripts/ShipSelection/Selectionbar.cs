using ShipSelection;
using EventSystem;
using ShipParts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Util;

namespace ShipSelection
{
    public class Selectionbar : MonoBehaviour
    {
        [SerializeField]
        private ButtonSelectionManager buttonSelectionManager;

        private PlayerSelectionScreensData selectionScreensData;

        private List<SelectableCollection> selectionCollections = new List<SelectableCollection>();

        private List<Arrow> arrowsUI = new List<Arrow>();

        private List<SelectableOption> selectableOptions = new List<SelectableOption>();

        private int currentSelectedCollectionIndex = 0;

        private int playerNumber;

        private void Awake()
        {
            foreach (SelectableOption selectableOption in gameObject.GetComponentsInChildren<SelectableOption>())
            {
                selectableOptions.Add(selectableOption);
            }
            foreach (Arrow arrow in GetComponentsInChildren<Arrow>())
            {
                arrowsUI.Add(arrow);
            }

            selectionScreensData = GetComponentInParent<PlayerSelectionScreensData>();
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(selectionScreensData.CollectionManager.CoreList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(selectionScreensData.CollectionManager.EngineList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(selectionScreensData.CollectionManager.WeaponList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(selectionScreensData.CollectionManager.SpecialList));
            UpdateLabelTexts();
        }

        private void Start()
        {
            playerNumber = GetComponentInParent<PlayerSelectionScreen>().PlayerNumber;
        }

        public void OnNavigate_Up()
        {
            currentSelectedCollectionIndex = ListLooper.SelectPrevious(selectionCollections, currentSelectedCollectionIndex);
            UpdateLabelTexts();
            buttonSelectionManager.UpdateButtons(this);
            Channels.OnSelectedCategoryChanged?.Invoke(CurrentSelectedCollection, playerNumber);
            Channels.OnNavigateUp?.Invoke();
        }

        public void OnNavigate_Down()
        {
            currentSelectedCollectionIndex = ListLooper.SelectNext(selectionCollections, currentSelectedCollectionIndex);
            UpdateLabelTexts();
            buttonSelectionManager.UpdateButtons(this);
            Channels.OnSelectedCategoryChanged?.Invoke(CurrentSelectedCollection, playerNumber);
            Channels.OnNavigateDown?.Invoke();
        }

        private void UpdateLabelTexts()
        {
            for (int i = 0; i <= selectionCollections.Count; i++)
            {
                //TODO: Make this an icon with text
                selectableOptions[i].SetSprite(selectionCollections[currentSelectedCollectionIndex].Selectables[i].Part.PartIcon);
                selectableOptions[i].SetText(selectionCollections[currentSelectedCollectionIndex].Selectables[i].Part.GetData().PartName);
            }
        }

        public void SetSelectedOptionIndex(int index)
        {
            CurrentSelectedCollection.CurrentSelectedIndex = index;
            buttonSelectionManager.UpdateButtons(this);
        }

        public Part GetCurrentSelectedPart()
        {
            buttonSelectionManager.UpdateButtons(this);
            return CurrentSelectedCollection.Selectables[CurrentSelectedCollection.CurrentSelectedIndex].Part;
        }

        public List<SelectableCollection> SelectionCollections => selectionCollections;
        public SelectableCollection CurrentSelectedCollection => selectionCollections[currentSelectedCollectionIndex];
        public int CurrentSelectedIndex { get => currentSelectedCollectionIndex; }
    }
}
