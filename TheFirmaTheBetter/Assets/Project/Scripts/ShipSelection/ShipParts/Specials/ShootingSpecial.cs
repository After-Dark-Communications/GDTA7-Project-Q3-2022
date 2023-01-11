using Projectiles;
using ShipParts.Specials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShipParts.Specials
{
    public class ShootingSpecial : SpecialAbility
    {
        [SerializeField]
        private Transform shootingPoint;

        [SerializeField]
        private Projectile projectilePrefab;

        protected override void HandleSpecial()
        {
            Projectile createdProjectile = Instantiate(projectilePrefab);

            Vector3 direction = shootingPoint.forward;

            createdProjectile.transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);

            createdProjectile.GetComponent<Rigidbody>().AddForce(direction * createdProjectile.ProjectileSpeed, ForceMode.Impulse);
        }
    }
}
