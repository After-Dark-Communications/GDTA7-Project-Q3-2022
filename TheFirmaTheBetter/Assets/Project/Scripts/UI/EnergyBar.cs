using UnityEngine;

public class EnergyBar : ShipStatBar
{
    private void OnEnable()
    {
        Channels.OnEnergyChanged += UpdateStatbar;
    }

    private void OnDisable()
    {
        Channels.OnEnergyChanged -= UpdateStatbar;
    }
}
