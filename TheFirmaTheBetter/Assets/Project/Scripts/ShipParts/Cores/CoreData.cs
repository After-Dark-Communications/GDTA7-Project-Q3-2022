using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts.Cores
{
    [CreateAssetMenu(fileName = "NewCoreData", menuName = "Part/Create new CoreData")]
    public class CoreData : PartData
    {
        [Header("CoreStats")]
        [SerializeField]
        private int health;

        [SerializeField]
        private float ammoCapacity;

        [SerializeField]
        private float fuelCapacity;

        public int Health => health;

        public float AmmoCapacity => ammoCapacity;

        public float FuelCapacity => fuelCapacity;
    }
}