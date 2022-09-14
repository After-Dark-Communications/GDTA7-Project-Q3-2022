using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : Part
{
    [SerializeField]
    private EngineData engineData;
    public override string PartName => "Engine";

    //IF YOU OVERRIDE PART'S AWAKE, BE SURE TO USE BASE.AWAKE() SO THAT IT HAS KNOWLEDGE OF ITS ROOT

    private void Start()
    {
        if (RootInputHanlder != null)
        {
            RootInputHanlder.OnPlayerMove.AddListener(MoveShip);
        }
    }

    private void MoveShip(Vector2 move)
    {//when starting to move, increase T and lerp towards top speed
     //when stopping, decrease T and lerp towards 0 speed

        if (move != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.y), GlobalUp.UP.up);
            ShipRoot.rotation = Quaternion.RotateTowards(ShipRoot.rotation, toRotation, engineData.Handling * Time.deltaTime);
        }
        DEBUG_Engine_Forward(new Vector3(move.x, 0, move.y).magnitude);
    }

    //TODO: Make forward and turning based on camera forward instead of ship forward
    public void DEBUG_Engine_Forward(float throttle)
    {
        Vector3 forward = ShipRoot.transform.forward;
        forward.y = 0;
        ShipRoot.transform.position += forward.normalized * throttle * (engineData.Speed * Time.deltaTime);
    }

    public void DEBUG_Engine_Turn(float direction)
    {
        ShipRoot.transform.Rotate(0, direction * engineData.Handling * Time.deltaTime, 0, Space.World);
    }
}