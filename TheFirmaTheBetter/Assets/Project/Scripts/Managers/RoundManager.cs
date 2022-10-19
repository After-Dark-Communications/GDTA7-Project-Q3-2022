using EventSystem;
using Managers;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public int DebugNumberOfRounds = 3;

    private int numberOfRounds;
    private int currentRoundIndex = -1;

    private void OnEnable()
    {
        Channels.OnRoundOver += RoundOver;
    }

    private void OnDisable()
    {
        Channels.OnRoundOver -= RoundOver;
    }

    private void Start()
    {
        SetRounds(DebugNumberOfRounds);
    }

    public void SetRounds(int numberOfRounds)
    {
        this.numberOfRounds = numberOfRounds;
        currentRoundIndex = 1;
        Channels.OnRoundStarted?.Invoke(currentRoundIndex, this.numberOfRounds);
    }

    private void RoundOver(int roundIndex, int winnerIndex)
    {
        NextRound();
    }

    private void NextRound()
    {
        if (!IsLastRound)
        {
            currentRoundIndex++;
            Channels.OnRoundStarted?.Invoke(currentRoundIndex, numberOfRounds);
        }
        else
        {
            Channels.OnGameOver?.Invoke();
            SceneSwitchManager.SwitchToLastScene();
        }
    }

    public int CurrentRoundIndex
    {
        get { return currentRoundIndex; }
    }

    public bool IsLastRound
    {
        get
        {
            if (currentRoundIndex == numberOfRounds)
            { return true; }
            else
            { return false; }
        }
    }
}
