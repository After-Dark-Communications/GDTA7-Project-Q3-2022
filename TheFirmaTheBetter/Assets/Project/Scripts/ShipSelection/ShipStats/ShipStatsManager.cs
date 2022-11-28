using EventSystem;
using ShipParts;
using ShipParts.Cores;
using ShipParts.Engines;
using ShipParts.Ship;
using ShipParts.Weapons;
using ShipParts.Specials;
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

            Channels.OnDisplayabeStatsChanged += OnDisplayabeStatsChanged;
            Channels.OnEnabledStatGameObject += SetAsEnableGameObject;
        }

        private void OnDestroy()
        {
            Channels.OnDisplayabeStatsChanged -= OnDisplayabeStatsChanged;
            Channels.OnEnabledStatGameObject -= SetAsEnableGameObject;
        }

        private void SetAsEnableGameObject(int activateStatGameObjectIndex, int playerNumber)
        {
            if (playerIndex != playerNumber)
                return;
            enabledStatGameObjectIndex = activateStatGameObjectIndex;
        }

        private void OnDisplayabeStatsChanged(int playerIndex, Part changedPart, ShipStats selectedStats, ShipStats hoveredStats)
        {
            if (this.playerIndex != playerIndex)
                return;

            if (changedPart is Core)
            {
                SetCoreStats(stats.ShipStats, selectedStats, hoveredStats);
            }
            else if (changedPart is Engine)
            {
                SetEngineStats(stats.ShipStats, selectedStats, hoveredStats);
            }
            else if (changedPart is Weapon)
            {
                SetWeaponStats(stats.WeaponStats, selectedStats, hoveredStats);
            }
            else if (changedPart is SpecialAbility)
            {
                SetSpecialStats(stats.SpecialStats, selectedStats, hoveredStats);
            }
        }

        private void SetCoreStats(List<ShipStat> shipStatsUI, ShipStats selectedStats, ShipStats hoveredStats)
        {
            shipStatsUI[0].SetValueFill(selectedStats.RawSpeed, StatBoundries.SPEED_BOUNDRIES);
            shipStatsUI[1].SetValueFill(selectedStats.MaxHealth, hoveredStats.MaxHealth, StatBoundries.HEALTH_BOUNDRIES);
            shipStatsUI[2].SetValueFill(selectedStats.Handling, StatBoundries.HANDLING_BOUNDRIES);
            shipStatsUI[3].SetValueFill(selectedStats.EnergyCapacity, hoveredStats.EnergyCapacity, StatBoundries.ENERGY_CAPACITY_BOUNDRIES);
        }

        private void SetEngineStats(List<ShipStat> shipStatsUI, ShipStats selectedStats, ShipStats hoveredStats)
        {
            shipStatsUI[0].SetValueFill(selectedStats.RawSpeed, hoveredStats.RawSpeed, StatBoundries.SPEED_BOUNDRIES);
            shipStatsUI[1].SetValueFill(selectedStats.MaxHealth, StatBoundries.HEALTH_BOUNDRIES);
            shipStatsUI[2].SetValueFill(selectedStats.Handling, hoveredStats.Handling, StatBoundries.HANDLING_BOUNDRIES);
            shipStatsUI[3].SetValueFill(selectedStats.EnergyCapacity, StatBoundries.ENERGY_CAPACITY_BOUNDRIES);
        }

        private void SetWeaponStats(List<WeaponStat> weaponStatsUI, ShipStats selectedStats, ShipStats hoveredStats)
        {
            weaponStatsUI[0].SetValueFill(selectedStats.Range, hoveredStats.Range, StatBoundries.RANGE_BOUNDRIES);
            weaponStatsUI[1].SetValueFill(selectedStats.FireRate, hoveredStats.FireRate, StatBoundries.FIRE_RATE_BOUNDRIES);
            weaponStatsUI[2].SetValueFill(selectedStats.EnergyCost, hoveredStats.EnergyCost, StatBoundries.ENERGY_COST_BOUNDRIES);
            weaponStatsUI[3].SetValueFill(selectedStats.DPS, hoveredStats.DPS, StatBoundries.DPS_BOUNDRIES);
            weaponStatsUI[4].SetValueFill(selectedStats.Damage, hoveredStats.Damage, StatBoundries.DAMAGE_BOUNDRIES);
        }

        private void SetSpecialStats(List<SpecialStat> specialStatsUI, ShipStats selectedStats, ShipStats hoveredStats)
        {
            specialStatsUI[0].StatName.text = hoveredStats.SpecialName.ToString();
            specialStatsUI[0].Description.text = hoveredStats.SpecialDescription.ToString();
        }

        public int PlayerIndex { get { return playerIndex; } set { playerIndex = value; } }
    }
}