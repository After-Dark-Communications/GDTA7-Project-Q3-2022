using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts.ShipSelection
{
    public class Selectionbar : MonoBehaviour
    {
        private PlayerSelectionScreensData selectionScreensData;

        private List<SelectableCollection> selectionCollections = new List<SelectableCollection>();

        private List<TMP_Text> buttonLabels = new List<TMP_Text>();

        private int currentSelectedCollectionIndex = 0;

        private void Awake()
        {
            foreach (Button button in gameObject.GetComponentsInChildren<Button>())
            {
                buttonLabels.Add(button.GetComponentInChildren<TMP_Text>());
            }

            selectionScreensData = GetComponentInParent<PlayerSelectionScreensData>();
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(selectionScreensData.CollectionManager.EngineList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(selectionScreensData.CollectionManager.WeaponList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(selectionScreensData.CollectionManager.SpecialList));
            selectionCollections.Add(SelectionCollectionInitializer.CreateNewSelectableCollection(selectionScreensData.CollectionManager.CoreList));
            UpdateLabelTexts();
        }

        public void OnNavigate_Up()
        {
            currentSelectedCollectionIndex = ListLooper.SelectPrevious(selectionCollections, currentSelectedCollectionIndex);
            UpdateLabelTexts();
        }

        public void OnNavigate_Down()
        {
            currentSelectedCollectionIndex = ListLooper.SelectNext(selectionCollections, currentSelectedCollectionIndex);

            UpdateLabelTexts();
        }

        private void UpdateLabelTexts()
        {
            for (int i = 0; i <= selectionCollections.Count; i++)
            {
                buttonLabels[i].SetText(selectionCollections[currentSelectedCollectionIndex].Selectables[i].Part.ToString());
            }
        }

        public void SetSelectedOptionIndex(int index)
        {
            CurrentSelectedCollection.CurrentSelectedIndex = index;
        }

        public Part GetCurrentSelectedPart()
        {
            return CurrentSelectedCollection.Selectables[CurrentSelectedCollection.CurrentSelectedIndex].Part;
        }

        public SelectableCollection CurrentSelectedCollection => selectionCollections[currentSelectedCollectionIndex];
    }
}
