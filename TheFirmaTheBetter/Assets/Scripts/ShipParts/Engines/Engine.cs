using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Part
{
    [SerializeField]
    private EngineData engineData;
    public override string partCategoryName => "Engine";
}