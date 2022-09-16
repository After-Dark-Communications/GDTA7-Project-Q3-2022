using Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints;
using UnityEngine;
public class SpecialAbility : Part
{
    [SerializeField]
    private SpecialData specialData;

    public override bool IsMyType(Part part)
    {
        if (part is SpecialAbility)
            return true;

        return false;
    }

    public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
    {
        if (connectionPoint is SpecialConnectionPoint)
            return true;

        return false;
    }

    public override string PartCategoryName => "Special";
}