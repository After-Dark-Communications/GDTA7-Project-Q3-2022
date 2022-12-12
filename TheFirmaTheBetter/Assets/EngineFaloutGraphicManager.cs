using EventSystem;
using ShipParts.Engines;
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

    private void Start()
    {
        playerNumber = GetComponent<Engine>().ShipBuilder.PlayerNumber;
    }

    private void OnEnable()
    {
        Channels.Movement.OnShipEngineActiveChanged += OnShipEngineActiveChanged;
    }

    private void OnDisable()
    {
        Channels.Movement.OnShipEngineActiveChanged -= OnShipEngineActiveChanged;
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
