using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class CrownShower : MonoBehaviour
{
    private Image crownImage;
    private int playerIndex;
    
    private void Awake()
    {
        Channels.OnRoundOver += OnRoundOver;
    }

    private void OnEnable()
    {
        crownImage = GetComponent<Image>();
    }

    private void OnDisable()
    {
        Channels.OnRoundOver -= OnRoundOver;
    }
    private void OnRoundOver(int roundIndex, int winnerIndex)
    {
        crownImage.enabled = false;

        if (playerIndex != winnerIndex)
            return;

        crownImage.enabled = true;
    }
    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }
}
