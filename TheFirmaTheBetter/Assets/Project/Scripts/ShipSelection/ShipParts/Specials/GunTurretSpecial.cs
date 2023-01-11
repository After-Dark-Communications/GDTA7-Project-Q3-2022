using Projectiles;
using ShipParts.Ship;
using UnityEngine;

namespace ShipParts.Specials
{
    public class GunTurretSpecial : SpecialAbility
    {
        [SerializeField]
        private Transform shootingPoint;

        [SerializeField]
        private Projectile projectilePrefab;

        protected override void HandleSpecial()
        {
            Projectile createdProjectile = Instantiate(projectilePrefab);

            SetNoTarget(createdProjectile);

            FireProjectile(createdProjectile);
        }

        private void FireProjectile(Projectile createdProjectile)
        {
            Vector3 direction = shootingPoint.forward;
            createdProjectile.transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);

            createdProjectile.GetComponent<Rigidbody>().AddForce(direction * createdProjectile.ProjectileSpeed, ForceMode.Impulse);
        }

        private DroneProjectile SetNoTarget(Projectile createdProjectile)
        {
            DroneProjectile drone = createdProjectile as DroneProjectile;
            drone.Notarget = shipRoot.GetComponentInChildren<ShipBuilder>();
            return drone;
        }
    }
}
