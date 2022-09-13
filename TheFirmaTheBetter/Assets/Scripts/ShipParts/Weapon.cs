using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Part
{
    public override string PartName => "Weapon";

    [SerializeField]
    private WeaponData weaponData;

    private int weaponDamage;

    private float weaponSpread;

    private float weaponRange;

    private float rateOfFire;

    private float projectileSpeed;

    private void Awake()
    {
        weaponDamage = weaponData.WeaponDamage;
        weaponSpread = weaponData.WeaponSpread;
        weaponRange = weaponData.WeaponRange;
        rateOfFire = weaponData.RateOfFire;
        projectileSpeed = weaponData.ProjectileSpeed;
    }
}
