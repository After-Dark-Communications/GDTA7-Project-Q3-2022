using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Part : MonoBehaviour
{
    private Vector3 connectionPoint;

    private PartData partData;

    private int weight;
    public abstract string PartName { get; }

    private void Awake()
    {
        connectionPoint = partData.ConnectionPoint;
        weight = partData.PartWeight;
    }
}
