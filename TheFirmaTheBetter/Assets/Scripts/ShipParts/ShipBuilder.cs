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

    private Core selectedCore;
    private Engine selectedEngine;
    private SpecialAbility selectedSpecial;
    private Weapon selectedWeapon;

    private List<Part> selectedParts = new List<Part>();

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

            instance.transform.position = new Vector3(0,0,0);
            instance.transform.localPosition = Vector3.zero;
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
                if (part.PartCategoryName != currentSelectedPart.PartCategoryName)
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
                    if (part is Core)
                    {
                        HandleSelectedCore(part);
                        part.gameObject.SetActive(true);
                        return;
                    }

                    if (selectedCore == null)
                        return;

                    part.gameObject.SetActive(true);
                    
                    ConnectSelectedPart(part);
                    break;
                }
            }
        }
    }

    private void ConnectSelectedPart(Part part)
    {
        int index = selectedParts.FindIndex(p => p.IsMyType(part));
        
        if (index >= 0)
            selectedParts.RemoveAt(index);

        selectedParts.Add(part);

        selectedCore.ConnectionPointCollection.ConnectPartToCorrectPoint(part);
    }

    private void HandleSelectedCore(Part part)
    {
        selectedCore = part as Core;

        foreach (Part selectedPart in selectedParts)
        {
            selectedCore.ConnectionPointCollection.ConnectPartToCorrectPoint(selectedPart);
        }
    }
}
