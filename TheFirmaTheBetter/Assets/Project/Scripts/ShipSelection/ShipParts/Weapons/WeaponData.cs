using Projectiles;
using UnityEngine;

namespace ShipParts.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Astrofire/Ship Parts/Weapon")]
    public class WeaponData : PartData
    {
        [Header("Weapon stats")]

        [SerializeField]
        private Projectile projectilePrefab;

        [SerializeField]
        [Range(0, 10)]
        [Tooltip("Bullets per second")]
        private float fireRate;

        [Range(1, 20)]
        [Tooltip("amountOfBullets")]
        [SerializeField]
        private int amountOfBullets = 1;

        [SerializeField]
        [Range(1, 10)]
        [Tooltip("Cost in energy to fire weapon")]
        private int energyCost;

        [SerializeField]
        [Range(0, 45)]
        private float sideSpreadAngle;

        [SerializeField]
        [Range(0, 100)]
        private float range;

        [SerializeField]
        private float armingTime;

        [Header("Audio")]

        [Tooltip("FMOD Event to call when fired")]
        [SerializeField]
        private FMODUnity.EventReference weaponFireEvent;

        public Projectile ProjectilePrefab { get { return projectilePrefab; } }
        public float FireRate { get { return fireRate; } }
        public float SideSpreadAngle { get { return sideSpreadAngle; } }
        public float Range { get { return range; } }
        public float ArmingTime { get { return armingTime; } }
        public int EnergyCost { get { return energyCost; } }
        public int AmountOfBullets{ get { return amountOfBullets; } }
        public float DPS { get { return ProjectilePrefab.ProjectileData.Damage * FireRate * AmountOfBullets; } }

        public FMODUnity.EventReference WeaponFireEvent => weaponFireEvent;
    }
}
