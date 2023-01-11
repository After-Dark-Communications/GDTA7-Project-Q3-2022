using EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class HeatMeterVisual : ShipStatBar
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        Channels.Movement.OnHeatChanged += ChangeHeatBar;
    }

    private void OnDisable()
    {
        Channels.Movement.OnHeatChanged -= ChangeHeatBar;
    }
    private void ChangeHeatBar(float heat, float min, float max, int playerNumber)
    {
        if (PlayerIndex == playerNumber)
        {
            _slider.value = heat.Remap(min, max, 0, 1);
        }
    }
}
