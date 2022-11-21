using EventSystem;
using UnityEngine;

namespace UI
{
    public class EnergyBar : ShipStatBar
    {
        private void Awake()
        {
            Channels.OnEnergyChanged += UpdateStatbar;
        }

        private void OnDestroy()
        {
            Channels.OnEnergyChanged -= UpdateStatbar;
        }
    }
}