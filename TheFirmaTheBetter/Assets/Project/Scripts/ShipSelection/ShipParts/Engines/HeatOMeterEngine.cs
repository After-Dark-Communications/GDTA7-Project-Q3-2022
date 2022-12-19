using EventSystem;
using UnityEngine;

namespace ShipParts.Engines
{
    internal class HeatOMeterEngine : Engine
    {
        //TODO: add event calling for heat change so that UI can be added and adjusted
        private const float _maxHeat = 10, _minHeat = 0f, _heatThreshold = 6f, _cooldownMultiplier = 2;
        private float _currentHeat = 0f;

        public override void MoveShip(Vector2 move)
        {
            if (CanMove == true && MoveValue != Vector2.zero)
            {
                _currentHeat += Time.deltaTime;
                Channels.Movement.OnHeatChanged?.Invoke(_currentHeat, _minHeat, _maxHeat, ShipBuilder.PlayerNumber);
                if (_currentHeat >= _maxHeat)
                {
                    CanMove = false;
                }

            }
            base.MoveShip(move);
        }

        protected override void Update()
        {
            if (CanMove == false || MoveValue == Vector2.zero)
            {
                _currentHeat -= (Time.deltaTime * _cooldownMultiplier);
                if (_currentHeat < _minHeat)
                {
                    _currentHeat = _minHeat;
                }
                if (CanMove == false && _currentHeat <= _heatThreshold)
                {
                    CanMove = true;
                }
                Channels.Movement.OnHeatChanged?.Invoke(_currentHeat, _minHeat, _maxHeat, ShipBuilder.PlayerNumber);
            }
            base.Update();
        }
    }
}
