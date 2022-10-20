using Projectiles;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class DroneProjectile : Projectile
    {
        private const float attackSpeed = 2;
        private const float lifeTime = 10f;

        [SerializeField]
        private Projectile projectilePrefab;
        [SerializeField]
        private GameObject explosionPrefab;
        [SerializeField]
        private Transform shootingPoint;

        private ShipBuilder noTarget;
        private ShipBuilder target;

        private float currentAttackSpeed = 0;
        private float currentDroneLifeTime = 0;

        public ShipBuilder Notarget { get => noTarget; set => noTarget = value; }

        private void OnTriggerEnter(Collider other)
        {
            ShipBuilder enteredShipBuilder = other.gameObject.GetComponentInParent<ShipBuilder>();

            if (enteredShipBuilder == null)
                return;

            if (enteredShipBuilder.PlayerNumber == noTarget.PlayerNumber)
                return;

            if (target != null)
                return;

            target = enteredShipBuilder;
        }

        private void OnTriggerStay(Collider other)
        {
            ShipBuilder enteredShipBuilder = other.gameObject.GetComponentInParent<ShipBuilder>();

            if (enteredShipBuilder == null)
                return;

            if (target != null)
                return;

            if (enteredShipBuilder.PlayerNumber == noTarget.PlayerNumber)
                return;

            target = enteredShipBuilder;
        }

        private void Update()
        {
            LookAtTarget();

            ShootTarget();

            currentDroneLifeTime += Time.deltaTime;

            if (currentDroneLifeTime < lifeTime)
                return;

            GameObject go = Instantiate(explosionPrefab);
            go.transform.position = transform.position;

            Destroy(gameObject);
        }

        private void LookAtTarget()
        {
            if (target == null)
                return;

            transform.LookAt(target.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            ShipBuilder exitShipBuilder = other.gameObject.GetComponentInParent<ShipBuilder>();

            if (exitShipBuilder == null)
                return;
            if (target == null)
                return;
            if (exitShipBuilder.PlayerNumber != target.PlayerNumber)
                return;

            target = null;
        }

        private void ShootTarget()
        {
            if (target == null)
            {
                currentAttackSpeed = 0;
                return;
            }

            if (currentAttackSpeed < attackSpeed)
            {
                currentAttackSpeed += Time.deltaTime;
                return;
            }

            currentAttackSpeed = 0;
            Projectile createdProjectile = Instantiate(projectilePrefab);
            Vector3 direction = shootingPoint.forward;

            createdProjectile.transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);

            createdProjectile.GetComponent<Rigidbody>().AddForce(direction * createdProjectile.ProjectileSpeed, ForceMode.Impulse);

        }
    }
}