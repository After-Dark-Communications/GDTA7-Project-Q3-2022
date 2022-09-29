using Assets.Project.Scripts.ShipParts;
using Parts;
using ShipParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            shipStats.UpdateStats(selectedPart.GetData() as EngineData);
        }

        if (selectedPart is Core)
        {
            shipStats.UpdateStats(selectedPart.GetData() as CoreData);
        }
        if (selectedPart is Weapon)
        {
            shipStats.UpdateStats(selectedPart.GetData() as WeaponData);
        }

        Channels.OnPlayerStatsChanged?.Invoke(shipBuilder, shipStats);
    }

    public int CurrentEnergyAmount => shipEnergy.CurrentEnergyAmount;

    public ShipStats ShipStats => shipStats;
}
