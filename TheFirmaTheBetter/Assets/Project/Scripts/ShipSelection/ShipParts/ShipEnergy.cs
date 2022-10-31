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

            Channels.OnEnergyUsed += OnEnergyUsed;
            Channels.OnRefillEnergy += OnEnergyRefilled;

            SetEnergy(shipStats);
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
            UpdateEnergy();
        }

        private void OnEnergyUsed(int playerNumber, int usedAmount)
        {
            if (IsThisNotMe(playerNumber)) return;
            if (maxEnergyAmount == 0) return;

            currentEnergyAmount -= usedAmount;

            UpdateEnergy();
        }

        public void ResetEnergy(ShipStats shipStats)
        {
            SetEnergy(shipStats);
            UpdateEnergy();
        }

        private void UpdateEnergy()
        {
            float energypersentage = currentEnergyAmount / maxEnergyAmount;
            Channels.OnEnergyChanged?.Invoke(playerNumber, energypersentage);
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
