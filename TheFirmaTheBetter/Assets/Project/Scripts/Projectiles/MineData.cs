using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(fileName = "MineData", menuName = "Astrofire/Projectiles/Create new MineData")]
    public class MineData : ProjectileData
    {
        [SerializeField]
        private float explodingTime;

        public float ExplodingTime { get { return explodingTime; } }
    }
}