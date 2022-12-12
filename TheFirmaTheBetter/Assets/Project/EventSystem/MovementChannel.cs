using System;
using UnityEngine;

namespace EventSystem
{
    public class MovementChannel
    {
        public delegate void ShipMove(Vector2 moveVector, int playerNumber);
        public delegate void HeatChanged(float heat, int playerNumber);

        public ShipMove OnShipMove;
        public HeatChanged OnHeatChanged;
    }
}