using EventSystem;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField]
    private int healthIncreaseAmount = 0;
    public override void PickUpAction(ShipBuilder shipBuilder)
    {
        Channels.OnPlayerHealed?.Invoke(healthIncreaseAmount, shipBuilder.PlayerNumber);
        base.PickUpAction(shipBuilder);
    }
}
