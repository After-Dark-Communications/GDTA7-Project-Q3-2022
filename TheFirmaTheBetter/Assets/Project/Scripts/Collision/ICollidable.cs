using ShipParts.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collisions
{
    public interface ICollidable
    {
        public void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats) where T1 : ICollidable;

        public void DestroySelf();
    }
}
