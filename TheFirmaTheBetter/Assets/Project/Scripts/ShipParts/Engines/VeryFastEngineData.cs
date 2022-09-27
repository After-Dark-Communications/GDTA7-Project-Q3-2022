using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewVeryFastEngineData", menuName = "Part/Create new VeryFastEngineData")]
public class VeryFastEngineData : EngineData
{
    [Header("VeryFastEngine stats")]
    [SerializeField]
    private float minumumTimeToFallOut;
    [SerializeField]
    private float maximumTimeToFallOut;
}
