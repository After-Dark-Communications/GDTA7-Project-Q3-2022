using EventSystem;
using ShipParts.Engines;
using ShipParts.Ship;
using UnityEngine;

namespace ShipParts
{
    class ShipResources : MonoBehaviour
    {
        private ShipBuilder shipBuilder;
        private ShipStats shipStats;
        private ShipHealth shipHealth;
        private ShipEnergy shipEnergy;

        private void Awake()
        {
            shipBuilder = GetComponent<ShipBuilder>();
            shipStats = new ShipStats();
            shipHealth = new ShipHealth(shipBuilder.PlayerNumber, shipStats);
            shipEnergy = new ShipEnergy(shipBuilder.PlayerNumber, shipStats);
        }

        private void Start()
        {
            Channels.OnShipPartSelected += OnShipPartSelected;
            Channels.OnShipCompleted += OnShipCompleted;
        }

        private void OnShipCompleted(ShipBuilder completedShipBuilder)
        {
            if (shipBuilder.PlayerNumber != completedShipBuilder.PlayerNumber)
                return;

            shipHealth.UpdateHealth(shipStats);
            shipEnergy.UpdateEnergy(shipStats);
        }

        private void OnShipPartSelected(Part selectedPart, int playerNumber)
        {
            if (shipBuilder.PlayerNumber != playerNumber)
                return;

            if (selectedPart is Engine)
            {
                shipBuilder = GetComponent<ShipBuilder>();
                shipStats = new ShipStats();
                shipHealth = new ShipHealth(shipBuilder.PlayerNumber, shipStats);
            }
        }
        public int CurrentEnergyAmount => shipEnergy.CurrentEnergyAmount;

        public ShipStats ShipStats => shipStats;
    }
}
