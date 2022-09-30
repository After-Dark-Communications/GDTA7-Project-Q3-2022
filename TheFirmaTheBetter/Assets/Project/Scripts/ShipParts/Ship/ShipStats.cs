using ShipParts.Cores;
using ShipParts.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        //weapon
        private float _range;
        private float _fireRate;
        private float _energyCost;
        //special
        private string _specialName;
        private string _specialDescription;

        private readonly List<float> _speedModifier;
        private readonly List<float> _handlingModifier;
        private readonly List<float> _dragModifier;
        private readonly List<int> _healthModifier;
        private readonly List<int> _energyCapacityModifier;
        private readonly List<float> _energuGenerationModifier;
        private readonly List<float> _fuelCapacityModifier;
        private readonly List<float> _fuelUsageModifier;

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

        }

        public void UpdateStats(EngineData engineData)
        {
            _speed = engineData.Speed;
            _handling = engineData.Handling;
            _fuelUsage = engineData.FuelUsage;
        }

        public void UpdateStats(CoreData coreData)
        {
            _energyCapacity = coreData.EnergyCapacity;
            _fuelCapacity = coreData.FuelCapacity;
            _maxHealth = coreData.Health;
        }

        public void UpdateStats(WeaponData weaponData)
        {
            _range = weaponData.Range;
            _fireRate = weaponData.FireRate;
            _energyCost = weaponData.EnergyCost;
        }

        public void UpdateStats(SpecialData specialData)
        {
            _specialName = specialData.PartName;
            _specialDescription = specialData.Description;
        }

        public float Speed { get => _speed; }
        public float Handling { get => _handling; }
        public float Drag { get => _drag; }
        public int MaxHealth { get => _maxHealth; }
        public int EnergyCapacity { get => _energyCapacity; }
        public float FuelCapacity { get => _fuelCapacity; }
        public float FuelUsage { get => _fuelUsage; }
        public float Range { get => _range; }
        public float FireRate { get => _fireRate; }
        public float EnergyCost { get => _energyCost; }
        public string SpecialName { get => _specialName; }
        public string SpecialDescription { get => _specialDescription; }

        public List<float> SpeedModifier => _speedModifier;
        public List<float> handlingModifier => _handlingModifier;
        public List<float> dragModifier => _dragModifier;
        public List<int> healthModifier => _healthModifier;
        public List<int> energyCapacityModifier => _energyCapacityModifier;
        public List<float> energyGenerationModifier => _energuGenerationModifier;
        public List<float> fuelCapacityModifier => _fuelCapacityModifier;
        public List<float> fuelUsageModifier => _fuelUsageModifier;
    }
}
