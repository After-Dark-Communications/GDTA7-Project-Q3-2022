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

        private ShipResources shipResources;
        private int playerNumber;
        private bool canShoot;

        protected override void Setup()
        {
            shipResources = GetComponentInParent<ShipResources>();
            playerNumber = GetComponentInParent<ShipBuilder>().PlayerNumber;

            Channels.OnChangeFireMode += OnChangeFireMode;
            canShoot = true;

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

        private void OnChangeFireMode(bool newValue)
        {
            canShoot = newValue;
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
            if (canShoot == false)
                return;

            for (int i = 0; i < weaponData.AmountOfBullets; i++)
            {
                if (weaponData.EnergyCost > shipResources.CurrentEnergyAmount)
                {
                    Channels.OnEnergyEmpty.Invoke();
                    return;
                }

                if (lastShootTime + 1 / weaponData.FireRate >= Time.time)
                    return;

                foreach (Transform point in projectileStartingPoints)
                {
                    GameObject projectileObject;
                    Vector3 direction;
                    Projectile projectile;

                    GetNewProjectileFromPool(point, out projectileObject, out direction, out projectile);

                    FireProjectile(projectileObject, direction, projectile);

                    ReturnProjectileToPoolAfterTime(projectile);
                }
            }

            lastShootTime = Time.time;


            void ReturnProjectileToPoolAfterTime(Projectile projectile)
            {
                float projectileLifetime = weaponData.Range / projectile.ProjectileSpeed;
                StartCoroutine(ArmProjectile(projectile));
                projectile.SetupProjectile(projectilesPool, projectileLifetime);
            }

            void FireProjectile(GameObject projectileObject, Vector3 direction, Projectile projectile)
            {
                projectileObject.GetComponent<Rigidbody>().AddForce(direction * projectile.ProjectileSpeed, ForceMode.Impulse);
                Channels.OnEnergyUsed.Invoke(playerNumber, weaponData.EnergyCost);
            }

            void GetNewProjectileFromPool(Transform point, out GameObject projectileObject, out Vector3 direction, out Projectile projectile)
            {
                projectileObject = projectilesPool.RentFromPool();
                projectileObject.transform.SetPositionAndRotation(point.position, point.rotation);

                direction = GetShootDirection(point, weaponData.SideSpreadAngle);
                projectile = projectileObject.GetComponent<Projectile>();
            }
        }

        private IEnumerator ArmProjectile(Projectile projectile)
        {
            Collider col = projectile.GetComponent<Collider>();
            col.enabled = false;
            yield return new WaitForSeconds(projectile.ArmingTime);
            col.enabled = true;
        }

        private Vector3 GetShootDirection(Transform shootingPoint, float sideSpreadAngle)
        {
            Vector3 direction = shootingPoint.forward;

            Quaternion rotatiton = Quaternion.AngleAxis(UnityEngine.Random.Range(-sideSpreadAngle, sideSpreadAngle), Vector3.up);
            direction = rotatiton * direction;

            direction.Normalize();

            return direction;
        }

        public override PartData GetData()
        {
            return weaponData;
        }
    }
}