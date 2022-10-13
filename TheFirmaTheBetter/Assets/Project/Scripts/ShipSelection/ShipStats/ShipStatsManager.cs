using EventSystem;
using ShipParts;
using ShipParts.Ship;
using ShipSelection.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            switch (enabledStatGameObjectIndex)
            {
                case 0:
                    SetShipStats(stats.ShipStats, changedShipStats);
                    break;
                case 1:
                    SetWeaponStats(stats.WeaponStats, changedShipStats);
                    break;
                case 2:
                    SetSpecialStats(stats.SpecialStats, changedShipStats);
                    break;
                default:
                    break;
            }

        }

        private void SetShipStats(List<ShipStat> shipStatsUI, ShipStats changedShipStats)
        {
            shipStatsUI[0].SetValueFill(changedShipStats.Speed, 75, 200);
            shipStatsUI[1].SetValueFill(changedShipStats.MaxHealth, 200, 1000);
            shipStatsUI[2].SetValueFill(changedShipStats.Handling, 200, 800);
            shipStatsUI[3].SetValueFill(changedShipStats.EnergyCapacity, 12, 50);
        }

        private void SetWeaponStats(List<WeaponStat> weaponStatsUI, ShipStats changedWeaponStats)
        {
            weaponStatsUI[0].SetValueFill(changedWeaponStats.Range, 10, 80);
            weaponStatsUI[1].SetValueFill(changedWeaponStats.FireRate, 1, 10);
            weaponStatsUI[2].SetValueFill(changedWeaponStats.EnergyCost, 0, 1);
        }

        private void SetSpecialStats(List<SpecialStat> specialStatsUI, ShipStats changedSpecialStats)
        {
            specialStatsUI[0].StatName.text = changedSpecialStats.SpecialName.ToString();
            specialStatsUI[0].Description.text = changedSpecialStats.SpecialDescription.ToString();
        }

        public int PlayerIndex { get { return playerIndex; } set { playerIndex = value; } }


        public void OnDisable()
        {
            Channels.OnPlayerStatsChanged -= PlayerStatChange;
            Channels.OnEnabledStatGameObject -= SetAsEnableGameObject;
        }
    }
}