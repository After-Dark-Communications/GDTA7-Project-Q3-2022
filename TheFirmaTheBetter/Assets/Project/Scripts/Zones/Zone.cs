using EventSystem;
using Zones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zone : MonoBehaviour
{

    public virtual void OnTriggerEnter(Collider other)
    {
        IHaveZoneInteraction zoneInteractor = other.GetComponentInParent<IHaveZoneInteraction>();

        if (zoneInteractor == null)
            return;

        zoneInteractor.HandleZoneEnterInteraction(this);

        Channels.OnZoneEntered?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        IHaveZoneInteraction zoneInteractor = other.GetComponentInParent<IHaveZoneInteraction>();

        if (zoneInteractor == null)
            return;

        zoneInteractor.HandleZoneExitInteraction(this);
    }

    /// <summary>
    /// Triggers a zone's effect
    /// </summary>
    /// <param name="obj">The player currently in the zone</param>
    public abstract void TriggerEffect(GameObject obj);
}
