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
using System;

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

        private int currentHoveredIndex = 0;

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

            Channels.OnPlayerJoined += SetUpSelectionBar;
        }


        private void OnDestroy()
        {
            Channels.OnPlayerJoined -= SetUpSelectionBar;
        }
        private void SetUpSelectionBar(int playerNumber, InputDevice joinedPlayerDevice)
        {
            //OnNavigate_Up();
            // OnNavigate_Down();
            buttonSelectionManager.ResetButtons();
            buttonSelectionManager.UpdateButtons(this);
            Channels.OnSelectedCategoryChanged?.Invoke(CurrentSelectedCollection, this.playerNumber);
            SetSelectedOptionIndex(CurrentSelectedCollection.CurrentSelectedIndex);
        }

        private void Start()
        {
            playerNumber = GetComponentInParent<PlayerSelectionScreen>().PlayerNumber;

            //Update
            //buttonSelectionManager.ResetButtons();
            //buttonSelectionManager.UpdateButtons(this);


        }

        public void OnNavigate_Up()
        {
            currentSelectedCollectionIndex = ListLooper.SelectPrevious(selectionCollections, currentSelectedCollectionIndex);
            UpdateLabelTexts();
            buttonSelectionManager.ResetButtons();
            buttonSelectionManager.UpdateButtons(this);

            Channels.OnSelectedCategoryChanged?.Invoke(CurrentSelectedCollection, playerNumber);
            Channels.OnNavigateUp?.Invoke();
        }

        public void OnNavigate_Down()
        {
            currentSelectedCollectionIndex = ListLooper.SelectNext(selectionCollections, currentSelectedCollectionIndex);
            UpdateLabelTexts();
            buttonSelectionManager.ResetButtons();
            buttonSelectionManager.UpdateButtons(this);

            Channels.OnSelectedCategoryChanged?.Invoke(CurrentSelectedCollection, playerNumber);
            Channels.OnNavigateDown?.Invoke();
        }

        public void OnNavigate_Right()
        {
            buttonSelectionManager.UpdateHoverEffectAt(currentHoveredIndex, false);
            currentHoveredIndex++;
            if (currentHoveredIndex > CurrentSelectedCollection.Selectables.Count - 1)
            {
                currentHoveredIndex = 0;
            }
            buttonSelectionManager.UpdateHoverEffectAt(currentHoveredIndex, true);
        }

        public void OnNavigate_Left()
        {
            buttonSelectionManager.UpdateHoverEffectAt(currentHoveredIndex, false);
            currentHoveredIndex--;
            if (currentHoveredIndex < 0)
            {
                currentHoveredIndex = CurrentSelectedCollection.Selectables.Count - 1;
            }
            buttonSelectionManager.UpdateHoverEffectAt(currentHoveredIndex, true);
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
            buttonSelectionManager.ResetButtonAt(CurrentSelectedCollection.CurrentSelectedIndex);

            CurrentSelectedCollection.CurrentSelectedIndex = index;

            buttonSelectionManager.UpdateButtons(this);
        }

        public Part GetCurrentSelectedPart()
        {


            // buttonSelectionManager.UpdateButtons(this);
            return CurrentSelectedCollection.Selectables[currentHoveredIndex].Part;
        }

        public List<SelectableCollection> SelectionCollections => selectionCollections;
        public SelectableCollection CurrentSelectedCollection => selectionCollections[currentSelectedCollectionIndex];
        public int CurrentSelectedIndex { get => currentSelectedCollectionIndex; }

        public string CurrentCategoryName => CurrentSelectedCollection.CategoryName;

        public int CurrentHoveredIndex => currentHoveredIndex;
    }
}
