using ShipSelection.ShipBuilder.ConnectionPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Parts
{
    [AddComponentMenu("Parts/Weapon")]
    public class Weapon : Part
    {
        public override string PartCategoryName => "Weapon";

        [SerializeField]
        private WeaponData weaponData;

        [SerializeField]
        private Transform[] projectileStartingPoints;

        private ObjectPool projectilesPool;
        private float lastShootTime;

        private void Start()
        {

        }

        protected override void Setup()
        {
            if (RootInputHandler != null)
            {
                RootInputHandler.OnPlayerAim.AddListener(AimWeapon);
                RootInputHandler.OnPlayerShoot.AddListener(ShootWeapon);
            }

            if (weaponData != null)
            {
                projectilesPool = new ObjectPool(weaponData.ProjectilePrefab, 10);
            }
        }

        private void ShootWeapon(ButtonStates state)
        {
            FireWeapon();
        }

        private void AimWeapon(float rotation)
        {
            //rotation is for left/right
            //Debug.LogError("AimWeapon is not implemented!");
        }

        public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
        {
            if (connectionPoint is WeaponConnectionPoint)
                return true;

            return false;
        }

        public override bool IsMyType(Part part)
        {
            if (part is Weapon)
                return true;

            return false;
        }

        public void FireWeapon()
        {
            if (lastShootTime + 1 / weaponData.FireRate < Time.time)
            {
                foreach (Transform point in projectileStartingPoints)
                {
                    // Get projectile from pool
                    GameObject projectileObject = projectilesPool.RentFromPool();
                    projectileObject.transform.SetPositionAndRotation(point.position, point.rotation);

                    Vector3 direction = GetShootDirection(point, weaponData.SideSpreadAngle);

                    Projectile projectile = projectileObject.GetComponent<Projectile>();

                    // Fire projectile
                    projectileObject.GetComponent<Rigidbody>().AddForce(direction * projectile.ProjectileSpeed, ForceMode.Impulse);

                    // Return projectile after time
                    float projectileLifetime = weaponData.Range / projectile.ProjectileSpeed;
                    StartCoroutine(ReturnProjectile(projectileObject, projectileLifetime));
                }

                lastShootTime = Time.time;
            }
        }

        private Vector3 GetShootDirection(Transform shootingPoint, float sideSpreadAngle)
        {
            Vector3 direction = shootingPoint.forward;

            Quaternion rotatiton = Quaternion.AngleAxis(UnityEngine.Random.Range(-sideSpreadAngle, sideSpreadAngle), Vector3.up);
            direction = rotatiton * direction;

            direction.Normalize();

            return direction;
        }

        IEnumerator ReturnProjectile(GameObject projectile, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            projectilesPool.ReturnToPool(projectile);
        }
    }
}