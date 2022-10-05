using UnityEngine;

namespace ShipParts.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Ship Parts/Weapon")]
    public class WeaponData : PartData
    {
        [SerializeField]
        private string weaponName;

        [SerializeField]
        private GameObject projectilePrefab;
        [SerializeField]
        private float armingTime;
        [Header("Weapon stats")]
        [SerializeField]
        [Range(0, 10)]
        [Tooltip("Bullets per second")]
        private float fireRate;
        [Range(1, 20)]
        [Tooltip("amountOfBullets")]
        [SerializeField]
        private float amountOfBullets = 1f;
        [SerializeField]
        [Range(1, 10)]
        [Tooltip("Cost in energy to fire weapon")]
        private int energyCost;
        [Tooltip("FMOD Event to call when fired")]
        [SerializeField]
        private FMODUnity.EventReference weaponFireEvent;

        [SerializeField]
        [Range(0, 45)]
        private float sideSpreadAngle;

        [SerializeField]
        [Range(0, 100)]
        private float range;

        public string WeaponName { get { return weaponName; } }
        public GameObject ProjectilePrefab { get { return projectilePrefab; } }
        public float FireRate { get { return fireRate; } }
        public float SideSpreadAngle { get { return sideSpreadAngle; } }
        public float Range { get { return range; } }

        public float ArmingTime => armingTime;

        public int EnergyCost => energyCost;

        public float AmountOfBullets => amountOfBullets;

        public FMODUnity.EventReference WeaponFireEvent => weaponFireEvent;
    }
}
