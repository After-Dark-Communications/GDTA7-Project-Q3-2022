using ShipParts.Ship;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : Manager
{
    [SerializeField]
    private List<ShipBuilder> results;

    public static ResultsManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }

        results = new();
    }

    public void AddResult(ShipBuilder shipBuilder)
    {
        results.Add(shipBuilder);
    }

    public ShipBuilder[] Results
    {
        get
        {
            results.Reverse();
            return results.ToArray();
        }
    }
}
