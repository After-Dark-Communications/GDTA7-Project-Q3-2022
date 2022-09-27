using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MineData", menuName = "Projectiles/Create new MineData")]
public class MineData : ProjectileData
{
    private float explodingTime;

    public float ExplodingTime { get { return explodingTime; } }
}
