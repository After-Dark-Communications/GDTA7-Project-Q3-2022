using System;
using UnityEngine;

public class Healthbar : Fillbar
{
    [SerializeField]
    private int playerIndex;

    protected Action<int, float> onValueUpdated;

    private void Awake()
    {
        onValueUpdated += UpdateHealthbar;
    }

    private void Start()
    {
        Channels.OnHealthChanged?.Invoke(1, 0.2f);
    }

    private void OnDestroy()
    {
        onValueUpdated -= UpdateHealthbar;
    }

    public void UpdateHealthbar(int playerIndex, float healthPrecentage)
    {
        if (playerIndex == this.playerIndex)
        {
            base.UpdateFill(healthPrecentage);
        }
    }
}
