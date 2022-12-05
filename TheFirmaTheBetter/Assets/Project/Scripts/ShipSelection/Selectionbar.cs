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
            // Animator should be loaded to handle the first buton to be selected
            Channels.OnShipAnimationManagerLoaded += SetUpSelectionBar;
            //Channels.OnPlayerJoined += SetUpSelectionBar;
        }


        private void OnDestroy()
        {
            //Channels.OnPlayerJoined -= SetUpSelectionBar;
            Channels.OnShipAnimationManagerLoaded -= SetUpSelectionBar;
        }
        private void SetUpSelectionBar()
        {

            buttonSelectionManager.ResetButtons();
            buttonSelectionManager.UpdateButtons(this);
            Channels.OnSelectedCategoryChanged?.Invoke(CurrentSelectedCollection, this.playerNumber);
            SetSelectedOptionIndex(CurrentSelectedCollection.CurrentSelectedIndex);
        }

        private void Start()
        {
            playerNumber = GetComponentInParent<PlayerSelectionScreen>().PlayerNumber;
        }

        public void OnNavigate_Up()
        {
            // Change the category
            currentSelectedCollectionIndex = ListLooper.SelectPrevious(selectionCollections, currentSelectedCollectionIndex);
            UpdateLabelTexts();
            buttonSelectionManager.ResetButtons();
            buttonSelectionManager.UpdateButtons(this);
            Channels.OnSelectedCategoryChanged?.Invoke(CurrentSelectedCollection, playerNumber);

            // Update the hovered button
            buttonSelectionManager.UpdateHoverEffectAt(currentHoveredIndex, false);
            currentHoveredIndex = CurrentSelectedCollection.CurrentSelectedIndex;
            buttonSelectionManager.UpdateHoverEffectAt(currentHoveredIndex, true);
            Channels.OnShipPartHovered?.Invoke(GetCurrentHoveredPart(), playerNumber);

            Channels.OnNavigateUp?.Invoke(playerNumber);
        }

        public void OnNavigate_Down()
        {
            // Change the category
            currentSelectedCollectionIndex = ListLooper.SelectNext(selectionCollections, currentSelectedCollectionIndex);
            UpdateLabelTexts();
            buttonSelectionManager.ResetButtons();
            buttonSelectionManager.UpdateButtons(this);
            Channels.OnSelectedCategoryChanged?.Invoke(CurrentSelectedCollection, playerNumber);

            // Update the hovered button
            buttonSelectionManager.UpdateHoverEffectAt(currentHoveredIndex, false);
            currentHoveredIndex = CurrentSelectedCollection.CurrentSelectedIndex;
            buttonSelectionManager.UpdateHoverEffectAt(currentHoveredIndex, true);
            Channels.OnShipPartHovered?.Invoke(GetCurrentHoveredPart(), playerNumber);

            Channels.OnNavigateDown?.Invoke(playerNumber);
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
            Channels.OnShipPartHovered?.Invoke(GetCurrentHoveredPart(), playerNumber);
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
            Channels.OnShipPartHovered?.Invoke(GetCurrentHoveredPart(), playerNumber);
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
            // Error check if the index in the function is different than the current Hovered index
            if (index != currentHoveredIndex)
            {
                index = currentHoveredIndex;
            }
           
            buttonSelectionManager.ResetButtons();
            CurrentSelectedCollection.CurrentSelectedIndex = index;
            buttonSelectionManager.ResetButtonAt(CurrentSelectedCollection.CurrentSelectedIndex);
            buttonSelectionManager.UpdateButtons(this);

        }

        public Part GetCurrentSelectedPart()
        {

            return CurrentSelectedCollection.Selectables[currentHoveredIndex].Part;
        }

        public Part GetCurrentHoveredPart()
        {
            return CurrentSelectedCollection.Selectables[currentHoveredIndex].Part;
        }

        public List<SelectableCollection> SelectionCollections => selectionCollections;
        public SelectableCollection CurrentSelectedCollection => selectionCollections[currentSelectedCollectionIndex];
        public int CurrentSelectedIndex { get => currentSelectedCollectionIndex; }

        public string CurrentCategoryName => CurrentSelectedCollection.CategoryName;

        public int CurrentHoveredIndex => currentHoveredIndex;
    }
}
