using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathObjects : MonoBehaviour
{
    [SerializeField]
    private Transform DeathHolder;
    private GameObject[] _OnDeathChildren;
    private ShipBuilder _shipBuilder;
    private void OnEnable()
    {
        Channels.OnPlayerSpawned += setup;
    }

    private void OnDisable()
    {
        Channels.OnPlayerSpawned -= setup;
        Channels.OnPlayerBecomesDeath -= OnPlayerDeath;
    }
    private void setup(GameObject shipBuilderObject, int playerNumber)
    {
        if (DeathHolder == null)
        { return; }
        _shipBuilder = transform.GetComponentInChildren<ShipBuilder>();
        if (_shipBuilder == null)
        {
            Debug.Log($"{transform.name} doesn't have shipBuilder");
            return;
        }
        Channels.OnPlayerBecomesDeath += OnPlayerDeath;
        _OnDeathChildren = new GameObject[DeathHolder.childCount];
        for (int i = 0; i < _OnDeathChildren.Length; i++)
        {
            GameObject child = DeathHolder.GetChild(i).gameObject;
            child.SetActive(false);
            _OnDeathChildren[i] = child;
        }
    }
    private void OnPlayerDeath(ShipBuilder shipBuilder, int killerIndex)
    {
        if (shipBuilder == null)
        { return; }
        if (shipBuilder.PlayerNumber == _shipBuilder.PlayerNumber)
        {
            for (int i = 0; i < _OnDeathChildren.Length; i++)
            {
                if (_OnDeathChildren[i] != null)
                {
                    _OnDeathChildren[i].SetActive(true);
                }
            }
        }
    }
}
