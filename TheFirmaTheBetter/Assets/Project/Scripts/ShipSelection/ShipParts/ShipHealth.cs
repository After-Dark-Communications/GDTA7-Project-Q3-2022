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

        private bool isAlive;

        public ShipHealth(int playerNumber, ShipStats shipStats)
        {//TODO: reinstantiate ShipHealth when starting game (playerspawned?)
            this.playerNumber = playerNumber;
            ResetHealth(shipStats);

            Channels.OnPlayerTakeDamage += TakeDamage;
            Channels.OnPlayerHealed += Heal;
        }

        public void Unsubscribe()
        {
            Channels.OnPlayerTakeDamage -= TakeDamage;
            Channels.OnPlayerHealed -= Heal;
        }

        public void ResetHealth(ShipStats shipStats)
        {
            maxHealth = shipStats.MaxHealth;
            currentShipHealth = maxHealth;
            isAlive = true;
            UpdateHealthBar(playerNumber);
        }

        private void Heal(int healthIncreaseAmount, int playerNumber)
        {
            //check pl number
            //increse health by persentage
            if (this.playerNumber != playerNumber)
                return;

            currentShipHealth += (float)healthIncreaseAmount / 100 * maxHealth;

            if (currentShipHealth >= maxHealth)
                currentShipHealth = maxHealth;

            UpdateHealthBar(playerNumber);

        }

        public void TakeDamage(ShipBuilder shipBuilder, int amount, int damagerIndex)
        {
            if (!isAlive || shipBuilder == null)
                return;
            if (playerNumber != shipBuilder.PlayerNumber)
                return;

            currentShipHealth -= amount;

            if (currentShipHealth <= 0)
            {
                currentShipHealth = 0;
                isAlive = false;
                Channels.OnPlayerBecomesDeath?.Invoke(shipBuilder, damagerIndex);
            }

            UpdateHealthBar(playerNumber);
        }

        private void UpdateHealthBar(int playerNumber)
        {
            Channels.OnHealthChanged?.Invoke(playerNumber, currentShipHealth / maxHealth);
        }

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentShipHealth => currentShipHealth;
    }
}
