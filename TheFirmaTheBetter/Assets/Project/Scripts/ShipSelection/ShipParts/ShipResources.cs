using EventSystem;
using ShipParts;
using ShipParts.Cores;
using ShipParts.Engines;
using ShipParts.Ship;
using ShipParts.Specials;
using ShipParts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShipParts
{
    public class ShipResources : MonoBehaviour

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

        private void OnDisable()
        {
            shipHealth.Unsubscribe();
            shipEnergy.Unsubscribe();
            Channels.OnShipPartSelected -= OnShipPartSelected;
            Channels.OnShipCompleted -= OnShipCompleted;
        }

        private void OnShipCompleted(ShipBuilder completedShipBuilder)
        {
            if (shipBuilder.PlayerNumber != completedShipBuilder.PlayerNumber)
                return;

            shipHealth.UpdateHealth(shipStats);
            shipEnergy.UpdateEnergy(shipStats);

            UpdateTotalWeight();
        }

        private void UpdateTotalWeight()
        {
            int[] partWeights = new int[shipBuilder.SelectedParts.Count];
            for (int i = 0; i < partWeights.Length; i++)
            {
                partWeights[i] = shipBuilder.SelectedParts[i].GetData().PartWeight;
            }
            shipStats.SetTotalWeight(shipBuilder, partWeights);
        }

        private void OnShipPartSelected(Part selectedPart, int playerNumber)
        {
            if (shipBuilder.PlayerNumber != playerNumber)
                return;

            UpdateTotalWeight();

            if (selectedPart is Engine)
            {
                shipStats.UpdateStats(selectedPart.GetData() as EngineData, shipBuilder);
            }

            if (selectedPart is Core)
            {
                shipStats.UpdateStats(selectedPart.GetData() as CoreData, shipBuilder);
            }
            if (selectedPart is Weapon)
            {
                shipStats.UpdateStats(selectedPart.GetData() as WeaponData);
            }
            if (selectedPart is SpecialAbility)
            {
                shipStats.UpdateStats(selectedPart.GetData() as SpecialData);
            }

            Channels.OnPlayerStatsChanged?.Invoke(shipBuilder, shipStats);
        }

        public int CurrentEnergyAmount => shipEnergy.CurrentEnergyAmount;

        public ShipStats ShipStats => shipStats;

    } 
}