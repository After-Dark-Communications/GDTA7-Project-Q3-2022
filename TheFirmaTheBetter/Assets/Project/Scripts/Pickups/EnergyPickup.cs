using EventSystem;
using ShipParts;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : Pickup
{
    [SerializeField]
    [Range(10, 100)]
    private int energyIncreaseAmount;
    public override void PickUpAction(ShipBuilder shipBuilder)
    {
        ShipResources resources = shipBuilder.GetComponent<ShipResources>();

        float amount =  (float)resources.ShipStats.EnergyCapacity / 100 * energyIncreaseAmount;

        Channels.OnRefillEnergy?.Invoke(shipBuilder.PlayerNumber, (int)amount);
        base.PickUpAction(shipBuilder);
    }
}
