using Assets.Project.Scripts.ShipParts.Specials;
using Projectiles;
using UnityEngine;

namespace ShipParts.Specials
{
    public class GunTurretSpecial : ShootingSpecial
    {
        protected override void HandleSpecial()
        {
            base.HandleSpecial();
            DroneProjectile drone = lastFiredProjectile as DroneProjectile;

            drone.Notarget = shipRoot;
        }
    }
}
