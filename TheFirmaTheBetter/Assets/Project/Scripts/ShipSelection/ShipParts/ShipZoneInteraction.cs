using Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ShipParts.Ship;
using EventSystem;
using ShipParts.Cores;
using ShipSelection.Stats;

namespace ShipParts
{
    public class ShipZoneInteraction : MonoBehaviour, IHaveZoneInteraction
    {
        private const float ZoneInterval = 0.75f;
        private const float RefillPersentage = 20;

        private int playerNumber;
        private ShipResources shipResources;
        private ShipBuilder shipBuilder;
        private Core selectedCore;

        private float currentZoneInterval = 0;


        private void Awake()
        {
            shipResources = GetComponent<ShipResources>();

            shipBuilder = GetComponent<ShipBuilder>();
            playerNumber = shipBuilder.PlayerNumber;
        }

        public void HandleZoneEnterInteraction(Zone enteredZone)
        {
            if (enteredZone is EnergyZone)
            {
                Channels.OnChangeFireMode?.Invoke(false, playerNumber);
            }

            if (enteredZone is EnergyDepletionWall)
            {
                Channels.OnEnergyUsed?.Invoke(playerNumber, Mathf.RoundToInt(shipResources.CurrentEnergyAmount + 0.5f));
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
                if (shipBuilder.IsTypeCore<BatteryCore>())
                    return;
                
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
