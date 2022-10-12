using Collisions;
using EventSystem;
using Projectiles;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour, ICollidable
{
    [SerializeField]
    private int damage;

    public void DestroySelf()
    {
    }

    public void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats) where T1 : ICollidable
    {
        if (objectThatHit is Projectile)
        {
            objectThatHit.DestroySelf();
            return;
        }
    }

    public int Damage { get { return damage; } }

}
