using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShipParts
{
    public class ShipStats
    {
        //movement
        private float _speed;
        private float _handling;
        private float _drag;

        //damage
        private float _maxHealth;
        //ammo
        private float _ammoCapacity;
        private float _ammoGenerationRate;
        //fuel
        private float _fuelCapacity;
        private float _fuelUsage;

        private readonly List<float> _speedModifier;
        private readonly List<float> _handlingModifier;
        private readonly List<float> _dragModifier;
        private readonly List<float> _healthModifier;
        private readonly List<float> _ammoCapacityModifier;
        private readonly List<float> _ammoGenerationModifier;
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
            _ammoCapacity = 0;
            _fuelCapacity = 0;
            _maxHealth = 0;

            //set modifier based values
            _ammoGenerationRate = 0;
            //calculate drag
            _drag = 0;

            //initialize lists
            _speedModifier = new List<float>();
            _handlingModifier = new List<float>();
            _dragModifier = new List<float>();
            _healthModifier = new List<float>();
            _ammoCapacityModifier = new List<float>();
            _ammoGenerationModifier = new List<float>();
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
            _ammoCapacity = coreData.AmmoCapacity;
            _fuelCapacity = coreData.FuelCapacity;
            _maxHealth = coreData.Health;
        }

        public float Speed { get => _speed; }
        public float Handling { get => _handling; }
        public float Drag { get => _drag; }
        public float MaxHealth { get => _maxHealth; }
        public float AmmoCapacity { get => _ammoCapacity; }
        public float FuelCapacity { get => _fuelCapacity; }
        public float FuelUsage { get => _fuelUsage; }

        public List<float> SpeedModifier => _speedModifier;
        public List<float> handlingModifier => _handlingModifier;
        public List<float> dragModifier => _dragModifier;
        public List<float> healthModifier => _healthModifier;
        public List<float> ammoCapacityModifier => _ammoCapacityModifier;
        public List<float> ammoGenerationModifier => _ammoGenerationModifier;
        public List<float> fuelCapacityModifier => _fuelCapacityModifier;
        public List<float> fuelUsageModifier => _fuelUsageModifier;
    }
}
