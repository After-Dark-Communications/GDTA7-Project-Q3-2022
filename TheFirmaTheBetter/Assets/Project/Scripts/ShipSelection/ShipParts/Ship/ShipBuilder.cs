using EventSystem;
using ShipParts.Cores;
using ShipSelection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipParts.Ship
{
    public class ShipBuilder : MonoBehaviour
    {
        [SerializeField]
        private int playerNumber;

        [SerializeField]
        private PartsCollectionManager collectionManager;

        private InputDevice playerDevice;
        private ShipBuilderInput shipBuilderInput;

        private List<Part> availablePlayerParts = new List<Part>();

        private Core selectedCore;

        private List<Part> selectedParts = new List<Part>();

        public bool IsTypeCore<T>()
        {
            return selectedCore is T;
        }

        private void Awake()
        {
            Channels.OnPlayerJoined += OnPlayerJoined;
            Channels.OnShipPartSelected += OnShipPartSelected;
            Channels.Input.OnShipCompletedInput += OnShipCompletedInput;

        }

        private void OnDestroy()
        {
            Channels.OnPlayerJoined -= OnPlayerJoined;
            Channels.OnShipPartSelected -= OnShipPartSelected;
            Channels.Input.OnShipCompletedInput -= OnShipCompletedInput;

            if (shipBuilderInput == null)
                return;

            shipBuilderInput.UnSubscribeToEvents();
        }

        private void OnPlayerJoined(int playerNumber, InputDevice playerDevice)
        {
            if (this.playerNumber != playerNumber)
                return;

            this.playerDevice = playerDevice;

            shipBuilderInput = new ShipBuilderInput(playerDevice, playerNumber);
        }

        private void OnShipCompletedInput(int playerNumber)
        {
            if (playerNumber != this.playerNumber)
                return;

            if (transform == null)
                return;

            transform.parent = null;
            DontDestroyOnLoad(transform);

            Channels.OnShipCompleted?.Invoke(this);
        }

        private void Start()
        {
            foreach (Part part in collectionManager.AllParts)
            {
                Part instancePart = Instantiate(part);

                availablePlayerParts.Add(instancePart);

                instancePart.transform.SetParent(transform);

                instancePart.transform.position = new Vector3(0, 0, 0);
                instancePart.transform.localPosition = Vector3.zero;
                instancePart.gameObject.SetActive(false);
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
                        RemovePartFromListIfAble(part);

                        if (part is Core)
                        {
                            HandleSelectedCore(part);
                            part.gameObject.SetActive(true);
                            selectedParts.Add(part);
                            return;
                        }

                        if (selectedCore == null)
                            return;

                        part.gameObject.SetActive(true);

                        ConnectSelectedPart(part);
                        break;
                    }
                }

                void ConnectSelectedPart(Part part)
                {
                    selectedParts.Add(part);

                    selectedCore.ConnectionPointCollection.ConnectPartToCorrectPoint(part);
                }

                void RemovePartFromListIfAble(Part part)
                {
                    int index = selectedParts.FindIndex(p => p.IsMyType(part));

                    if (index >= 0)
                        selectedParts.RemoveAt(index);
                }
            }
        }

        private void HandleSelectedCore(Part part)
        {
            selectedCore = part as Core;

            foreach (Part selectedPart in selectedParts)
            {
                selectedCore.ConnectionPointCollection.ConnectPartToCorrectPoint(selectedPart);
            }
        }


        public int PlayerNumber => playerNumber;
        public List<Part> SelectedParts => selectedParts;
        public InputDevice PlayerDevice => playerDevice;
    }
}