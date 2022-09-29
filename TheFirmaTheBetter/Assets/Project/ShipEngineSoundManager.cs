using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ShipParts.Ship;
using EventSystem;

public class ShipEngineSoundManager : MonoBehaviour
{
    private StudioEventEmitter emitter;
    [Range(0f, 100f)]
    private float rpm;
    private int playerNumber;

    [Tooltip("The multiplier used for manipulating the RPM parameter")]
    [SerializeField]
    private float RPMSpeed;

    // Start is called before the first frame update
    void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        try
        {
            playerNumber = transform.GetChild(1).GetComponent<ShipBuilder>().PlayerNumber;
        }
        catch
        {

        }
        Channels.Movement.OnShipMove += SetRPM;
    }


    private void SetRPM(Vector2 movement, int playerNumber)
    {
        if (this.playerNumber == playerNumber)
        {
            rpm = movement.x + movement.y;
            if (rpm < 0)
            {
                rpm = rpm * -1;
            }
            Debug.Log(rpm * RPMSpeed);
            emitter.SetParameter("RPM", rpm * RPMSpeed);
        }
    }

}
