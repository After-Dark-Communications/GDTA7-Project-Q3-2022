using EventSystem;
using Pooling;
using Projectiles;
using ShipParts.Ship;
using ShipSelection.ShipBuilders.ConnectionPoints;
using System;
using System.Collections;
using UnityEngine;
using Util;

namespace ShipParts.Weapons
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

        private ButtonStates currentFireButtonState = ButtonStates.NONE;

        private void Awake()
        {
            CalculateHighestAndLowest();
        }

        protected override void Setup()
        {
            shipResources = GetComponentInParent<ShipResources>();
            playerNumber = GetComponentInParent<ShipBuilder>().PlayerNumber;

            Channels.OnChangeFireMode += OnChangeFireMode;
            canShoot = true;

            if (rootInputHandler != null)
            {
                rootInputHandler.OnPlayerAim.AddListener(AimWeapon);
                rootInputHandler.OnPlayerShoot.AddListener(ShootWeapon);
            }

            if (weaponData != null)
            {
                projectilesPool = new ObjectPool(weaponData.ProjectilePrefab.gameObject, 10);
            }
        }

        private void Update()
        {
            if (currentFireButtonState == ButtonStates.CANCELED || currentFireButtonState == ButtonStates.NONE)
                return;

            if (currentFireButtonState == ButtonStates.STARTED || currentFireButtonState == ButtonStates.PERFORMED)
                FireWeapon();
        }

        private void OnChangeFireMode(bool newValue)
        {
            canShoot = newValue;
        }

        private void ShootWeapon(ButtonStates state)
        {
            currentFireButtonState = state;
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

            if (lastShootTime + 1 / weaponData.FireRate >= Time.time)
                return;

            lastShootTime = Time.time;

            if (weaponData.EnergyCost > shipResources.CurrentEnergyAmount)
            {
                Channels.OnEnergyEmpty?.Invoke();
                return;
            }

            for (int i = 0; i < weaponData.AmountOfBullets; i++)
            {
                foreach (Transform point in projectileStartingPoints)
                {
                    GameObject projectileObject;
                    Vector3 direction;
                    Projectile projectile;

                    GetNewProjectileFromPool(point, out projectileObject, out direction, out projectile);

                    FireProjectile(projectileObject, direction, projectile);

                    ReturnProjectileToPoolAfterTime(projectile);

                    Channels.OnWeaponFired?.Invoke(weaponData.WeaponFireEvent);
                }
            }



            void ReturnProjectileToPoolAfterTime(Projectile projectile)
            {
                float projectileLifetime = weaponData.Range / projectile.ProjectileSpeed;
                projectile.SetupProjectile(projectilesPool, projectileLifetime, playerNumber);
            }

            void FireProjectile(GameObject projectileObject, Vector3 direction, Projectile projectile)
            {
                projectileObject.GetComponent<Rigidbody>().AddForce(direction * projectile.ProjectileSpeed, ForceMode.Impulse);
                Channels.OnEnergyUsed?.Invoke(playerNumber, weaponData.EnergyCost);
            }

            void GetNewProjectileFromPool(Transform point, out GameObject projectileObject, out Vector3 direction, out Projectile projectile)
            {
                projectileObject = projectilesPool.RentFromPool();
                projectileObject.transform.SetPositionAndRotation(point.position, point.rotation);

                direction = GetShootDirection(point, weaponData.SideSpreadAngle);
                projectile = projectileObject.GetComponent<Projectile>();
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

        public override PartData GetData()
        {
            return weaponData;
        }

        protected override void CalculateHighestAndLowest()
        {
            base.CalculateHighestAndLowest();
            StatBoundries.SetHighestAndLowest(weaponData.FireRate, ref StatBoundries.FIRE_RATE_BOUNDRIES);
            StatBoundries.SetHighestAndLowest(weaponData.Range, ref StatBoundries.RANGE_BOUNDRIES);
            StatBoundries.SetHighestAndLowest(weaponData.EnergyCost, ref StatBoundries.ENERGY_COST_BOUNDRIES);
            StatBoundries.SetHighestAndLowest(weaponData.DPS, ref StatBoundries.DPS_BOUNDRIES);
        }
    }
}