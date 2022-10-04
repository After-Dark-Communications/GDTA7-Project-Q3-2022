using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EndStatsData
{
    private int timeSurvived;
    private int playersKilled;

    public EndStatsData(int timeSurvived, int playersKilled)
    {
        this.timeSurvived = timeSurvived;
        this.playersKilled = playersKilled;
    }

    public float TimeLasted { get { return timeSurvived; } }
    public float PlayersKilled { get { return playersKilled; } }
}