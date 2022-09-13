using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEngineData", menuName = "Create new EngineData")]
public class EngineData : PartData
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float handling;

    [SerializeField]
    private float fuelUsage;

    public float Speed => speed;

    public float Handling => handling;

    public float FuelUsage => fuelUsage;
}