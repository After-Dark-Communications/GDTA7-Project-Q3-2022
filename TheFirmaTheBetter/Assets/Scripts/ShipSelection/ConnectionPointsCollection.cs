using  ShipSelection.ShipBuilder.ConnectionPoints;
using Parts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPointsCollection : MonoBehaviour
{
    private List<ConnectionPoint> connectionPoints = new List<ConnectionPoint>();

    private void Awake()
    {
        foreach (ConnectionPoint connectionPoint in gameObject.GetComponentsInChildren<ConnectionPoint>())
        {
            connectionPoints.Add(connectionPoint);
        }
    }

    public ConnectionPoint ConnectPartToCorrectPoint(Part part)
    {
        foreach (ConnectionPoint connection in connectionPoints)
        {
            if (!part.IsMyConnectionType(connection))
                continue;

            connection.ConnectPart(part);
        }

        return null;
    }

    public void RepositionAllParts()
    {
        foreach (ConnectionPoint connectionPoint in connectionPoints)
        {
            connectionPoint.ConnectPart(connectionPoint.ConnectedPart);
        }
    }

    public ConnectionPoint GetConnectionPoint(Part part)
    {
        int index = connectionPoints.FindIndex(cp => part.IsMyConnectionType(cp));
        
        if (index < 0)
            index = 0;

        return connectionPoints[index];
    }
}
