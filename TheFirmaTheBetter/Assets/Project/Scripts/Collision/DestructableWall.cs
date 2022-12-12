using Projectiles;
using ShipParts.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Collisions
{
    public class DestructableWall : Wall
    {
        public override void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats)
        {
            base.HandleCollision(objectThatHit, shipStats);

            if (objectThatHit is Projectile)
            {
                Destroy(gameObject);
            }
        }
    }
}
