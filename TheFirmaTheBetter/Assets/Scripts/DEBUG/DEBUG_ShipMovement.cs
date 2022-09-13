using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_ShipMovement : MonoBehaviour
{
    [SerializeField]
    public float EngineSpeed = 1f;
    [SerializeField]
    public float RotationSpeed = 1f;
    [SerializeField]
    public int HullIntegrity = 1;
    [SerializeField]
    public string ShowOffValue = "normal string"; 


    private int _CurrentHullIntegrity;
    private float _OldRotation, _CurrentRotation;

    private Transform _Parent;

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        { return; }
#endif

        _Parent = transform.root;
        Debug.Log($"_Parent:{_Parent.name}");

        ShipInputHandler handler = _Parent.GetComponent<ShipInputHandler>();
        if (handler != null)
        {
            handler.OnPlayerForwardBackward.AddListener(DEBUG_Engine_Forward);
            handler.OnPlayerLeftRight.AddListener(DEBUG_Engine_Turn);
            handler.OnPlayerShoot.AddListener(DEBUG_Weapon);
            handler.OnPlayerSpecial.AddListener(DEBUG_Special);
            handler.OnPlayerMoveUp.AddListener(DEBUG_Engine_Up);
            handler.OnPlayerMoveDown.AddListener(DEBUG_Engine_Down);

        }
        else
        {
            Debug.LogError($"_Parent({_Parent.name}) does not have a ShipInputHanlder component! disabling...");
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        ShipInputHandler handler = _Parent.GetComponent<ShipInputHandler>();
        if (handler != null)
        {
            handler.OnPlayerForwardBackward.RemoveListener(DEBUG_Engine_Forward);
            handler.OnPlayerLeftRight.RemoveListener(DEBUG_Engine_Turn);
            handler.OnPlayerShoot.RemoveListener(DEBUG_Weapon);
            handler.OnPlayerSpecial.RemoveListener(DEBUG_Special);
            handler.OnPlayerMoveUp.RemoveListener(DEBUG_Engine_Up);
            handler.OnPlayerMoveDown.RemoveListener(DEBUG_Engine_Down);
        }
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

    public void DEBUG_Engine_Up(bool[] values)
    {
        DEBUG_InputBools(values);
        if (values[0] == true)//started
        {
            Debug.Log("Go UP");
        }
    }

    public void DEBUG_Engine_Down(bool[] values)
    {
        DEBUG_InputBools(values);
        if (values[0] == true)//started
        {
            Debug.Log("Go DOWN");
        }
    }

    public void DEBUG_Hull(float damage)
    {

    }

    public void DEBUG_Weapon(bool[] values)
    {
        DEBUG_InputBools(values);
        if (values[0] == true)//started
        {
            Debug.Log("Shoot on start");
        }
    }

    public void DEBUG_Special(bool[] values)
    {
        DEBUG_InputBools(values);
    }

    private void DEBUG_InputBools(bool[] values)
    {
        Debug.Log($"started:{values[0]}, performed:{values[1]}, canceled:{values[2]}");
    }
}
