using EventSystem;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedCategory : MonoBehaviour
{
    private TMP_Text tmpTextField;

    private PlayerSelectionScreen playerSelectionScreen;

    private void Start()
    {
        tmpTextField = GetComponent<TMP_Text>();
        playerSelectionScreen = GetComponentInParent<PlayerSelectionScreen>();

        Channels.OnSelectedCategoryChanged += CategoryChanged;
    }

    private void CategoryChanged(SelectableCollection currentSelectedCollection, int playerNumber)
    {
        if (playerSelectionScreen.PlayerNumber != playerNumber)
            return;

        tmpTextField.SetText(currentSelectedCollection.CategoryName);
    }
}
