using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EndStatsData
{
    private int playerIndex;
    private GameObject shipObject;
    private float timeSurvived;
    private int playersKilled;


    public EndStatsData(int playerIndex, GameObject shipObject, float timeSurvived, int playersKilled)
    {
        this.playerIndex = playerIndex;
        this.shipObject = shipObject;
        this.timeSurvived = timeSurvived;
        this.playersKilled = playersKilled;
    }

    public int PlayerIndex { get { return playerIndex; } }
    public GameObject ShipObject { get { return shipObject; } }
    public float TimeSurvived { get { return timeSurvived; } }
    public int PlayersKilled { get { return playersKilled; } }
}