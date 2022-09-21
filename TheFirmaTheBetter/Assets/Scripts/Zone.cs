using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zone : MonoBehaviour
{

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerEffect(other.gameObject);
            Channels.OnZoneEntered?.Invoke(other.gameObject);
        }
    }

    /// <summary>
    /// Triggers a zone's effect
    /// </summary>
    /// <param name="obj">The player currently in the zone</param>
    public abstract void TriggerEffect(GameObject obj);
}
