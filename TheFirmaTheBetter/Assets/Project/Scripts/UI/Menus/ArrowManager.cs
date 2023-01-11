using EventSystem;
using ShipParts.Ship;
using ShipSelection;
using System;
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
        Channels.OnShipCompleted += TriggerDisableArrow;
    }

    private void OnDisable()
    {
        Channels.OnNavigateUp -= TriggerUpArrow;
        Channels.OnNavigateDown -= TriggerDownArrow;
        Channels.OnShipCompleted -= TriggerDisableArrow;
    }

    private void TriggerUpArrow(int playerNumber)
    {
        Debug.Log($" UP");
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

    /// <summary>
    /// Method that calls the method of Class Arrow and sets the boolean
    /// that tracks if ship is completed to True
    /// </summary>
    /// <param name="completedShipBuilder"></param>
    private void TriggerDisableArrow(ShipBuilder completedShipBuilder)
    {
        if (completedShipBuilder.PlayerNumber == this.playerSelectionScreen.PlayerNumber)
        {
            upArrow.PlayShipCompletedAnimation();
            downArrow.PlayShipCompletedAnimation();
        }
           
    }

}
