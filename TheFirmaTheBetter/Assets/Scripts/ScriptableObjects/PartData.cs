using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPartData", menuName = "Create new PartData")]
public class PartData : ScriptableObject
{
    [SerializeField]
    private Vector3 connectionPoint;

    [SerializeField]
    private int partWeight;

    public Vector3 ConnectionPoint => connectionPoint;

    public int PartWeight => partWeight;
}
