using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTextChanger : MonoBehaviour
{
    private const string RoundText = "Round: ";

    [SerializeField]
    private TMP_Text tmpTextField;

    private void Awake()
    {
        Channels.OnRoundOver += OnRoundOver;
    }

    private void OnRoundOver(int roundIndex, int winnerIndex)
    {
        tmpTextField.SetText(RoundText + (roundIndex + 1).ToString());
    }

    private void OnDestroy()
    {
        Channels.OnRoundOver -= OnRoundOver;
    }
}