using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    [SerializeField]
    private List<ShipBuilder> results;

    private void Awake()
    {
        transform.parent = null;
        DontDestroyOnLoad(gameObject);

        results = new();

        Channels.OnPlayerBecomesDeath += AddResult;
    }

    public void AddResult(ShipBuilder shipBuilder)
    {
        results.Add(shipBuilder);
    }

}
