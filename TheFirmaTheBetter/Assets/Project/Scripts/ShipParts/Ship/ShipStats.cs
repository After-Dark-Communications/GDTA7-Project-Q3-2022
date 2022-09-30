﻿using ShipParts.Cores;
using ShipParts.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EventSystem;

namespace ShipParts.Ship
{
    public class ShipStats
    {
        //movement
        private float _speed;
        private float _handling;
        private float _drag;

        //damage
        private int _maxHealth;
        //energy
        private int _energyCapacity;
        private float _energyGenerationRate;
        //fuel
        private float _fuelCapacity;
        private float _fuelUsage;
        //body
        private int _totalWeight;

        private readonly List<float> _speedModifier;
        private readonly List<float> _handlingModifier;
        private readonly List<float> _dragModifier;
        private readonly List<int> _healthModifier;
        private readonly List<int> _energyCapacityModifier;
        private readonly List<float> _energuGenerationModifier;
        private readonly List<float> _fuelCapacityModifier;
        private readonly List<float> _fuelUsageModifier;
        private readonly List<int> _totalWeightModifier;

        public ShipStats()//, SpecialData specialData, WeaponData weaponData)
        {
            //set fixed values
            //engine
            _speed = 0;
            _handling = 0;
            _fuelUsage = 0;

            //core
            _energyCapacity = 0;
            _fuelCapacity = 0;
            _maxHealth = 0;

            //set modifier based values
            _energyGenerationRate = 0;
            //body values
            _totalWeight = 0;
            //calculate drag
            _drag = 0;

            //initialize lists
            _speedModifier = new List<float>();
            _handlingModifier = new List<float>();
            _dragModifier = new List<float>();
            _healthModifier = new List<int>();
            _energyCapacityModifier = new List<int>();
            _energuGenerationModifier = new List<float>();
            _fuelCapacityModifier = new List<float>();
            _fuelUsageModifier = new List<float>();
            _totalWeightModifier = new List<int>();

        }

        /// <summary>Updates the <see cref="EngineData"/> related stats</summary>
        /// <param name="engineData"><see cref="EngineData"/> values to use</param>
        /// <param name="shipBuilder"><see cref="ShipBuilder"/> that is associated with these stats</param>
        public void UpdateStats(EngineData engineData, ShipBuilder shipBuilder)
        {
            _speed = engineData.Speed;
            _handling = engineData.Handling;
            _fuelUsage = engineData.FuelUsage;
            Channels.OnPlayerStatsChanged?.Invoke(shipBuilder, this);
        }

        /// <summary>Updates the <see cref="CoreData"/> related stats</summary>
        /// <param name="coreData"><see cref="CoreData"/> values to use</param>
        /// <param name="shipBuilder"><see cref="ShipBuilder"/> that is associated with these stats</param>
        public void UpdateStats(CoreData coreData, ShipBuilder shipBuilder)
        {
            _energyCapacity = coreData.EnergyCapacity;
            _fuelCapacity = coreData.FuelCapacity;
            _maxHealth = coreData.Health;
            Channels.OnPlayerStatsChanged?.Invoke(shipBuilder, this);
        }

        /// <summary>Sets <see cref="_totalWeight"/> with the given weights</summary>
        /// <param name="shipBuilder"><see cref="ShipBuilder"/> that is associated with these stats</param>
        /// <param name="weights">weights that will be set</param>
        public void SetTotalWeight(ShipBuilder shipBuilder, params int[] weights)
        {
            _totalWeight = 0;
            foreach (int weight in weights)
            {
                _totalWeight += weight;
            }
            Channels.OnPlayerStatsChanged?.Invoke(shipBuilder, this);
        }

        /// <summary>Adds to the weight modifier list.</summary>
        /// <param name="weight">new weight to add</param>
        /// <param name="shipBuilder"><see cref="ShipBuilder"/> that is associated with these stats</param>
        /// <remarks>The weight will be added to the last index of the list</remarks>
        public void AddWeightModifier(int weight, ShipBuilder shipBuilder)
        {
            _totalWeightModifier.Add(weight);
            Channels.OnPlayerStatsChanged?.Invoke(shipBuilder, this);
        }

        /// <summary>Removes weight from the modifier list at index.</summary>
        /// <param name="weightIndex">index to remove at</param>
        /// <param name="shipBuilder"><see cref="ShipBuilder"/> that is associated with these stats</param>
        public void RemoveWeightModifier(int weightIndex, ShipBuilder shipBuilder)
        {

            _totalWeightModifier.RemoveAt(weightIndex);
            Channels.OnPlayerStatsChanged?.Invoke(shipBuilder, this);
        }

        public float Speed { get => _speed; }
        public float Handling { get => _handling; }
        public float Drag { get => _drag; }
        public int MaxHealth { get => _maxHealth; }
        public int EnergyCapacity { get => _energyCapacity; }
        public float FuelCapacity { get => _fuelCapacity; }
        public float FuelUsage { get => _fuelUsage; }
        public int TotalWeight { get => _totalWeight; }

        public List<float> SpeedModifier => _speedModifier;
        public List<float> handlingModifier => _handlingModifier;
        public List<float> dragModifier => _dragModifier;
        public List<int> healthModifier => _healthModifier;
        public List<int> energyCapacityModifier => _energyCapacityModifier;
        public List<float> energyGenerationModifier => _energuGenerationModifier;
        public List<float> fuelCapacityModifier => _fuelCapacityModifier;
        public List<float> fuelUsageModifier => _fuelUsageModifier;
        public List<int> TotalWeightModifier => _totalWeightModifier;


    }
}
