using System;
using UnityEngine;

namespace EventSystem
{
    public class MovementChannel
    {
        public Action<Vector2, int> OnShipMove;
    }
}