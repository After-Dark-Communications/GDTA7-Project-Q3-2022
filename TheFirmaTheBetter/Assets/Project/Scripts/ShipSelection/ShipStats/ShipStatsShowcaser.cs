using EventSystem;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace ShipSelection
{
    public class ShipStatsShowcaser : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> statsGameObjectList = new List<GameObject>();
        [SerializeField]
        private int playerNumber;
        private List<string> labelStatNames = new List<string> { "Ship Stats", "Weapon Stats", "Special Stats" };
        [SerializeField]
        private TMP_Text labelStats;

        private void Awake()
        {
            DisableAllStats();
            EnableOneStat(0);

            Channels.Input.OnSelectionBarUpAndDownNaviagtedInput += ShowStatsFromIndex;
        }
        private void Start()
        {
            playerNumber = GetComponentInParent<PlayerSelectionScreen>().PlayerNumber;
        }

        private void EnableOneStat(int index)
        {
            switch (index)
            {
                case 0:
                    statsGameObjectList[0].SetActive(true);
                    ChangeLabelStats(0);
                    break;
                case 1:
                    statsGameObjectList[1].SetActive(true);
                    ChangeLabelStats(1);
                    break;
                case 2:
                    statsGameObjectList[2].SetActive(true);
                    ChangeLabelStats(2);
                    break;
                default:
                    break;
            }
            Channels.OnEnabledStatGameObject?.Invoke(index, playerNumber);

        }

        private void DisableAllStats()
        {
            foreach (GameObject statGameObject in statsGameObjectList)
            {
                statGameObject.SetActive(false);
            }
        }

        private void ShowStatsFromIndex(int playerIndex, int selectionBarIndex)
        {
            if (playerNumber != playerIndex)
                return;
            DisableAllStats();
            switch (selectionBarIndex)
            {
                case <= 1:
                    EnableOneStat(0);
                    break;
                case 2:
                    EnableOneStat(1);
                    break;
                case 3:
                    EnableOneStat(2);
                    break;
                default:
                    EnableOneStat(0);
                    break;
            }
        }
        private void ChangeLabelStats(int index)
        {
            labelStats.text = labelStatNames[index].ToString();
        }

        private void OnDisable()
        {
            Channels.Input.OnSelectionBarUpAndDownNaviagtedInput -= ShowStatsFromIndex;
        }

    }
}