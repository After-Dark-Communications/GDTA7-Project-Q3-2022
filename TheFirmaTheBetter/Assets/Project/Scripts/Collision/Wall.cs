using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project.Scripts.Collision
{
    public class Wall : ICollidable
    {
        public void DestroySelf() { }

        public void HandleCollision<T1>(T1 objectThatHit) where T1 : ICollidable
        {
            if (objectThatHit is Projectile)
            {
                objectThatHit.DestroySelf();
                return;
            }


        }
    }
}
