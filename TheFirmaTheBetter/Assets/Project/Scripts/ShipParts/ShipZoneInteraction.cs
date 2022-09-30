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
        private int playerNumber;
        private ShipResources shipResources;

        private void Awake()
        {
            shipResources = GetComponent<ShipResources>();
            playerNumber = GetComponent<ShipBuilder>().PlayerNumber;
        }

        public void HandleZoneEnterInteraction(Zone enteredZone)
        {
            if (enteredZone is EnergyZone)
            {
                Channels.OnRefillEnergy.Invoke(playerNumber, shipResources.ShipStats.EnergyCapacity);
                Channels.OnChangeFireMode.Invoke(false);
            }
        }

        public void HandleZoneExitInteraction(Zone enteredZone)
        {
            if (enteredZone is EnergyZone)
            {
                Channels.OnChangeFireMode.Invoke(true);
            }
        }
    }
}
