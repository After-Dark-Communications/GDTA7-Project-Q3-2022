using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTextShower : MonoBehaviour
{
    private const string countDownTriggerName = "StartCountDown";

    [SerializeField]
    private Animator animator;


    private void Awake()
    {
        Channels.OnControllerShemeHidden += OnControllerShemeHidden;
    }

    private void OnControllerShemeHidden()
    {
        ShowRoundText();
        Channels.OnRoundStarted += OnRoundStarted;
    }

    private void OnRoundStarted(int roundIndex, int numberOfRounds)
    {
        ShowRoundText();
    }

    private void ShowRoundText()
    {
        animator.SetTrigger(countDownTriggerName);
    }

    private void OnDestroy()
    {
        Channels.OnRoundStarted -= OnRoundStarted;
        Channels.OnControllerShemeHidden -= OnControllerShemeHidden;
    }
}
