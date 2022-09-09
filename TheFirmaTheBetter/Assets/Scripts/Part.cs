using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Part : MonoBehaviour
{
    [SerializeField]
    private Vector3 connectionPoint;

    public abstract string PartName { get; }
}
