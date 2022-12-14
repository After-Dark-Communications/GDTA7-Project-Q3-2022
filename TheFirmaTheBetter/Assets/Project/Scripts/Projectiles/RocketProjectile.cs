using Collisions;
using EventSystem;
using Projectiles;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class RocketProjectile : Projectile
    {
        [SerializeField]
        private float maxLifeTime;

        private float currentTime = 0;

        private float currentArmingTime = 0;

        private bool armed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (armed == false)
                return;

            ShipBuilder otherBuilder = other.GetComponentInParent<ShipBuilder>();

            if (otherBuilder == null)
            {
                ICollidable collidable = other.GetComponentInParent<ICollidable>();

                if (collidable != null)
                    Explode();

                return;
            }

            Explode();
            Channels.OnPlayerTakeDamage?.Invoke(otherBuilder, ProjectileData.Damage, PlayerIndex);
        }

        private void Update()
        {
            ArmRocket();

            currentTime += Time.deltaTime;

            if (currentTime < maxLifeTime)
                return;

            Explode();
        }

        private void Explode()
        {
            GameObject spawned = Instantiate(this.ProjectileData.SpawnedObjectOnImpact);
            spawned.transform.position = transform.position;

            Destroy(gameObject);
        }

        private void ArmRocket()
        {
            if (armed)
                return;

            currentArmingTime += Time.deltaTime;

            if (currentArmingTime < ProjectileData.ArmingTime)
                return;

            armed = true;
        }
    }
}