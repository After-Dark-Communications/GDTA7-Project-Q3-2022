using ShipParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts.Engines
{
    [CreateAssetMenu(fileName = "NewEngineData", menuName = "Part/Create new EngineData")]
    public class EngineData : PartData
    {
        [Header("EngineStats")]
        [SerializeField]
        private float speed;

        [SerializeField]
        private float handling;

        [SerializeField]
        private float fuelUsage;

        public float Speed => speed;

        public float Handling => handling;

        public float FuelUsage => fuelUsage;
    }
}