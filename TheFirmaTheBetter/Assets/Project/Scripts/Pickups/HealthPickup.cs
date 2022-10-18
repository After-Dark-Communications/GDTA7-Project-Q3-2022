using EventSystem;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField]
    [Range(10,500)]
    private int healthIncreaseAmount;

    [SerializeField]
    private GameObject particleExplosion;

    public override void PickUpAction(ShipBuilder shipBuilder)
    {
        Channels.OnPlayerHealed?.Invoke(healthIncreaseAmount, shipBuilder.PlayerNumber);
       // TriggerExplosion();
        base.PickUpAction(shipBuilder);
    }

    private void TriggerExplosion()
    {
        particleExplosion.GetComponent<ParticleSystem>().Play();
    }
}
