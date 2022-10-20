
using Collisions;
using Pooling;
using ShipParts.Ship;
using System.Collections;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(ImpactSpawner))]
    public class Projectile : MonoBehaviour, IObjectPoolItem, ICollidable
    {
        [SerializeField]
        private ProjectileData projectileData;

        private int projectileDamage;
        private int amountToSpawn;

        private float armingTime;
        private float projectileSpeed;
        private float currentLifeTime;
        private float lifeTime;

        [SerializeField]
        private int playerIndex;

        private ObjectPool projectilesPool;

        private ImpactSpawner impactSpawner;

        private int firerId;

        private void OnEnable()
        {
            projectileDamage = projectileData.Damage;
            projectileSpeed = projectileData.ProjectileSpeed;
            armingTime = projectileData.ArmingTime;
            amountToSpawn = projectileData.AmountToSpawn;
            currentLifeTime = 0;
            impactSpawner = GetComponent<ImpactSpawner>();
        }

        private void Update()
        {
            if (projectilesPool == null)
                return;
            currentLifeTime += Time.deltaTime;
            if (gameObject.activeSelf == false)
                return;

            if (currentLifeTime < lifeTime)
                return;

            impactSpawner.SpawnImpactHitPrefab();
            projectilesPool.ReturnToPool(gameObject);
        }

        public void SetupProjectile(ObjectPool projectilesPool, float lifeTime, int playerIndex)
        {
            this.projectilesPool = projectilesPool;
            this.lifeTime = lifeTime;
            this.playerIndex = playerIndex;
        }

        public void ResetPoolItem()
        {
            Rigidbody projectileRigidbody = GetComponent<Rigidbody>();
            projectileRigidbody.velocity = Vector3.zero;
            projectileRigidbody.angularVelocity = Vector3.zero;
        }

        public void SpawnObjectOnImpact()
        {
            GameObject impactParticleObject = Instantiate(projectileData.SpawnedObjectOnImpact, transform.position, transform.rotation);

            ParticleSystem impactParticleSystem = impactParticleObject.GetComponent<ParticleSystem>();
            float impactTime = 0;
            if (impactParticleSystem != null)
            {
                impactTime = impactParticleSystem.main.duration;
            }

            Destroy(impactParticleObject, impactTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            ICollidable collisionObject = other.GetComponentInParent<ICollidable>();

            if (collisionObject != null)
            { 
                if (collisionObject is Projectile)
                    return;
                
                if(collisionObject is ShipCollision)
                {
                    ShipCollision collidedShip = collisionObject as ShipCollision;
                    ShipBuilder builder = collidedShip.GetComponent<ShipBuilder>();

                    if (builder.PlayerNumber == firerId)
                    {
                        return;
                    }
                }    

                impactSpawner.SpawnImpactHitPrefab();
                collisionObject.HandleCollision(this, null);
            }
        }

        public void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats) where T1 : ICollidable { }

        public void DestroySelf()
        {
            if (projectilesPool == null)
                return;
            projectilesPool.ReturnToPool(gameObject);
        }

        public int ProjectileDamage { get { return projectileDamage; } }
        public float ProjectileSpeed { get { return projectileSpeed; } }
        public float ArmingTime { get { return armingTime; } }
        public int AmountToSpawn { get { return amountToSpawn; } }
        public int PlayerIndex { get { return playerIndex; } }
        public int FirerId { get { return firerId; } set { firerId = value; } }
        public ProjectileData ProjectileData => projectileData;
    }
}