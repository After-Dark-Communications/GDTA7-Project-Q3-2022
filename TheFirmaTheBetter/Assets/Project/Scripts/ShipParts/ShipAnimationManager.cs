using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ShipAnimationManager : MonoBehaviour
{
    private const string trigger = "ShipIdle";
    private Animator shipAnimator;
    private void Awake()
    {
        shipAnimator = GetComponent<Animator>();
        shipAnimator.SetTrigger(trigger);
    }
    private void OnEnable()
    {
        Channels.OnShipCompleted += OnShipCompleted;
    }

    private void OnShipCompleted(ShipBuilder obj)
    {
        //throw new NotImplementedException();
        Debug.Log($"Completed");
        shipAnimator.SetTrigger(trigger);
        
    }

    private void OnDisable()
    {
        Channels.OnShipCompleted -= OnShipCompleted;
    }
}
