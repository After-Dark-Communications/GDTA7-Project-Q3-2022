using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipParts.Ship;
using EventSystem;
using ShipSelection;

public class KingOfTheHillStartManager : MonoBehaviour
{
    private List<int> playerNumbers;
    // Start is called before the first frame update
    void Start()
    {
        playerNumbers = new List<int>();
        foreach (Transform child in transform)
        {
            if (child.GetComponentInChildren<ShipBuilder>())
            {
                playerNumbers.Add(child.GetComponent<ShipInfo>().PlayerNumber);
            }
        }
        Channels.KingOfTheHill.OnKingOfTheHillStart?.Invoke(playerNumbers);
    }
}
