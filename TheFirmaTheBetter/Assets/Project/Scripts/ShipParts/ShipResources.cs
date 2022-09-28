using ShipParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EventSystem;
using ShipParts.Ship;
using ShipParts.Engines;
using ShipParts.Cores;

namespace ShipParts
{
    public class ShipResources : MonoBehaviour
    {
        private ShipBuilder shipBuilder;
        private ShipStats shipStats;
        private ShipHealth shipHealth;

        private void Awake()
        {
            shipBuilder = GetComponent<ShipBuilder>();
            shipStats = new ShipStats();
            shipHealth = new ShipHealth(shipBuilder.PlayerNumber, shipStats);
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


        }

        private void OnShipPartSelected(Part selectedPart, int playerNumber)
        {
            if (shipBuilder.PlayerNumber != playerNumber)
                return;

            if (selectedPart is Engine)
            {
                shipStats.UpdateStats(selectedPart.GetData() as EngineData);
            }

            if (selectedPart is Core)
            {
                shipStats.UpdateStats(selectedPart.GetData() as CoreData);
            }

            Channels.OnPlayerStatsChanged?.Invoke(shipBuilder, shipStats);
        }

        public ShipStats ShipStats => shipStats;
    }
}