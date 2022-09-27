using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project.Scripts.Collision
{
    public interface ICollidable
    {
        public void HandleCollision<T1>(T1 objectThatHit) where T1 : ICollidable;

        public void DestroySelf();
    }
}
