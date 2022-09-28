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

            maxHealth = shipStats.MaxHealth;
            currentShipHealth = maxHealth;

            Channels.OnPlayerTakeDamage += TakeDamage;
        }

        public void TakeDamage(ShipBuilder shipBuilder, int amount)
        {
            if (playerNumber != shipBuilder.PlayerNumber)
                return;

            currentShipHealth -= amount;

            if (currentShipHealth <= 0)
            {
                currentShipHealth = 0;
                Channels.OnPlayerBecomesDeath?.Invoke(shipBuilder);
            }
            Channels.OnHealthChanged(playerNumber, currentShipHealth / maxHealth);

        }

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentShipHealth => currentShipHealth;
    }
}
