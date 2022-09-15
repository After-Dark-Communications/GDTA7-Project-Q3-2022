using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilder : MonoBehaviour
{
    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private PartsCollectionManager collectionManager;

    private List<Part> availablePlayerParts = new List<Part>();

    private void Awake()
    {
        Channels.OnShipPartSelected += OnShipPartSelected;
    }

    private void Start()
    {
        foreach (Part part in collectionManager.AllParts)
        {
            GameObject instance = Instantiate(part.gameObject);

            Part instancePart = instance.GetComponent<Part>();
            availablePlayerParts.Add(instancePart);

            instance.transform.SetParent(transform);
            instance.gameObject.SetActive(false);
        }
    }

    private void OnShipPartSelected(Part selectedPart, int playerNumber)
    {
        if (this.playerNumber != playerNumber)
            return;

        Part currentSelectedPart = selectedPart;

        HideAllParts();
        SpawnPart();

        void HideAllParts()
        {
            foreach (Part part in availablePlayerParts)
            {
                if (part.partCategoryName != currentSelectedPart.partCategoryName)
                    continue;

                part.gameObject.SetActive(false);
            }
        }

        void SpawnPart()
        {
            foreach (Part part in availablePlayerParts)
            {
                if (part.GetType() == currentSelectedPart.GetType())
                {
                    part.gameObject.SetActive(true);
                    part.ConnectionPointCollection.ConnectPartToCorrectPoint(part);
                    break;
                }
            }
        }
    }
}
