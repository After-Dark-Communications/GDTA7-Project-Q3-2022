using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts.Cores
{
    [CreateAssetMenu(fileName = "NewCoreData", menuName = "Astrofire/Part/Create new CoreData")]
    public class CoreData : PartData
    {
        [Header("CoreStats")]
        [SerializeField]
        private int health;

        [SerializeField]
        private int energyCapacity;

        [SerializeField]
        private float fuelCapacity;

        public int Health => health;

        public int EnergyCapacity => energyCapacity;

        public float FuelCapacity => fuelCapacity;
    }
}