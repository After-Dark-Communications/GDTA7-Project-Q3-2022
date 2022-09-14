using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Part : MonoBehaviour
{
    [SerializeField]
    private Image partIcon;
    [SerializeField]
    private Vector3 connectionPoint;

    protected Transform ShipRoot { get; private set; }
    protected ShipInputHandler RootInputHanlder { get; private set; }

    public abstract string PartName { get; }

    protected virtual void Awake()
    {
        ShipRoot = transform.root;
        RootInputHanlder = ShipRoot.GetComponent<ShipInputHandler>();
    }
}
