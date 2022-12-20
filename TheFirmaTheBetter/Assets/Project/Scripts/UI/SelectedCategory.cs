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
    private const string BOOL_ENABLE_NAME = "Enabled";

    private Animator animator;

    private void Start()
    {
        tmpTextField = GetComponent<TMP_Text>();
        playerSelectionScreen = GetComponentInParent<PlayerSelectionScreen>();

        Channels.OnSelectedCategoryChanged += CategoryChangedList;

        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        Channels.OnSelectedCategoryChanged -= CategoryChangedList;
    }
#
    /// <summary>
    /// Old solution
    /// </summary>
    /// <param name="currentSelectedCollection"></param>
    /// <param name="playerNumber"></param>
    //private void CategoryChanged(SelectableCollection currentSelectedCollection, int playerNumber)
    //{
    //    if (playerSelectionScreen.PlayerNumber != playerNumber)
    //        return;

    //    tmpTextField.SetText(currentSelectedCollection.CategoryName);
    //}

    private void CategoryChangedList (SelectableCollection currentSelectedCollection, int playerNumber)
    {
        if (playerSelectionScreen.PlayerNumber != playerNumber)
            return;

        String current = currentSelectedCollection.CategoryName.ToLower();
        String field = tmpTextField.text.ToLower();

        if (String.Compare(current, field) == 0)
        {
            animator.SetBool(BOOL_ENABLE_NAME, true);

        }
        else
        {

           animator.SetBool(BOOL_ENABLE_NAME, false);
        }
    }

}
