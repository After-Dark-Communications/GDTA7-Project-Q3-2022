using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Part/Create new WeaponData")]
public class WeaponData : PartData
{
    [SerializeField]
    private  int weaponDamage;

    [SerializeField]
    private  float weaponSpread;

    [SerializeField]
    private  float weaponRange;

    [SerializeField]
    private  float rateOfFire;

    [SerializeField]
    private  float projectileSpeed;

    public int WeaponDamage => weaponDamage;

    public float WeaponSpread => weaponSpread;

    public float WeaponRange => weaponRange;

    public float RateOfFire => rateOfFire;

    public float ProjectileSpeed => projectileSpeed;
}