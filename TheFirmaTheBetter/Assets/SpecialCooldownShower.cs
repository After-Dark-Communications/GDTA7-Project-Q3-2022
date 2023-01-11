using EventSystem;
using ShipParts;
using ShipParts.Ship;
using ShipParts.Specials;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCooldownShower : MonoBehaviour
{
    [SerializeField]
    private Color notAvailableColor;

    private Color availableColor;

    private Image abilityIconImage;

    private int playerIndex;

    private void Awake()
    {
        Channels.OnPlayerBarsLoaded += OnPlayerBarsLoaded;
        Channels.OnSpecialUsed += OnSpecialUsed;
        Channels.OnSpecialReady += ShowSpecialIcon;

        abilityIconImage = GetComponent<Image>();
        availableColor = abilityIconImage.color;
    }

    private void ShowSpecialIcon(ShipBuilder shipBuilder)
    {
        if (playerIndex != shipBuilder.PlayerNumber)
            return;

        abilityIconImage.color = availableColor;
    }

    private void OnSpecialUsed(ShipBuilder shipBuilderThatUsedSpecial)
    {
        if (playerIndex != shipBuilderThatUsedSpecial.PlayerNumber)
            return;

        abilityIconImage.color = notAvailableColor;
    }

    private void OnDestroy()
    {
        Channels.OnPlayerBarsLoaded -= OnPlayerBarsLoaded;
        Channels.OnSpecialUsed -= OnSpecialUsed;
        Channels.OnSpecialReady -= ShowSpecialIcon;
    }

    private void OnPlayerBarsLoaded(ShipBuilder shipBuilder)
    {
        if (shipBuilder.PlayerNumber != playerIndex)
            return;

        foreach (Part shipPart in shipBuilder.SelectedParts)
        {
            if (shipPart is not SpecialAbility)
                continue;

            abilityIconImage.sprite = shipPart.PartIcon;
            break;
        }
    }

    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }
}
