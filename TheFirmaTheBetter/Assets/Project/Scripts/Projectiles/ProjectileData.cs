using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Astrofire/Projectiles/Create new ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        [Header("Projectile stats")]
        [SerializeField]
        private int damage;
        [SerializeField]
        private float projectileSpeed;
        [SerializeField]
        private float armingTime;

        [Header("On Impact")]
        [SerializeField]
        private GameObject spawnedObjectOnImpact;

        public int Damage { get { return damage; } }
        public float ProjectileSpeed { get { return projectileSpeed; } }
        public float ArmingTime { get { return armingTime; } }
        public GameObject SpawnedObjectOnImpact { get { return spawnedObjectOnImpact; } }
    }
}