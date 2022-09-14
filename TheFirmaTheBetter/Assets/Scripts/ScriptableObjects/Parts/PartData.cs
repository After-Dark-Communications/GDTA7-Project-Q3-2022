using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartData : ScriptableObject
{
    [Header("PartStats")]
    [SerializeField]
    private Vector3 connectionPoint;

    [SerializeField]
    private int partWeight;

    public Vector3 ConnectionPoint => connectionPoint;

    public int PartWeight => partWeight;
}
