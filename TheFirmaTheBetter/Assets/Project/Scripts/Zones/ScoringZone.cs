using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipSelection;
using EventSystem;
using Zones;

public class ScoringZone : Zone
{
    [SerializeField]
    [Range(0, 100)]
    private int ScoreAmount;

    private void Start()
    {
        Channels.KingOfTheHill.OnKingOfTheHillEnterZone += PlayerEnterZone;
    }

    private void OnDestroy()
    {
        Channels.KingOfTheHill.OnKingOfTheHillEnterZone -= PlayerEnterZone;
    }

    public override void TriggerEffect(GameObject obj)
    {
        Debug.Log($"{obj.name} has entered");
    }

    public void PlayerEnterZone(int playerNumber)
    {
        Channels.KingOfTheHill.OnKingOfTheHillScore?.Invoke(playerNumber, ScoreAmount);
    }
}
