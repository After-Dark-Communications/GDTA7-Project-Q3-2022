using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField]
    private Dictionary<int, int> playerScores;
    [SerializeField]
    private List<int> orderedScores;

    public Dictionary<int, int> PlayerScores { get => playerScores; set => playerScores = value; }

    private void Awake()
    {
        PlayerScores = new Dictionary<int, int>();
        orderedScores = new List<int>();
        Channels.KingOfTheHill.OnKingOfTheHillScore += AddScore;
        Channels.KingOfTheHill.OnKingOfTheHillStart += StartGame;
    }

    private void Update()
    {
        for (int i = 0; i < orderedScores.Count; i++)
        {
            orderedScores[i] = playerScores[i];
        }
    }

    private void OnDestroy()
    {
        Channels.KingOfTheHill.OnKingOfTheHillScore -= AddScore;
        Channels.KingOfTheHill.OnKingOfTheHillStart -= StartGame;
    }

    private void AddScore(int playerNumber, int scoreToAdd)
    {
        int score = 0;
        PlayerScores.TryGetValue(playerNumber, out score);
        score += scoreToAdd;
        PlayerScores[playerNumber] = score;
    } 
    
    private void StartGame(List<int> playerNumbers)
    {
        foreach(int playerNumber in playerNumbers)
        {
            PlayerScores.Add(playerNumber, 0);
            orderedScores.Add(0);
        }
    }
}
