using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts
{
    [RequireComponent(typeof(Animator))]
    public class ShipAnimationManager : MonoBehaviour
    {
        private const string trigger = "ShipIdle";
        private Animator shipAnimator;
        private ShipBuilder shipBuilder;

        private void Awake()
        {
            shipAnimator = GetComponent<Animator>();
            shipAnimator.SetTrigger(trigger);

            shipBuilder = GetComponent<ShipBuilder>();
        }
        private void OnEnable()
        {
            Channels.OnShipCompleted += OnShipCompleted;

        }

        private void OnShipCompleted(ShipBuilder obj)
        {
            //throw new NotImplementedException();
            //Debug.Log($"Completed");
            if (obj.PlayerNumber != shipBuilder.PlayerNumber)
                return;

            shipAnimator.SetTrigger(trigger);
            shipAnimator.enabled = false;
            this.enabled = false;

        }

        private void OnDisable()
        {
            Channels.OnShipCompleted -= OnShipCompleted;
        }
    }
}