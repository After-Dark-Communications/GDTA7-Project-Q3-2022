using EventSystem;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorShower : MonoBehaviour
{
    [SerializeField] 
    private Image border;

    private TMP_Text tmpTextField;

    private int playerIndex;

    private Animator animator;

    private void Awake()
    {
        Channels.OnControllerShemeHidden += ShowIndicator;
        Channels.OnRoundStarted += OnRoundStarted;

        animator = GetComponent<Animator>();
        animator.Play(0);
    }

    private void OnDestroy()
    {
        Channels.OnControllerShemeHidden -= ShowIndicator;
        Channels.OnRoundStarted -= OnRoundStarted;
    }

    private void ShowIndicator()
    {
        animator.SetBool("indicatorReady", true);

        tmpTextField = GetComponentInChildren<TMP_Text>();

        switch (playerIndex)
        {
            case 1:
                border.color = new Color(0.3607f, 0.8039f, 0.3921f);
                break;
            case 2:
                border.color = new Color(1f, 0f, 1f);
                break;
            case 3:
                border.color = new Color(1f, 0.968f, 0.2f);
                break;
            default:
                border.color = new Color(0f, 1f, 1f);
                break;
        }

        tmpTextField.SetText($"Player {playerIndex + 1}");
    }

    private void OnRoundStarted(int roundIndex, int numberOfRounds)
    {
        Debug.Log("round started");
        animator.Play(0);
        animator.SetBool("indicatorReady", true);
    }

    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }
}
