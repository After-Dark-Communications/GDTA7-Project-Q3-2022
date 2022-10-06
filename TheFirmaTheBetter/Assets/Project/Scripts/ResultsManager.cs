using ShipSelection.ShipBuilders;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : Manager
{
    public static ResultsManager Instance;

    [SerializeField]
    private EndStatsData[] results;

    private int lastEmptyPosition;

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
    }

    public void SetupResults(int numberOfPlayers)
    {
        results = new EndStatsData[numberOfPlayers];
        lastEmptyPosition = numberOfPlayers - 1;
    }

    public void AddResult(EndStatsData result)
    {
        if (lastEmptyPosition >= 0)
        {
            results[lastEmptyPosition] = result;
            lastEmptyPosition--;
        }
    }

    public EndStatsData[] Results { get { return results; } }
}
