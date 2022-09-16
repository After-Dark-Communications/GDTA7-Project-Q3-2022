using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoZone : Zone
{
    /// <summary>
    /// Triggers a zone's effect
    /// </summary>
    /// <param name="obj">The player currently in the zone</param>
    public override void TriggerEffect(GameObject obj)
    {
        Debug.Log("In trigger");
        // Todo: refill ship ammo
    }
}
