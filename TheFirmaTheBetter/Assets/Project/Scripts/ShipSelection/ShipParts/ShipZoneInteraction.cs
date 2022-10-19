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
                Channels.OnRefillEnergy?.Invoke(playerNumber, shipResources.ShipStats.EnergyCapacity);
                Channels.OnChangeFireMode?.Invoke(false);
            }
            else if (enteredZone is ScoringZone)
            {
                Channels.KingOfTheHill.OnKingOfTheHillScore?.Invoke(playerNumber, 10);
            }
        }

        public void HandleZoneExitInteraction(Zone enteredZone)
        {
            if (enteredZone is EnergyZone)
            {
                Channels.OnChangeFireMode?.Invoke(true);
            }
            else if (enteredZone is ScoringZone)
            {
                Channels.KingOfTheHill.OnKingOfTheHillLeaveZone?.Invoke(playerNumber);
            }
        }

        public void HandleZoneStayInteraction(Zone enteredZone)
        {
            if (enteredZone is ScoringZone)
            {
                Channels.KingOfTheHill.OnKingOfTheHillEnterZone?.Invoke(playerNumber);
            }
        }
    }
}
