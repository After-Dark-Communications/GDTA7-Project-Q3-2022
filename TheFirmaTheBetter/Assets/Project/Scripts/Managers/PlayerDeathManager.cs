using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    private void Awake()
    {
        Channels.OnPlayerBecomesDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath(ShipBuilder shipBuilderThatDied)
    {
        shipBuilderThatDied.gameObject.SetActive(false);
    }
}
