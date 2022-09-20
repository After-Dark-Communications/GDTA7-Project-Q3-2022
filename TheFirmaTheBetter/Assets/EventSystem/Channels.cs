using Parts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Channels
{
    public static Action<GameObject> OnZoneEntered;
    public static Action<Manager> OnManagerInitialized;
    public static Action<Part,int> OnShipPartSelected;
}
