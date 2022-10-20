using EventSystem;
using ShipParts;
using ShipParts.Ship;
using ShipSelection.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace ShipSelection
{
    public class ShipStatsManager : MonoBehaviour
    {
        private ShipStatsCollection stats;
        [SerializeField]
        private int enabledStatGameObjectIndex;
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
            Channels.OnEnabledStatGameObject += SetAsEnableGameObject;
        }

        private void SetAsEnableGameObject(int activateStatGameObjectIndex, int playerNumber)
        {
            if (playerIndex != playerNumber)
                return;
            enabledStatGameObjectIndex = activateStatGameObjectIndex;
        }

        private void PlayerStatChange(ShipBuilder shipBuilder, ShipStats changedShipStats)
        {
            if (playerIndex != shipBuilder.PlayerNumber)
                return;

            SetShipStats(stats.ShipStats, changedShipStats);
            SetWeaponStats(stats.WeaponStats, changedShipStats);
            SetSpecialStats(stats.SpecialStats, changedShipStats);

        }

        private void SetShipStats(List<ShipStat> shipStatsUI, ShipStats changedShipStats)
        {
            shipStatsUI[0].SetValueFill(changedShipStats.Speed, StatBoundries.SPEED_BOUNDRIES);
            shipStatsUI[1].SetValueFill(changedShipStats.MaxHealth, StatBoundries.HEALTH_BOUNDRIES);
            shipStatsUI[2].SetValueFill(changedShipStats.Handling, StatBoundries.HANDLING_BOUNDRIES);
            shipStatsUI[3].SetValueFill(changedShipStats.EnergyCapacity, StatBoundries.ENERGY_CAPACITY_BOUNDRIES);
        }

        private void SetWeaponStats(List<WeaponStat> weaponStatsUI, ShipStats changedWeaponStats)
        {
            weaponStatsUI[0].SetValueFill(changedWeaponStats.Range, StatBoundries.RANGE_BOUNDRIES);
            weaponStatsUI[1].SetValueFill(changedWeaponStats.FireRate, StatBoundries.FIRE_RATE_BOUNDRIES);
            weaponStatsUI[2].SetValueFill(changedWeaponStats.EnergyCost, StatBoundries.ENERGY_COST_BOUNDRIES);
            weaponStatsUI[3].SetValueFill(changedWeaponStats.DPS, StatBoundries.DPS_BOUNDRIES);
        }

        private void SetSpecialStats(List<SpecialStat> specialStatsUI, ShipStats changedSpecialStats)
        {
            specialStatsUI[0].StatName.text = changedSpecialStats.SpecialName.ToString();
            specialStatsUI[0].Description.text = changedSpecialStats.SpecialDescription.ToString();
        }

        public int PlayerIndex { get { return playerIndex; } set { playerIndex = value; } }

        private void OnDestroy()
        {
            Channels.OnPlayerStatsChanged -= PlayerStatChange;
            Channels.OnEnabledStatGameObject -= SetAsEnableGameObject;
        }
    }
}