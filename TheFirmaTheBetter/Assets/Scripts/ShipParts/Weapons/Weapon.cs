using Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Part
{
    [SerializeField]
    private WeaponData weaponData;

    public override string PartCategoryName => "Weapon";

    public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
    {
        if (connectionPoint is WeaponConnectionPoint)
            return true;

        return false;
    }

    public override bool IsMyType(Part part)
    {
        if (part is Weapon)
            return true;

        return false;
    }
}
