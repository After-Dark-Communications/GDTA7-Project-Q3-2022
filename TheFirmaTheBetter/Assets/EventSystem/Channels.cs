using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Channels
{
    public static EventHandler<Manager> OnManagerInitialized;

    public static Action<int, float> OnHealthChanged;
    public static Action<int, float> OnFuelChanged;
    public static Action<int, int> OnAmmoChanged;

}
