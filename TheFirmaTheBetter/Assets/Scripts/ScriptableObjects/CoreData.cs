using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCoreData", menuName = "Create new CoreData")]
public class CoreData : PartData
{
    [SerializeField]
    private int health;

    [SerializeField]
    private float ammoCapacity;

    [SerializeField]
    private float fuelCapacity;

    public int Health => health;

    public float AmmoCapacity => ammoCapacity;

    public float FuelCapacity => fuelCapacity;
}
