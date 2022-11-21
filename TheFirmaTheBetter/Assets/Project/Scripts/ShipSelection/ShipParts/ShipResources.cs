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
        private ShipStats hoveredStats;
        private ShipHealth shipHealth;
        private ShipEnergy shipEnergy;

        private void Awake()
        {
            shipBuilder = GetComponent<ShipBuilder>();
            shipStats = new ShipStats();
            hoveredStats = new ShipStats();
            shipHealth = new ShipHealth(shipBuilder.PlayerNumber, shipStats);
            shipEnergy = new ShipEnergy(shipBuilder.PlayerNumber, shipStats);

            Channels.OnShipPartHovered += OnShipStatHovered;
            Channels.OnShipPartSelected += OnShipPartSelected;
            Channels.OnShipCompleted += OnShipCompleted;
            Channels.OnPlayerRespawned += OnPlayerRespawned;
            shipHealth.Subscribe();
            shipEnergy.Subscribe();
        }

        private void OnDestroy()
        {
            shipHealth.Unsubscribe();
            shipEnergy.Unsubscribe();
            Channels.OnShipPartHovered -= OnShipStatHovered;
            Channels.OnShipPartSelected -= OnShipPartSelected;
            Channels.OnShipCompleted -= OnShipCompleted;
            Channels.OnPlayerRespawned -= OnPlayerRespawned;
        }

        private void OnShipCompleted(ShipBuilder completedShipBuilder)
        {
            if (shipBuilder.PlayerNumber != completedShipBuilder.PlayerNumber)
                return;

            ResetRescources();

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

            UpdateShipStats(shipStats, selectedPart);

            Channels.OnDisplayabeStatsChanged?.Invoke(playerNumber, selectedPart, shipStats, shipStats);
        }


        private void OnShipStatHovered(Part hoveredPart, int playerNumber)
        {
            if (shipBuilder.PlayerNumber != playerNumber)
                return;

            UpdateShipStats(hoveredStats, hoveredPart);

            Channels.OnDisplayabeStatsChanged?.Invoke(playerNumber, hoveredPart, shipStats, hoveredStats);
        }

        private void UpdateShipStats(ShipStats stats, Part shipPart)
        {
            if (shipPart is Engine)
            {
                stats.UpdateStats(shipPart.GetData() as EngineData, shipBuilder);
            }
            else if (shipPart is Core)
            {
                stats.UpdateStats(shipPart.GetData() as CoreData, shipBuilder);
            }
            else if (shipPart is Weapon)
            {
                stats.UpdateStats(shipPart.GetData() as WeaponData);
            }
            else if (shipPart is SpecialAbility)
            {
                stats.UpdateStats(shipPart.GetData() as SpecialData);
            }
        }

        private void OnPlayerRespawned(int playerNumber)
        {
            if (shipBuilder.PlayerNumber != playerNumber)
                return;

            ResetRescources();
        }

        public void ResetRescources()
        {
            shipHealth.ResetHealth(shipStats);
            shipEnergy.ResetEnergy(shipStats);
        }

        public float CurrentEnergyAmount => shipEnergy.CurrentEnergyAmount;

        public ShipStats ShipStats => shipStats;

    } 
}