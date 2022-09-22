using Assets.EventSystem;
using Parts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public static class Channels
{
    public static InputChannel Input = new InputChannel();

    public static Action<GameObject> OnZoneEntered;
    public static Action<Manager> OnManagerInitialized;
    public static Action<Part,int> OnShipPartSelected;
    public static Action<ShipBuilder> OnShipCompleted;
    public static Action<int, InputDevice> OnPlayerJoined;

    public static Action OnEveryPlayerReady;

    public static Action<GameObject, int> OnPlayerSpawned;

    public static Action<int, float> OnHealthChanged;
    public static Action<int, float> OnFuelChanged;
    public static Action<int, float> OnAmmoChanged;

}
