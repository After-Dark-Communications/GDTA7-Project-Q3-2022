using Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints;
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
            if (part is Core)
            {
                connection.ConnectPart(part as Core);
                return connection;
            }

            if (part is Weapon)
            {
                connection.ConnectPart(part as Weapon);
                return connection;
            }

            if (part is Special)
            {
                connection.ConnectPart(part as Special);
                return connection;
            }

            if (part is Engine)
            {
                connection.ConnectPart(part as Engine);
                return connection;
            }
        }

        return null;
    }
}
