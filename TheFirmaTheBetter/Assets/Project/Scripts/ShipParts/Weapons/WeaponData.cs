using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Ship Parts/Weapon")]
public class WeaponData : PartData
{
    [SerializeField]
    private string weaponName;

    [SerializeField]
    private GameObject projectilePrefab;

    [Header("Weapon stats")]
    [SerializeField]
    [Range(0, 10)]
    [Tooltip("Bullets per second")]
    private float fireRate;

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

}
