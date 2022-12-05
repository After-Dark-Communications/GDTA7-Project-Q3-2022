using EventSystem;
using ShipSelection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField]
    private Arrow upArrow;

    [SerializeField]
    private Arrow downArrow;

    [SerializeField]
    private PlayerSelectionScreen playerSelectionScreen;

    private void OnEnable()
    {
        Channels.OnNavigateUp += TriggerUpArrow;
        Channels.OnNavigateDown += TriggerDownArrow;
    }

    private void OnDisable()
    {
        Channels.OnNavigateUp -= TriggerUpArrow;
        Channels.OnNavigateDown -= TriggerDownArrow;
    }

    private void TriggerUpArrow(int playerNumber)
    {
        if (playerNumber == playerSelectionScreen.PlayerNumber)
        {
            upArrow.PlaySelectedAnimation();
        }
    }

    private void TriggerDownArrow(int playerNumber)
    {
        if (playerNumber == this.playerSelectionScreen.PlayerNumber)
        {
            downArrow.PlaySelectedAnimation();
        }
    }
}
