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

    private const string TRIGGER_ENABLE_NAME = "Enable";
    private const string TRIGGER_DISABLE_NAME = "Disable";
    private Animator animator;

    private void OnEnable()
    {
        Channels.OnNavigateUp += CategoryChangedListUP;
        Channels.OnNavigateDown += CategoryChangedListDOWN;
    }

    private void CategoryChangedListDOWN(int playerNumber)
    {
       
    }

    private void CategoryChangedListUP(int playerNumber)
    {
        throw new NotImplementedException();
    }

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

    private void CategoryChanged(SelectableCollection currentSelectedCollection, int playerNumber)
    {
        if (playerSelectionScreen.PlayerNumber != playerNumber)
            return;

        tmpTextField.SetText(currentSelectedCollection.CategoryName);
    }

    private void CategoryChangedList (SelectableCollection currentSelectedCollection, int playerNumber)
    {
        if (playerSelectionScreen.PlayerNumber != playerNumber)
            return;

        String current = currentSelectedCollection.CategoryName.ToLower();
        String field = tmpTextField.text.ToLower();
        if (String.Compare(current, field) == 0)
        {
            Debug.Log($"equal: " + current + ' ' + field);
            animator.SetTrigger(TRIGGER_ENABLE_NAME);
            return;
        }
        else
        {
            Debug.Log($"--not " + current + ' ' + field);
            animator.SetTrigger(TRIGGER_DISABLE_NAME);
        }
    }

    public void PlaySelectedAnimation()
    {
       //animator.SetTrigger(TRIGGER_NAME);
    }
}
