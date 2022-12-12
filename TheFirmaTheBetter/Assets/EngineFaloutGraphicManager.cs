using EventSystem;
using ShipParts.Engines;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventSystem.MovementChannel;

public class EngineFaloutGraphicManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> engineTransforms = new List<Transform>();

    private int playerNumber;

    private void OnEnable()
    {
        Channels.Movement.OnShipEngineActiveChanged += OnShipEngineActiveChanged;
        Channels.OnEveryPlayerReady += Setup;
    }


    private void OnDisable()
    {
        Channels.Movement.OnShipEngineActiveChanged -= OnShipEngineActiveChanged;
        Channels.OnEveryPlayerReady -= Setup;
    }
    private void Setup(int playersInGameCount)
    {
        if (gameObject.activeSelf == true)
        {
            playerNumber = transform.parent.GetComponent<ShipBuilder>().PlayerNumber;
        }
    }

    private void OnShipEngineActiveChanged(int playerNumber, bool canMove)
    {
        if (this.playerNumber != playerNumber)
            return;

        foreach (Transform transform in engineTransforms)
        {
            transform.gameObject.SetActive(canMove);
        }
    }
}
