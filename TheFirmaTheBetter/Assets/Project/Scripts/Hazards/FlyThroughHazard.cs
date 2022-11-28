using Collisions;
using EventSystem;
using ShipParts.Ship;
using System.Collections.Generic;
using UnityEngine;

namespace Hazards
{
    public class FlyThroughHazard : Hazard
    {
        private List<ICollidable> shipsCollidersThatEntered = new List<ICollidable>();

        private float timer;
        [SerializeField]
        private float timeToTakeDamage = 30;

        private void Start()
        {
            Channels.OnPlayerBecomesDeath += RemoveDeadPlayerFromTrigger;
        }

        private void OnDestroy()
        {
            Channels.OnPlayerBecomesDeath -= RemoveDeadPlayerFromTrigger;
        }
        private void Update()
        {
            if (shipsCollidersThatEntered.Count == 0)
                return;
            timer += Time.deltaTime;
            if (timer >= timeToTakeDamage)
            {
                DoDamageToAllShips(shipsCollidersThatEntered);
                timer = 0;
            }
        }

        private void DoDamageToAllShips(List<ICollidable> EnteredShips)
        {
            List<ICollidable> ships = EnteredShips;
            foreach (ICollidable collidableShip in ships)
            {
                collidableShip.HandleCollision(this, null);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            ICollidable collisionObject = other.GetComponentInParent<ICollidable>();
            AddCollidable(collisionObject);
        }
        private void OnTriggerExit(Collider other)
        {
            ICollidable collisionObject = other.GetComponentInParent<ICollidable>();
            RemoveCollidable(collisionObject);
        }

        private void RemoveDeadPlayerFromTrigger(ShipBuilder deadPlayer, int playerNumber)
        {
            ICollidable collidable = deadPlayer.GetComponent<ICollidable>();
            RemoveCollidable(collidable);
        }

        private void AddCollidable(ICollidable collidable)
        {
            if (collidable != null)
            {
                if (collidable is ShipCollision)
                {
                    if (shipsCollidersThatEntered.Contains(collidable))
                        return;
                    shipsCollidersThatEntered.Add(collidable);
                }
            }
        }

        private void RemoveCollidable(ICollidable collidable)
        {
            if (collidable != null)
            {
                if (collidable is ShipCollision)
                {
                    if (shipsCollidersThatEntered.Contains(collidable))
                        shipsCollidersThatEntered.Remove(collidable);
                }
            }
        }
    }
}