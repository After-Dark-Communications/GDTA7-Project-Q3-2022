using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ShipEngineSoundManager : MonoBehaviour
{
    private StudioEventEmitter emitter;
    [Range(0f, 100f)]
    private float rpm;
    private int playerNumber;

    [SerializeField]
    private float RPMSpeed;

    // Start is called before the first frame update
    void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        playerNumber = transform.GetChild(1).GetComponent<ShipBuilder>().PlayerNumber;
        Channels.Movement.OnShipMove += SetRPM;
    }


    private void SetRPM(Vector2 movement, int playerNumber)
    {
        if (this.playerNumber == playerNumber)
        {
            emitter.SetParameter("RPM", (movement.x + movement.y) * RPMSpeed);
        }
    }

}
