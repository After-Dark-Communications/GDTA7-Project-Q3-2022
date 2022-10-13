using Collisions;
using Projectiles;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyThroughHazard : Hazard
{
    private List<ICollidable> shipsCollidersThatEntered = new List<ICollidable>();

    private float timer;
    [SerializeField]
    private float timeToTakeDamage = 30;


    private void Update()
    {
        if (shipsCollidersThatEntered.Count == 0)
            return;
        timer += Time.deltaTime;
        if(timer >= timeToTakeDamage)
        {
            doDamageToAllShips(shipsCollidersThatEntered);
            timer = 0;
        }
    }

    void doDamageToAllShips(List<ICollidable> ships)
    {
        foreach (ICollidable collidableShip in ships)
        {
            collidableShip.HandleCollision(this, null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollidable collisionObject = other.GetComponentInParent<ICollidable>();
        if (collisionObject != null)
        {
            if (collisionObject is ShipCollision)
            {
                if (shipsCollidersThatEntered.Contains(collisionObject))
                    return;
                shipsCollidersThatEntered.Add(collisionObject);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ICollidable collisionObject = other.GetComponentInParent<ICollidable>();
        if (collisionObject != null)
        {
            if (collisionObject is ShipCollision)
            {
                if (shipsCollidersThatEntered.Contains(collisionObject))
                    shipsCollidersThatEntered.Remove(collisionObject);
            }

        }
    }
}
