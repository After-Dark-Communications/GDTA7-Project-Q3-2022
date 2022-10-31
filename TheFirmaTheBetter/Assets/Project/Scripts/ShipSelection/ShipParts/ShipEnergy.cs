using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShipParts
{
    public class ShipEnergy
    {
        private int playerNumber;
        private float currentEnergyAmount;
        private int maxEnergyAmount;

        public ShipEnergy(int playerNumber, ShipStats shipStats)
        {
            this.playerNumber = playerNumber;

            Subscribe();

            SetEnergy(shipStats);
        }

        public void Subscribe()
        {
            Channels.OnEnergyUsed += OnEnergyUsed;
            Channels.OnRefillEnergy += OnEnergyRefilled;
        }

        public void Unsubscribe()
        {
            Channels.OnEnergyUsed -= OnEnergyUsed;
            Channels.OnRefillEnergy -= OnEnergyRefilled;
        }


        private void SetEnergy(ShipStats shipStats)
        {
            maxEnergyAmount = shipStats.EnergyCapacity;
            currentEnergyAmount = maxEnergyAmount;
        }

        private void OnEnergyRefilled(int playerNumber, int refillAmount)
        {
            if (IsThisNotMe(playerNumber))
                return;

            currentEnergyAmount += refillAmount;

            if (currentEnergyAmount > maxEnergyAmount)
            {
                currentEnergyAmount = maxEnergyAmount;
            }
            Channels.OnEnergyChanged?.Invoke(playerNumber, (float)currentEnergyAmount / maxEnergyAmount);
        }

        public void UpdateEnergy(ShipStats shipStats)
        {
            SetEnergy(shipStats);
        }

        private void OnEnergyUsed(int playerNumber, int usedAmount)
        {
            if (IsThisNotMe(playerNumber)) return;
            if (maxEnergyAmount == 0) return;

            currentEnergyAmount -= usedAmount;

            float energypersentage = (float)currentEnergyAmount / maxEnergyAmount;

            Channels.OnEnergyChanged(playerNumber, energypersentage);
        }

        private bool IsThisNotMe(int playerNumber)
        {
            if (this.playerNumber != playerNumber)
                return true;

            return false;
        }

        public float CurrentEnergyAmount => currentEnergyAmount;
    }
}
