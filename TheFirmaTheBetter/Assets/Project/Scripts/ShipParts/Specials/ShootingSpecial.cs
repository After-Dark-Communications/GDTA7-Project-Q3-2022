using Projectiles;
using ShipParts.Specials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.Scripts.ShipParts.Specials
{
    public class ShootingSpecial : SpecialAbility
    {
        [SerializeField]
        private Transform shootingPoint;

        [SerializeField]
        private Projectile projectilePrefab;

        protected Projectile lastFiredProjectile;

        protected override void HandleSpecial()
        {
            lastFiredProjectile = Instantiate(projectilePrefab);
            Vector3 direction = shootingPoint.forward;

            lastFiredProjectile.transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);

            lastFiredProjectile.GetComponent<Rigidbody>().AddForce(direction * lastFiredProjectile.ProjectileSpeed, ForceMode.Impulse);
        }
    }
}
