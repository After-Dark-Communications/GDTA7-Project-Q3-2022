using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Ship Parts/Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string weaponName;

    [SerializeField]
    private GameObject projectilePrefab;

    [Header("Weapon stats")]

    [SerializeField]
    private int damage;

    [SerializeField]
    [Range(0, 10)]
    [Tooltip("Bullets per second")]
    private float fireRate;

    [SerializeField]
    [Range(0, 45)]
    private float sideSpreadAngle;

    [SerializeField]
    [Range(0, 100)]
    private float weaponRange;

    public string WeaponName { get { return weaponName; } }
    public GameObject ProjectilePrefab { get { return projectilePrefab; } }
    public float Damage { get { return damage; } }
    public float FireRate { get { return fireRate; } }
    public float SideSpreadAngle { get { return sideSpreadAngle; } }

}
