using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShipThruster : MonoBehaviour
{
    [SerializeField]
    private GameObject thrust;
    private void Start()
    {
        transform.root.GetComponent<ShipInputHandler>().OnPlayerMove.AddListener(OnMove);
    }

    private void OnMove(Vector2 move)
    {
        if (move != Vector2.zero)
        {
            thrust.SetActive(true);
        }
        else
        {
            thrust.SetActive(false);
        }
    }
}
