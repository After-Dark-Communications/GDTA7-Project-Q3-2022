using EventSystem;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : Pickup
{
    [SerializeField]
    [Range(10, 500)]
    private int energyIncreaseAmount;
    public override void PickUpAction(ShipBuilder shipBuilder)
    {
        Channels.OnRefillEnergy?.Invoke(shipBuilder.PlayerNumber, energyIncreaseAmount);
        base.PickUpAction(shipBuilder);
    }
}
