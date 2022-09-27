using System;
using UnityEngine;

public abstract class ShipStatBar : FillBar
{
    [SerializeField]
    private int playerIndex;

    protected virtual void UpdateStatbar(int playerIndex, float statPrecentage)
    {
        if (playerIndex == this.playerIndex)
        {
            UpdateFill(statPrecentage);
        }
    }

    public int PlayerIndex
    {
        get { return playerIndex; }
        set { if (value >= 1 && value <= 4) playerIndex = value; }
    }
}