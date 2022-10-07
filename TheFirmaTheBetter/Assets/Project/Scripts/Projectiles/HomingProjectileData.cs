using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(fileName = "HomingProjectileData", menuName = "Projectiles/Create new HomingProjectileData")]
    public class HomingProjectileData : ProjectileData
    {
        private float homingRange;

        public float HomingRange { get { return homingRange; } }
    }
}