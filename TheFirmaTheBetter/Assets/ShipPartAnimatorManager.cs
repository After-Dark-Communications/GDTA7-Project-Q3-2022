using EventSystem;
using ShipParts;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShipPartAnimatorManager : MonoBehaviour
{
    private const string flashBoolName = "Flash";
    
    private Part part;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        part = GetComponent<Part>();
    }

    private void OnEnable()
    {
        Channels.OnSelectedCategoryChanged += OnCategoryChanged;
        Channels.OnShipPartSelected += OnPartSelected;
    }

    private void OnPartSelected(Part selectedPart, int playerNumber)
    {
        if (selectedPart.IsMyType(part))
        {
            animator.SetBool(flashBoolName, true);
            return;
        }

        animator.SetBool(flashBoolName, false);
    }

    private void OnCategoryChanged(SelectableCollection currentSelectedCollection)
    {
        Part currentSelectedPartFromCategory = currentSelectedCollection.Selectables[currentSelectedCollection.CurrentSelectedIndex].Part;
        
        if (currentSelectedPartFromCategory.IsMyType(part))
        {
            animator.SetBool(flashBoolName, true);
            return;
        }

        animator.SetBool(flashBoolName, false);
    }

    private void OnDisable()
    {
        Channels.OnSelectedCategoryChanged -= OnCategoryChanged;
        Channels.OnShipPartSelected -= OnPartSelected;
    }
}
