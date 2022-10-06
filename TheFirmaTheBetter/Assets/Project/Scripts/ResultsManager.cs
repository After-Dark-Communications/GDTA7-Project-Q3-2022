using ShipSelection.ShipBuilders;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : Manager
{
    public static ResultsManager Instance;

    [SerializeField]
    private PlayerStatistics[] results;

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
        results = new PlayerStatistics[numberOfPlayers];
        lastEmptyPosition = numberOfPlayers - 1;
    }

    public void AddResult(PlayerStatistics result)
    {
        if (lastEmptyPosition >= 0)
        {
            results[lastEmptyPosition] = result;
            lastEmptyPosition--;
        }
    }

    public PlayerStatistics[] Results { get { return results; } }
}
