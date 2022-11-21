using ShipParts.Cores;
using ShipParts.Engines;
using ShipParts.Specials;
using ShipParts.Weapons;
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
        private const float liniarFactor = 0.06f;
        private const float floatinessMultiplier = 0.25f;

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
        //weapon
        private float _range;
        private float _fireRate;
        private float _energyCost;
        private float _dps;
        private float _damage;
        //special
        private string _specialName;
        private string _specialDescription;
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
            //weapon
            _range = 0;
            _fireRate = 0;
            _energyCost = 0;
            //special
            _specialName = "";
            _specialDescription = "";

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
        }

        /// <summary>Updates the <see cref="CoreData"/> related stats</summary>
        /// <param name="coreData"><see cref="CoreData"/> values to use</param>
        /// <param name="shipBuilder"><see cref="ShipBuilder"/> that is associated with these stats</param>
        public void UpdateStats(CoreData coreData, ShipBuilder shipBuilder)
        {
            _energyCapacity = coreData.EnergyCapacity;
            _fuelCapacity = coreData.FuelCapacity;
            _maxHealth = coreData.Health;
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

            UpdateDragWithWeight();
        }

        private void UpdateDragWithWeight()
        {
            _drag = _totalWeight * liniarFactor;
        }

        /// <summary>Adds to the weight modifier list.</summary>
        /// <param name="weight">new weight to add</param>
        /// <param name="shipBuilder"><see cref="ShipBuilder"/> that is associated with these stats</param>
        /// <remarks>The weight will be added to the last index of the list</remarks>
        public void AddWeightModifier(int weight, ShipBuilder shipBuilder)
        {
            _totalWeightModifier.Add(weight);
        }

        /// <summary>Removes weight from the modifier list at index.</summary>
        /// <param name="weightIndex">index to remove at</param>
        /// <param name="shipBuilder"><see cref="ShipBuilder"/> that is associated with these stats</param>
        public void RemoveWeightModifier(int weightIndex, ShipBuilder shipBuilder)
        {
            _totalWeightModifier.RemoveAt(weightIndex);
        }

        public void UpdateStats(WeaponData weaponData)
        {
            _range = weaponData.Range;
            _fireRate = weaponData.FireRate;
            _energyCost = weaponData.EnergyCost;
            _dps = weaponData.DPS;
            _damage = weaponData.Damage;
        }

        public void UpdateStats(SpecialData specialData)
        {
            _specialName = specialData.PartName;
            _specialDescription = specialData.Description;
        }

        public float Speed { get => _speed * floatinessMultiplier; }
        public float RawSpeed { get => _speed; }
        public float Handling { get => _handling; }
        public float Drag { get => _drag * floatinessMultiplier; }
        public float RawDrag { get => _drag; }
        public int MaxHealth { get => _maxHealth; }
        public int EnergyCapacity { get => _energyCapacity; }
        public float FuelCapacity { get => _fuelCapacity; }
        public float FuelUsage { get => _fuelUsage; }
        public float Range { get => _range; }
        public float FireRate { get => _fireRate; }
        public float EnergyCost { get => _energyCost; }
        public float DPS { get => _dps; }
        public float Damage { get => _damage; }
        public string SpecialName { get => _specialName; }
        public string SpecialDescription { get => _specialDescription; }
        public int TotalWeight { get => _totalWeight; }

        public List<float> SpeedModifier => _speedModifier;
        public List<float> HandlingModifier => _handlingModifier;
        public List<float> DragModifier => _dragModifier;
        public List<int> HealthModifier => _healthModifier;
        public List<int> EnergyCapacityModifier => _energyCapacityModifier;
        public List<float> EnergyGenerationModifier => _energuGenerationModifier;
        public List<float> FuelCapacityModifier => _fuelCapacityModifier;
        public List<float> FuelUsageModifier => _fuelUsageModifier;
        public List<int> TotalWeightModifier => _totalWeightModifier;

        public float SumSpeedModifier => _speedModifier.Sum();
        public float SumhHandlingModifier => _handlingModifier.Sum();
        public float SumDragModifier => _dragModifier.Sum();
        public int SumHealthModifier => _healthModifier.Sum();
        public int SumEnergyCapacityModifier => _energyCapacityModifier.Sum();
        public float SumEnergyGenerationModifier => _energuGenerationModifier.Sum();
        public float SumFuelCapacityModifier => _fuelCapacityModifier.Sum();
        public float SumFuelUsageModifier => _fuelUsageModifier.Sum();
        public int SumTotalWeightModifier => _totalWeightModifier.Sum();
    }
}
