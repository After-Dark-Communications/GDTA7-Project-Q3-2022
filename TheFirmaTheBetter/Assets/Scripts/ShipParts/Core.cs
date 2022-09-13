using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Part
{
    public override string PartName => "Core";
    [SerializeField]
    private CoreData coreData;

    private int health;
    private float ammoCapacity;
    private float fuelCapacity;

    private void Awake()
    {
        health = coreData.Health;
        ammoCapacity = coreData.AmmoCapacity;
        fuelCapacity = coreData.FuelCapacity;
    }
}
