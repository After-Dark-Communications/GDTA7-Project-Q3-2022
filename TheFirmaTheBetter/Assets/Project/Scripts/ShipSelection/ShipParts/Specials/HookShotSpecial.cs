using Assets.Project.Scripts.ShipParts.Specials;
using Projectiles;
using ShipParts.Ship;
using UnityEngine;

namespace ShipParts.Specials
{
    public class HookShotSpecial : SpecialAbility
    {
        [SerializeField]
        private Transform shootingPoint;

        [SerializeField]
        private GameObject projectilePrefab;

        private HookShotProjectile hookShotProjectile;

        private void Awake()
        {
            hookShotProjectile = projectilePrefab.GetComponentInChildren<HookShotProjectile>();
        }

        protected override void HandleSpecial()
        {
            GameObject createdProjectile = Instantiate(projectilePrefab);
            HookShotProjectile projectile = createdProjectile.GetComponentInChildren<HookShotProjectile>();
            projectile.SetJointFirer(shipRigidBody);
            Vector3 direction = shootingPoint.forward;

            createdProjectile.transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);

            Rigidbody body = createdProjectile.GetComponent<Rigidbody>();
            body.AddForce(direction * hookShotProjectile.ProjectileData.ProjectileSpeed, ForceMode.Impulse);
        }
    }
}
