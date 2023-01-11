using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipSelection;
using EventSystem;
using Collisions;
using ShipParts.Ship;
using System;

public class ScoringZone : MonoBehaviour
{
    List<ShipBuilder> shipsEntered = new List<ShipBuilder>();
    [SerializeField]
    float timeInBetweenGivingPoint = 2f;
    float timer;
    [SerializeField]
    [Range(1, 3)]
    private int ScoreAmount;
    private void OnEnable()
    {
        Channels.OnPlayerBecomesDeath += CheckToRemoveShip; 
    }

    private void CheckToRemoveShip(ShipBuilder shipBuilderThatNeedsDying, int playerIndexOfKiller)
    {
        if(shipsEntered.Contains(shipBuilderThatNeedsDying))
        {
            shipsEntered.Remove(shipBuilderThatNeedsDying);
        }
    }

    private void Update()
    {
        if (shipsEntered.Count == 0)
            return;
        timer += Time.deltaTime;
        if(timer >= timeInBetweenGivingPoint)
        {
            GiveShipInZonePoint();
            timer = 0;
        }
    }

    void GiveShipInZonePoint()
    {
        foreach (ShipBuilder shipsThatEntered in shipsEntered)
        {
            int enteredShipPlayerNumber = shipsThatEntered.PlayerNumber;
            Channels.KingOfTheHill.OnKingOfTheHillScore?.Invoke(enteredShipPlayerNumber, ScoreAmount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ShipBuilder enteredShip = other.GetComponentInParent<ShipBuilder>();
        if (enteredShip != null)
        {
            if (shipsEntered.Contains(enteredShip))
                return;
            shipsEntered.Add(enteredShip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ShipBuilder enteredShip = other.GetComponentInParent<ShipBuilder>();
        if (enteredShip != null)
        {
            if (shipsEntered.Contains(enteredShip))
                shipsEntered.Remove(enteredShip);
        }
    }

    private void OnDisable()
    {
        Channels.OnPlayerBecomesDeath -= CheckToRemoveShip;
    }
}
