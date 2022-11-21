using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KOTHGameEnder : MonoBehaviour
{
    private const int _endGameAmountOfPoints = 30;

    private Dictionary<int,int> _points = new Dictionary<int,int>();

    private void OnEnable()
    {
        Channels.KingOfTheHill.OnKingOfTheHillScore += OnKingOfTheHillScore;
    }

    private void OnDisable()
    {
        Channels.KingOfTheHill.OnKingOfTheHillScore -= OnKingOfTheHillScore;
    }

    private void OnKingOfTheHillScore(int playerNumber, int scoreToAdd)
    {
        if (_points.ContainsKey(playerNumber) == false)
        {
            _points.Add(playerNumber, scoreToAdd);
            return;
        }

        _points[playerNumber] += scoreToAdd;

        CheckEndGame();
    }

    void CheckEndGame()
    {
        for (int i = 0; i < _points.Values.Count; i++)
        {
            int value = _points[i];

            if (value >= _endGameAmountOfPoints)
            {
                Channels.OnRoundOver?.Invoke(0, _points.Keys.ToArray()[i]) ;
            }
        }
    }
}
