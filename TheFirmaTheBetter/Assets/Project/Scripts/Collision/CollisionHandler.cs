using Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Collisions
{
    public static class CollisionHandler
    {
        public static void HandleCollision<T>(T objectThatHit, Wall wall) where T : ICollidable
        {
            if (objectThatHit is not Projectile)
                return;

            objectThatHit.DestroySelf();
        }
    }
}
