using ShipParts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatsManager : MonoBehaviour
{
    [SerializeField]
    private List<ShipStatsCollection> stats = new List<ShipStatsCollection>();
    [SerializeField]
    private int playerIndex;
    private void Awake()
    {
        foreach (ShipStatsCollection shipStatsCollection in gameObject.GetComponentsInChildren<ShipStatsCollection>())
        {
            stats.Add(shipStatsCollection);
        }

        Channels.OnPlayerStatsChanged += PlayerStatChange;
    }

    private void PlayerStatChange(ShipBuilder shipBuilder, ShipStats changedShipStats)
    {
        playerIndex = shipBuilder.PlayerNumber;
        SetShipStats(stats[0], changedShipStats);
    }

    private void SetShipStats(ShipStatsCollection shipStatsUI, ShipStats changedShipStats)
    {
        shipStatsUI.ShipStats[0].GetComponent<ShipStat>().StatValue.text = changedShipStats.Speed.ToString();
        shipStatsUI.ShipStats[1].GetComponent<ShipStat>().StatValue.text = changedShipStats.MaxHealth.ToString();
        shipStatsUI.ShipStats[2].GetComponent<ShipStat>().StatValue.text = changedShipStats.Handling.ToString();
        shipStatsUI.ShipStats[3].GetComponent<ShipStat>().StatValue.text = changedShipStats.EnergyCapacity.ToString();

    }

    private void SetWeaponStats()
    {

    }
}
