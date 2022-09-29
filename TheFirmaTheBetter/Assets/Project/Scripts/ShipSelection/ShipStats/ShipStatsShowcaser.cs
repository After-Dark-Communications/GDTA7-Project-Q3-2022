using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatsShowcaser : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> statsGameObjectList = new List<GameObject>();

    private void Awake()
    {
        foreach (GameObject statGameObject in statsGameObjectList)
        {
            statGameObject.SetActive(false);
        }
        statsGameObjectList[0].SetActive(true);
    }

}
