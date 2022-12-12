using UnityEngine;

namespace ShipParts.Engines
{
    internal class HeatOMeterEngine : Engine
    {
        private const float _maxHeat = 10, _heatThreshold = 7.5f;
        private float _currentHeat = 0f;

        public override void MoveShip(Vector2 move)
        {
            if (CanMove == true && MoveValue != Vector2.zero)
            {
                _currentHeat += Time.deltaTime;
                if (_currentHeat >= _maxHeat)
                {
                    CanMove = false;
                }

            }
            base.MoveShip(move);
        }

        protected override void Update()
        {
            Debug.Log("Can Move: " + CanMove);
            if (CanMove == false || MoveValue == Vector2.zero)
            {
                _currentHeat -= Time.deltaTime;
                if (_currentHeat < 0)
                {
                    _currentHeat = 0;
                }
                if (CanMove == false && _currentHeat <= _heatThreshold)
                {
                    CanMove = true;
                }
            }
            base.Update();
        }
    }
}
