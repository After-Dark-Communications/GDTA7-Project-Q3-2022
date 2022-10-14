using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipParts.Ship;
using EventSystem;

public class EngineAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponentInChildren<ShipBuilder>())
            {
                Channels.OnEnableEngine?.Invoke();
            }
            else
            {
                Channels.OnDisableEngine?.Invoke();
            }
        }
    }
}
