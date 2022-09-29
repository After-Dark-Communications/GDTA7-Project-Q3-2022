using ShipParts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatsManager : MonoBehaviour
{
    [SerializeField]
    private ShipStatsCollection stats;
    [SerializeField]
    private int playerIndex;
    private void Awake()
    {
        ShipStatsCollection statsCollection = gameObject.GetComponentInChildren<ShipStatsCollection>();
        if (statsCollection != null)
        {
            stats = statsCollection;
        }

        Channels.OnPlayerStatsChanged += PlayerStatChange;
    }

    private void PlayerStatChange(ShipBuilder shipBuilder, ShipStats changedShipStats)
    {
        if (playerIndex != shipBuilder.PlayerNumber)
            return;
        SetShipStats(stats.ShipStats, changedShipStats);
        SetWeaponStats(stats.WeaponStats, changedShipStats);
    }

    private void SetShipStats(List<ShipStat> shipStatsUI, ShipStats changedShipStats)
    {
        shipStatsUI[0].StatValue.text = changedShipStats.Speed.ToString();
        shipStatsUI[1].StatValue.text = changedShipStats.MaxHealth.ToString();
        shipStatsUI[2].StatValue.text = changedShipStats.Handling.ToString();
        shipStatsUI[3].StatValue.text = changedShipStats.EnergyCapacity.ToString();

    }

    private void SetWeaponStats(List<WeaponStat> weaponStatsUI, ShipStats changedWeaponStats)
    {
        weaponStatsUI[0].StatValue.text = changedWeaponStats.Range.ToString();
        weaponStatsUI[1].StatValue.text = changedWeaponStats.FireRate.ToString();
        weaponStatsUI[2].StatValue.text = changedWeaponStats.EnergyCost.ToString();
    }

    public int PlayerIndex { get { return playerIndex; } set { playerIndex = value; } }
}
