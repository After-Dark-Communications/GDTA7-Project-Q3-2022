using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using ShipSelection;
using UnityEngine;

public class DamageIndicatorLightUp : MonoBehaviour
{
    private ShipInfo _ShipInfo;

    private Animator animator;

    [SerializeField] int currentPlayerNumber;

    private void OnEnable()
    {
        _ShipInfo = GetComponentInParent<ShipInfo>();
        currentPlayerNumber = _ShipInfo.PlayerNumber;

        animator = GetComponent<Animator>();

        Channels.OnPlayerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnPlayerTakeDamage(ShipBuilder hittedBuilder, int damageAmount, int indexOfPlayerThatShotTheBullet)
    {
        if (hittedBuilder.PlayerNumber == currentPlayerNumber)
        {
            animator.Play("Base Layer.DamageFlash", -1, 0);
        }
        
    }

    private void OnDisable()
    {
        Channels.OnPlayerTakeDamage -= OnPlayerTakeDamage;
    }
}
