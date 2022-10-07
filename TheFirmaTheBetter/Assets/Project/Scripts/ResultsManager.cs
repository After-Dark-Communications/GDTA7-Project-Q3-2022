using EventSystem;
using ShipSelection.ShipBuilders;
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
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        SetupResults(ShipBuildManager.Instance.AmountOfPlayersJoined);
    }

    private void OnEnable()
    {
        Channels.OnEveryPlayerReady += SetupResults;
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
