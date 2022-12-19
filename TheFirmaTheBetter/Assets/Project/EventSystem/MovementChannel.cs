using System;
using UnityEngine;

namespace EventSystem
{
    public class MovementChannel
    {
        public delegate void ShipMove(Vector2 moveVector, int playerNumber);
        public delegate void ShipEngineActiveChanged(int playerNumber, bool canMove);
        public delegate void HeatChanged(float heat, float min, float max, int playerNumber);

        public ShipMove OnShipMove;
        public ShipEngineActiveChanged OnShipEngineActiveChanged;

        public HeatChanged OnHeatChanged;
    }
}