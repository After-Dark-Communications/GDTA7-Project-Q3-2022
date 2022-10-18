using EventSystem;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private int numberOfRounds;

    private int currentRoundIndex = -1;

    private void OnEnable()
    {
        SetRounds(3);

        Channels.OnRoundOver += RoundOver;
    }

    private void OnDisable()
    {
        Channels.OnRoundOver -= RoundOver;
    }

    public void SetRounds(int numRounds)
    {
        this.numberOfRounds = numRounds;
        currentRoundIndex = 1;
        Channels.OnRoundStarted?.Invoke(currentRoundIndex);
    }

    private void RoundOver(int roundIndex, int winnerIndex)
    {
        NextRound();
    }

    private void NextRound()
    {
        if (IsLastRound)
        {
            currentRoundIndex++;
            Channels.OnRoundStarted?.Invoke(currentRoundIndex);
        }
        else
        {
            Channels.OnGameOver?.Invoke();
            SceneSwitchManager.SwitchToSceneWithIndex(5);
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
