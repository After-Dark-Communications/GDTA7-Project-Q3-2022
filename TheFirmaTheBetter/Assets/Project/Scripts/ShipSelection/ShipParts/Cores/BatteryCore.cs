using EventSystem;
using ShipParts.Ship;
using UnityEngine;

namespace ShipParts.Cores
{
    public class BatteryCore : Core
    {
        private const float Interval = 1f;
        private const float RefillPersentage = 5f;

        private float currentTime = 0;

        private ShipResources resources;
        private ShipBuilder builder;

        private void Start()
        {
            resources = GetComponentInParent<ShipResources>();
            builder = GetComponentInParent<ShipBuilder>();
        }

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (currentTime < Interval)
                return;

            currentTime = 0;

            //wizard stuff casting floats at the enemies
            float newValue = (float)resources.ShipStats.EnergyCapacity / 100 * RefillPersentage;

            Channels.OnRefillEnergy(builder.PlayerNumber, (int)newValue);
        }
    }
}
