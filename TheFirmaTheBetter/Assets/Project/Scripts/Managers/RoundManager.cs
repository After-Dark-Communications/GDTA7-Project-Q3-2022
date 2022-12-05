using EventSystem;
using Managers;
using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static readonly float _slowMotionTiming = 2;
    public static readonly float _slowMotionKillTimer = 1.99f;

    private const float slowStrenght = 0.5f;

    public int DebugNumberOfRounds = 3;

    private int numberOfRounds;
    private int currentRoundIndex = -1;

    private void OnDisable()
    {
        Channels.OnRoundOver -= RoundOver;
    }

    private void Start()
    {
#if UNITY_EDITOR
        DebugNumberOfRounds = 1;
#endif
        SetRounds(DebugNumberOfRounds);
        Channels.OnRoundOver += RoundOver;
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
            StartCoroutine(EndOfRoundTime());
        }
        else
        {
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndOfRoundTime()
    {
        Time.timeScale = slowStrenght;
        yield return new WaitForSeconds(_slowMotionTiming);
        Time.timeScale = 1f;

        currentRoundIndex++;
        Channels.OnRoundStarted?.Invoke(currentRoundIndex, numberOfRounds);
    }

    private IEnumerator EndGame()
    {
        Time.timeScale = slowStrenght;
        yield return new WaitForSeconds(_slowMotionTiming);
        Channels.OnGameOver?.Invoke();
        Time.timeScale = 1f;
        SceneSwitchManager.SwitchToLastScene();
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
