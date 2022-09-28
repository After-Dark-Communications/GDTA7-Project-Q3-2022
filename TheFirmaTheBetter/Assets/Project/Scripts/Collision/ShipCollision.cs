using EventSystem;
using Projectiles;
using ShipParts.Ship;
using System;
using UnityEngine;

namespace Collisions
{
    public class ShipCollision : MonoBehaviour, ICollidable
    {
        private ShipBuilder shipBuilder;

        private void Awake()
        {
            shipBuilder = GetComponent<ShipBuilder>();
        }

        public void DestroySelf()
        {

        }

        public void HandleCollision<T1>(T1 objectThatHit) where T1 : ICollidable
        {
            if (objectThatHit is Projectile)
            {
                HandleHitByProjectile(objectThatHit as Projectile);
            }

            if (objectThatHit is ShipCollision)
            {
                HandleHitByOtherShip(objectThatHit as ShipCollision);
            }
        }

        private void HandleHitByOtherShip(ShipCollision shipCollision)
        {
            //TODO: handle getting hit by other ship
        }

        private void HandleHitByProjectile(Projectile projectileThatHit)
        {
            //Debug.Log($"took damage! ({Channels.OnPlayerTakeDamage?.GetInvocationList().Length})called");
            Channels.OnPlayerTakeDamage?.Invoke(shipBuilder, projectileThatHit.ProjectileDamage);

            projectileThatHit.DestroySelf();
        }
    }
}