using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.Scripts.Collision
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
