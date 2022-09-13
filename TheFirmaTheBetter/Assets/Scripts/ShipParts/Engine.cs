using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Part
{
    public override string PartName => "Engine";
    [SerializeField]
    private EngineData engineData;

    private float speed;
    private float handling;
    private float fuelUsage;

    private void Awake()
    {
        speed = engineData.Speed;
        handling = engineData.Handling;
        fuelUsage = engineData.FuelUsage;
    }
}