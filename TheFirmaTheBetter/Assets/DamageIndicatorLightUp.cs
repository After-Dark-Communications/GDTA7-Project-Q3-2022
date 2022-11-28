using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using ShipSelection;
using UnityEngine;

public class DamageIndicatorLightUp : MonoBehaviour
{
    private ShipInfo _ShipInfo;

    [SerializeField] int currentPlayerNumber;

    private void OnEnable()
    {
        //get player number for current ship (from ShipInfo)
        _ShipInfo = GetComponent<ShipInfo>();
        currentPlayerNumber = _ShipInfo.PlayerNumber;
        //subscribe to OnPlayerTakeDamage
        Channels.OnPlayerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnPlayerTakeDamage(ShipBuilder hittedBuilder, int damageAmount, int indexOfPlayerThatShotTheBullet)
    {
        //if current ship is damaged light up
        if (hittedBuilder.PlayerNumber == currentPlayerNumber)
        {
            Debug.Log("player " + currentPlayerNumber + " hit");
        }
        
    }

    private void OnDisable()
    {
        //unsubscribe to OnPlayerTakeDamage?
    }
}
