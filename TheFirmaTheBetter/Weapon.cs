using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Part
{
    [SerializeField]
    private WeaponData weaponData;

    public override string PartName => "Weapon";
}
