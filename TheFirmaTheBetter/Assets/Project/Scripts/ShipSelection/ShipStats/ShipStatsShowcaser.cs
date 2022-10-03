using EventSystem;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipStatsShowcaser : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> statsGameObjectList = new List<GameObject>();
    [SerializeField]
    private int playerNumber;

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
                break;
            case 1: 
                statsGameObjectList[1].SetActive(true); 
                break ;
            case 2:
                statsGameObjectList[2].SetActive(true);
                break;
            default:
                break;
        }
        Channels.OnEnabledStatGameObject.Invoke(index,playerNumber);

    }

    private void DisableAllStats()
    {
        foreach (GameObject statGameObject in statsGameObjectList)
        {
            statGameObject.SetActive(false);
        }
    }

    private void ShowStatsFromIndex(int playerIndex,int selectionBarIndex)
    {
        if (playerNumber != playerIndex)
            return;
        DisableAllStats();
        switch (selectionBarIndex)
        {
            case <=1:
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

    private void OnDisable()
    {
        Channels.Input.OnSelectionBarUpAndDownNaviagtedInput -= ShowStatsFromIndex;
    }

}
