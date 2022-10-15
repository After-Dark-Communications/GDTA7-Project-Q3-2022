using EventSystem;
using ShipParts;
using ShipParts.Ship;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartAnimatorManager : MonoBehaviour
{
    private const string flashBoolName = "Flash";
 
    private List<Animator> animators = new List<Animator>();

    private int playerNumber;

    private Part part;


    private void Awake()
    {
        Channels.OnSelectedCategoryChanged += OnCategoryChanged;
        Channels.OnShipPartSelected += OnPartSelected;
        Channels.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void OnDestroy()
    {
        Channels.OnSelectedCategoryChanged -= OnCategoryChanged;
        Channels.OnShipPartSelected -= OnPartSelected;
        Channels.OnPlayerSpawned -= OnPlayerSpawned;
    }

    private void Start()
    {
        part = GetComponent<Part>();
        playerNumber = GetComponentInParent<ShipBuilder>().PlayerNumber;

        foreach (Animator animator in GetComponentsInChildren<Animator>())
        {
            animators.Add(animator);
        }
    }

    private void OnPartSelected(Part selectedPart, int playerNumber)
    {
        SetBoolsForPart(selectedPart, playerNumber);
    }

    private void OnCategoryChanged(SelectableCollection currentSelectedCollection, int playerNumber)
    {
        Part currentSelectedPartFromCategory = currentSelectedCollection.Selectables[currentSelectedCollection.CurrentSelectedIndex].Part;
        SetBoolsForPart(currentSelectedPartFromCategory, playerNumber);
    }

    private void OnPlayerSpawned(GameObject spawnedShipBuilderObject, int playerNumber)
    {
        if (playerNumber != this.playerNumber)
            return;

        SetAllFlashingBools(false);
    }


    private void SetBoolsForPart(Part selectedPart, int playerNumber = -1)
    {
        if (playerNumber == -1)
        {
            ShipBuilder builder = selectedPart.GetComponentInParent<ShipBuilder>();

            if (builder != null)
                playerNumber = builder.PlayerNumber;
        }

        if (this.playerNumber != playerNumber)
            return;

        if (selectedPart.IsMyType(part))
        {
            SetAllFlashingBools(true);
            return;
        }

        SetAllFlashingBools(false);
    }

    private void SetAllFlashingBools(bool value)
    {
        foreach (Animator animator in animators)
        {
            animator.SetBool(flashBoolName, value);
        }
    }
}
