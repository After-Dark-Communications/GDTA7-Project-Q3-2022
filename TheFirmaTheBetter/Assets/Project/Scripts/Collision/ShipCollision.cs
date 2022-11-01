using EventSystem;
using Hazards;
using Projectiles;
using ShipParts;
using ShipParts.Ship;
using System;
using UnityEngine;

namespace Collisions
{
    public class ShipCollision : MonoBehaviour, ICollidable
    {
        private ShipBuilder shipBuilder;
        private Rigidbody rigidbody;
        private ShipStats shipStats;

        private const float _collisionWeightImpactMultiplier = 0.5f, _bumpForceClamp = 25f;//adjust to get different feel (used to prevent "random" spikes to 500+ bumpforce)

        public ShipBuilder ShipBuilder { get => shipBuilder;}

        private void Awake()
        {
            shipBuilder = GetComponent<ShipBuilder>();
            Channels.OnPlayerSpawned += ShipSpawned;

        }

        private void OnDisable()
        {
            Channels.OnPlayerSpawned -= ShipSpawned;
        }

        private void ShipSpawned(GameObject SpawnedShip, int playerIndex)
        {
            if (playerIndex == shipBuilder.PlayerNumber)
            {
                rigidbody = GetComponentInParent<Rigidbody>();
                shipStats = GetComponent<ShipResources>()?.ShipStats;
            }
        }

        public void DestroySelf()
        {

        }

        public void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats) where T1 : ICollidable
       {//objectThatHit and shipStats are BOTH from the object that hit this one

            if (objectThatHit is Projectile)
            {
                HandleHitByProjectile(objectThatHit as Projectile);
            }

            if (objectThatHit is ShipCollision)
            {
                HandleHitByOtherShip(objectThatHit as ShipCollision, shipStats);
            }

            if (objectThatHit is Hazard)
            {
                HandleHitByHazard(objectThatHit as Hazard);
            }
        }

        private void HandleHitByOtherShip(ShipCollision shipCollision, ShipStats collisionShipStats)
        {
            if (shipCollision != null)
            {
                Debug.Log($"{transform.parent.name} got hit by {shipCollision.transform.parent.name}");
                Rigidbody otherRigidbody = shipCollision.gameObject.GetComponentInParent<Rigidbody>();
                if (otherRigidbody)
                {
                    //apply force to both ships based on position delta and respective weights
                    Vector3 bumpDir = transform.position - shipCollision.transform.position;

                    float WeightDif = getWeightDif();

                    float totalBumpForce = otherRigidbody.velocity.magnitude + (WeightDif * _collisionWeightImpactMultiplier);
                    //Debug.DrawLine(transform.position, transform.position + (bumpDir.normalized * totalBumpForce), Color.red, 2f);
                    rigidbody.AddForce(bumpDir.normalized * Mathf.Clamp(totalBumpForce, 0, _bumpForceClamp), ForceMode.Impulse);//issue, some bumps are too strong
                    //Debug.Log($"[{shipCollision.name}]BumpForce: {Mathf.Clamp(totalBumpForce, 0, _bumpForceClamp)} (unclamped: {totalBumpForce})");
                }
            }

            float getWeightDif()
            {
                float myWeight = shipStats.TotalWeight + shipStats.SumTotalWeightModifier;
                float otherWeight = collisionShipStats.TotalWeight + collisionShipStats.SumTotalWeightModifier;
                return Mathf.Clamp(otherWeight - myWeight, 0, Mathf.Infinity);
            }
        }

        private void HandleHitByProjectile(Projectile projectileThatHit)
        {
            //Debug.Log($"took damage! ({Channels.OnPlayerTakeDamage?.GetInvocationList().Length})called");
            Channels.OnPlayerTakeDamage?.Invoke(shipBuilder, projectileThatHit.ProjectileDamage, projectileThatHit.PlayerIndex);
            Channels.OnPlayerHit?.Invoke();
            projectileThatHit.DestroySelf();
        }

        private void HandleHitByHazard(Hazard hazardThatHit)
        {
            //Since none of the players shot the current shipbuilder, we make playerindex -1, since it can't be null.
            Channels.OnPlayerTakeDamage?.Invoke(shipBuilder, hazardThatHit.Damage, -1);
            Channels.OnPlayerHit?.Invoke();
        }
    }
}