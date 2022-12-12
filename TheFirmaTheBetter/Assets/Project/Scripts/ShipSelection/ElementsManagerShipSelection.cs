using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipSelection
{
    public class ElementsManagerShipSelection : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> elementsToHide = new List<GameObject>();

        [SerializeField]
        private List<GameObject> elementsToDisplay = new List<GameObject>();

        private int playerNumber;
        private void Awake()
        {
            Channels.OnShipCompleted += ShipCompletedScreenManagement;
        }

        //Managing what elements to be hidden and waht elements to be displayed
        //when a ship is completed
        private void ShipCompletedScreenManagement(ShipBuilder completedShipBuilder)
        {
            playerNumber = GetComponentInParent<PlayerSelectionScreen>().PlayerNumber;
            HideElements(elementsToHide, completedShipBuilder);
            ShowElements(elementsToDisplay, completedShipBuilder);
        }

        private void OnDestroy()
        {
            Channels.OnShipCompleted -= ShipCompletedScreenManagement;
        }


        private void HideElements(List<GameObject> elementsToHide, ShipBuilder completedBuilder)
        {
            if (completedBuilder.PlayerNumber != playerNumber)
                return;
            foreach (GameObject element in elementsToHide)
            { 
                if (!element.activeSelf)
                    return;
                element.SetActive(false);
            }
        }

        private void ShowElements(List<GameObject> elementsToShow, ShipBuilder completedBuilder)
        {
            if (completedBuilder.PlayerNumber != playerNumber)
                return;
            foreach (GameObject element in elementsToShow)
            {
                if (element.activeSelf)
                    return;
                element.SetActive(true);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
