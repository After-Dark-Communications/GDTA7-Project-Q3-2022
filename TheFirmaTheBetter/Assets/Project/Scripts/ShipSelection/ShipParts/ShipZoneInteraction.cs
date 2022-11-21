using Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ShipParts.Ship;
using EventSystem;

namespace ShipParts
{
    public class ShipZoneInteraction : MonoBehaviour, IHaveZoneInteraction
    {
        private const float ZoneInterval = 0.75f;
        private const float RefillPersentage = 20;

        private int playerNumber;
        private ShipResources shipResources;
        private float currentZoneInterval = 0;


        private void Awake()
        {
            shipResources = GetComponent<ShipResources>();

            ShipBuilder shipBuilder = GetComponent<ShipBuilder>();
            playerNumber = shipBuilder.PlayerNumber;
        }

        public void HandleZoneEnterInteraction(Zone enteredZone)
        {
            if (enteredZone is EnergyZone)
            {
                Channels.OnChangeFireMode?.Invoke(false, playerNumber);
            }
        }

        public void HandleZoneExitInteraction(Zone enteredZone)
        {
            if (enteredZone is EnergyZone)
            {
                Channels.OnChangeFireMode?.Invoke(true, playerNumber);
                currentZoneInterval = 0;
            }
        }

        public void HandleZoneStayInteraction(Zone enteredZone)
        {
            if (enteredZone is EnergyZone)
            {
                currentZoneInterval += Time.deltaTime;

                if (currentZoneInterval < ZoneInterval)
                    return;

                currentZoneInterval = 0;

                float newValue = (float)shipResources.ShipStats.EnergyCapacity / 100 * RefillPersentage;
                Channels.OnRefillEnergy?.Invoke(playerNumber, (int)newValue);
            }
        }
    }
}
