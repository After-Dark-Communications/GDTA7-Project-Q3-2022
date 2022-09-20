using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class DEBUG_ShipMovement : MonoBehaviour
{
    [SerializeField]
    public float EngineSpeed = 1f;
    [SerializeField]
    public float AccelerationSpeed = 1f;
    [SerializeField]
    public float RotationSpeed = 1f;
    [SerializeField]
    public int HullIntegrity = 1;


    private int _CurrentHullIntegrity;
    private float _OldRotation, _CurrentRotation;
    private float _AccelT = 0f;
    private Transform _Parent;

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        { return; }
#endif

        _Parent = transform.root;
        //Debug.Log($"_Parent:{_Parent.name}");

        ShipInputHandler handler = _Parent.GetComponent<ShipInputHandler>();
        if (handler != null)
        {
            //handler.OnPlayerForwardBackward.AddListener(DEBUG_Engine_Forward);
            //handler.OnPlayerLeftRight.AddListener(DEBUG_Engine_Turn);
            handler.OnPlayerMove.AddListener(DEBUG_Engine_Move);
            handler.OnPlayerShoot.AddListener(DEBUG_Weapon);
            handler.OnPlayerSpecial.AddListener(DEBUG_Special);
            handler.OnPlayerMoveUp.AddListener(DEBUG_Engine_Up);
            handler.OnPlayerMoveDown.AddListener(DEBUG_Engine_Down);

        }
        else
        {
            //Debug.LogError($"_Parent({_Parent.name}) does not have a ShipInputHanlder component! disabling...");
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        ShipInputHandler handler = _Parent.GetComponent<ShipInputHandler>();
        if (handler != null)
        {
            //handler.OnPlayerForwardBackward.RemoveListener(DEBUG_Engine_Forward);
            //handler.OnPlayerLeftRight.RemoveListener(DEBUG_Engine_Turn);
            handler.OnPlayerShoot.RemoveListener(DEBUG_Weapon);
            handler.OnPlayerSpecial.RemoveListener(DEBUG_Special);
            handler.OnPlayerMoveUp.RemoveListener(DEBUG_Engine_Up);
            handler.OnPlayerMoveDown.RemoveListener(DEBUG_Engine_Down);
        }
    }


    private void DEBUG_Engine_Move(Vector2 move)
    {//when starting to move, increase T and lerp towards top speed
     //when stopping, decrease T and lerp towards 0 speed
        
        if (move != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.y), GlobalUp.UP.up);
            _Parent.rotation = Quaternion.RotateTowards(_Parent.rotation, toRotation, RotationSpeed * Time.deltaTime);
        }
        DEBUG_Engine_Forward(new Vector3(move.x, 0, move.y).magnitude);
    }

    //TODO: Make forward and turning based on camera forward instead of ship forward
    public void DEBUG_Engine_Forward(float throttle)
    {
        Vector3 forward = _Parent.transform.forward;
        forward.y = 0;
        _Parent.transform.position += forward.normalized * throttle * (EngineSpeed * Time.deltaTime);
    }

    public void DEBUG_Engine_Turn(float direction)
    {
        _Parent.transform.Rotate(0, direction * RotationSpeed * Time.deltaTime, 0, Space.World);
    }

    public void DEBUG_Engine_Up(ButtonStates state)
    {
        DEBUG_InputBools(state);
        if (state.HasFlag(ButtonStates.STARTED))//started
        {
            //Debug.Log("Go UP");
        }
    }

    public void DEBUG_Engine_Down(ButtonStates state)
    {
        DEBUG_InputBools(state);
        if (state.HasFlag(ButtonStates.STARTED))//started
        {
            //Debug.Log("Go DOWN");
        }
    }

    public void DEBUG_Hull(float damage)
    {

    }

    public void DEBUG_Weapon(ButtonStates state)
    {
        DEBUG_InputBools(state);
    }

    public void DEBUG_Special(ButtonStates state)
    {
        DEBUG_InputBools(state);
    }

    private void DEBUG_InputBools(ButtonStates state)
    {
       // Debug.Log($"started:{state.HasFlag(ButtonStates.STARTED)}, performed:{state.HasFlag(ButtonStates.PERFORMED)}, canceled:{state.HasFlag(ButtonStates.CANCELED)}");
    }
}
