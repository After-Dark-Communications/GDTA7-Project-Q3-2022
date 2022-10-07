using EventSystem;
using ShipParts;
using ShipParts.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ShipParts
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
        }

        private void ResetHealth(ShipStats shipStats)
        {
            maxHealth = shipStats.MaxHealth;
            currentShipHealth = maxHealth;
        }

        public void TakeDamage(ShipBuilder shipBuilder, int amount, int damagerIndex)
        {
            if (playerNumber != shipBuilder.PlayerNumber)
                return;

            currentShipHealth -= amount;

            if (currentShipHealth <= 0)
            {
                currentShipHealth = 0;
                Channels.OnPlayerBecomesDeath?.Invoke(shipBuilder, damagerIndex);
            }

            Channels.OnHealthChanged(playerNumber, currentShipHealth / maxHealth);
        }

        public void UpdateHealth(ShipStats shipStats)
        {
            ResetHealth(shipStats);
        }

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentShipHealth => currentShipHealth;
    }
}
