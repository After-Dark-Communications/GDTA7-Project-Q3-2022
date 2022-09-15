using Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPointsCollection : MonoBehaviour
{
    private List<ConnectionPoint> connectionPoints = new List<ConnectionPoint>();

    private void Awake()
    {
        foreach(ConnectionPoint connectionPoint in gameObject.GetComponentsInChildren<ConnectionPoint>())
        {
            connectionPoints.Add(connectionPoint);
        }    
    }

    //TODO: Create function to get connection point so that we can acctually connect the correct type to that point
}
