using Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Part
{
    [SerializeField]
    private CoreData coreData;
    public override string PartCategoryName => "Core";

    public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
    {
        if (connectionPoint is CoreConnectionPoint)
            return true;

        return false;
    }

    public override bool IsMyType(Part part)
    {
        if (part is Core)
            return true;

        return false;
    }
}
