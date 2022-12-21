using Collisions;
using EventSystem;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zones;

public class EnergyDepletionWall : Zone
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Destroy(gameObject);
    }

    public override void TriggerEffect(GameObject obj)
    {

    }
}
