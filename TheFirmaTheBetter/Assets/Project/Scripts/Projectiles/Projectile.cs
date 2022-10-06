
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

        private ObjectPool projectilesPool;

        private ImpactSpawner impactSpawner;

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
            currentLifeTime += Time.deltaTime;
            if (gameObject.activeSelf == false)
                return;

            if (currentLifeTime < lifeTime)
                return;

            projectilesPool.ReturnToPool(gameObject);
        }

        public void SetupProjectile(ObjectPool projectilesPool, float lifeTime)
        {
            this.projectilesPool = projectilesPool;
            this.lifeTime = lifeTime;
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
            //Debug.Log($"Hit a {collisionObject.GetType()}");
                Debug.Log($"Projectile hit something");
            if (collisionObject != null)
            {
                impactSpawner.SpawnImpactHitPrefab();
                collisionObject.HandleCollision(this,null);
                // ship.TakeDamage;
                //Debug.Log("A ship was hit");
            }
        }

        public void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats) where T1 : ICollidable { }

        public void DestroySelf()
        {
            projectilesPool.ReturnToPool(gameObject);
        }

        public int ProjectileDamage { get { return projectileDamage; } }
        public float ProjectileSpeed { get { return projectileSpeed; } }
        public float ArmingTime { get { return armingTime; } }
        public int AmountToSpawn { get { return amountToSpawn; } }

        public ProjectileData ProjectileData => projectileData;
    }
}