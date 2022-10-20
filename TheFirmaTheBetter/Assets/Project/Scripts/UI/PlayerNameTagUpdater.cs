using EventSystem;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PlayerNameTagUpdater : MonoBehaviour
    {
        private const string baseString = "Player: ";

        private TMP_Text tmpTextField;

        private void Awake()
        {
            tmpTextField = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            PlayerSelectionScreen selectionScreen = GetComponentInParent<PlayerSelectionScreen>();
            tmpTextField.SetText($"Player: {selectionScreen.PlayerNumber + 1}");
        }
    }
}