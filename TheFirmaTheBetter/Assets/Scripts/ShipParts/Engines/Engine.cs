using Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Part
{
    [SerializeField]
    private EngineData engineData;
    public override string PartCategoryName => "Engine";

    public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
    {
        if (connectionPoint is EngineConnectionPoint)
            return true;

        return false;
    }

    public override bool IsMyType(Part part)
    {
        if (part is Engine)
            return true;

        return false;
    }
}