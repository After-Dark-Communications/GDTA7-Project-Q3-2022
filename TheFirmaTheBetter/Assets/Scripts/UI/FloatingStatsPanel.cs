using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStatsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToFollow;

    [SerializeField]
    private int xOffsetPixels;

    [SerializeField]
    private int yOffsetPixels;

    private void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectToFollow.transform.position);
        transform.position = new Vector3(screenPosition.x + xOffsetPixels, screenPosition.y +yOffsetPixels);
    }
}
