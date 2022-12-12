using Collisions;
using EventSystem;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDepletionWall : Wall
{
    public override void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats)
    {
        base.HandleCollision(objectThatHit, shipStats);

        if (objectThatHit is ShipCollision)
        {
            Channels.OnEnergyUsed?.Invoke(shipStats.PlayerNumber, shipStats.EnergyCapacity);
        }
    }
}
