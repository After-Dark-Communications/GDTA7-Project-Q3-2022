using Projectiles;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProjectile : Projectile
{
    private Transform notarget;

    private Transform target;

    public Transform Notarget { get => notarget; set => notarget = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (target != null)
            return;

        target = other.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (target != null)
            return;

        target = other.transform;
    }

    private void Update()
    {
        if (target == null)
            return;

        transform.LookAt(target);
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }
}
