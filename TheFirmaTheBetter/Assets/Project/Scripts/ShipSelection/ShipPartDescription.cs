using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using ShipParts;
using TMPro;
using ShipSelection;

public class ShipPartDescription : MonoBehaviour
{
    [SerializeField]
    private RectTransform descriptionBox;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private PlayerSelectionScreen playerSelectionScreen;

    private void OnEnable()
    {
        Channels.OnShipPartHovered += OnShipPartHovered;
    }

    private void OnDisable()
    {
        Channels.OnShipPartHovered -= OnShipPartHovered;
    }

    private void Start()
    {
        HideDescription();
    }

    private void OnShipPartHovered(Part hoveredPart, int playerNumber)
    {
        if (playerNumber != playerSelectionScreen.PlayerNumber)
            return;

        string partDescription = hoveredPart.GetData().PartDescription;
        if (string.IsNullOrWhiteSpace(partDescription) == false)
        {
            ShowDescription(partDescription);
        }
        else
        {
            HideDescription();
        }
    }

    private void ShowDescription(string description)
    {
        descriptionText.text = description;
        descriptionBox.gameObject.SetActive(true);
    }

    private void HideDescription()
    {
        descriptionText.text = "";
        descriptionBox.gameObject.SetActive(false);
    }
}
