using UnityEngine;

public class Fuelbar : Fillbar
{
    [SerializeField]
    private int playerIndex;

    private void Awake()
    {
        Channels.OnHealthChanged += UpdateFuelbar;
    }

    private void Start()
    {
        Channels.OnHealthChanged?.Invoke(1, 0.2f);
    }

    private void OnDestroy()
    {
        Channels.OnHealthChanged -= UpdateFuelbar;
    }

    public void UpdateFuelbar(int playerIndex, float fuelPrecentage)
    {
        if (playerIndex == this.playerIndex)
        {
            base.UpdateFill(fuelPrecentage);
        }
    }
}
