using EventSystem;
using ShipParts;
using ShipParts.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShipParts
{
    public class ShipHealth
    {
        private readonly int playerNumber;

        private float currentShipHealth;

        private float maxHealth;

        public ShipHealth(int playerNumber, ShipStats shipStats)
        {//TODO: reinstantiate ShipHealth when starting game (playerspawned?)
            this.playerNumber = playerNumber;
            ResetHealth(shipStats);

            Channels.OnPlayerTakeDamage += TakeDamage;
            Channels.OnPlayerHealed += Heal;
        }

        private void Heal(int healthIncreaseAmount, int playerNumber)
        {
            //check pl number
            //increse health
            if (this.playerNumber != playerNumber)
                return;

            currentShipHealth += healthIncreaseAmount;

            if (currentShipHealth >= maxHealth)
                currentShipHealth = maxHealth;

            UpdateHealthBar(playerNumber);

        }

        private void UpdateHealthBar(int playerNumber)
        {
            Channels.OnHealthChanged(playerNumber, currentShipHealth / maxHealth);
        }

        private void ResetHealth(ShipStats shipStats)
        {
            maxHealth = shipStats.MaxHealth;
            currentShipHealth = maxHealth;
        }

        public void TakeDamage(ShipBuilder shipBuilder, int amount, int damagerIndex)
        {
            if (shipBuilder == null)
            { return; }
            if (playerNumber != shipBuilder.PlayerNumber)
                return;

            currentShipHealth -= amount;

            if (currentShipHealth <= 0)
            {
                currentShipHealth = 0;
                Channels.OnPlayerBecomesDeath?.Invoke(shipBuilder, damagerIndex);
            }

            UpdateHealthBar(playerNumber);
        }

        public void UpdateHealth(ShipStats shipStats)
        {
            ResetHealth(shipStats);
        }

        public void Unsubscribe()
        {
            Channels.OnPlayerTakeDamage -= TakeDamage;
            Channels.OnPlayerHealed -= Heal;
        }

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentShipHealth => currentShipHealth;
    }
}
