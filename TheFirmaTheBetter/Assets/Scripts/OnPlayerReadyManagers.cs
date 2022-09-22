using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerReadyManagers : MonoBehaviour
{
    private void OnEnable()
    {
        Channels.OnEveryPlayerReady += OnEveryPlayerReady;   
    }

    private void OnEveryPlayerReady()
    {
        SceneSwitchManager.SwitchToNextScene();
    }
}
