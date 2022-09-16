using Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Part : MonoBehaviour
{

    [SerializeField]
    private Image partIcon;

    [SerializeField]
    private ConnectionPointsCollection connectionPointCollection;

    public virtual string PartCategoryName => "part";

    public abstract bool IsMyType(Part part);
    public abstract bool IsMyConnectionType(ConnectionPoint connectionPoint);

    public ConnectionPointsCollection ConnectionPointCollection => connectionPointCollection;

}
