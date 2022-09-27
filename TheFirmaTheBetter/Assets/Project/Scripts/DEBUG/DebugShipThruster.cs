using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugShipThruster : MonoBehaviour
{
    [SerializeField]
    private GameObject thrust;
    [SerializeField]
    private float LowStrength = 1f,HighStrength = 1f;
    private void Start()
    {
        transform.root.GetComponent<ShipInputHandler>().OnPlayerMove.AddListener(OnMove);
    }

    private void OnMove(Vector2 move)
    {
        if (move != Vector2.zero)
        {
            thrust.SetActive(true);
            //Gamepad.current.SetMotorSpeeds(LowStrength,HighStrength);
        }
        else
        {
            thrust.SetActive(false);
            //Gamepad.current.SetMotorSpeeds(0,0);
        }
    }
}
