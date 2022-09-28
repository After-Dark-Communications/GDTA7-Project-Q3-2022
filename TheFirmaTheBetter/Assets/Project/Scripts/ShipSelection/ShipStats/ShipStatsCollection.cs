using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatsCollection : MonoBehaviour
{
    [SerializeField]
    private List<ShipStat> shipStats = new List<ShipStat>();

    private int currentSelectedIndex = 0;

    public List<ShipStat> ShipStats { get => shipStats; }

    public int CurrentSelectedIndex { get => currentSelectedIndex; }

    private void Start()
    {
        foreach (ShipStat shipStat in gameObject.GetComponentsInChildren<ShipStat>())
        {
            shipStats.Add(shipStat);
        }
    }
}
