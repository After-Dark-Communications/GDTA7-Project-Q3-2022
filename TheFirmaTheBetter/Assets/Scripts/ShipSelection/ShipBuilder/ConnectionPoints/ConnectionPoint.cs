using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints
{
    public abstract class ConnectionPoint : MonoBehaviour
    {
        private List<Part> connectedParts = new List<Part>();

        public void ConnectPart(Part toConnect)
        {
            connectedParts.Add(toConnect);
            MovePartToConnect();

            void MovePartToConnect()
            {
                Vector3 newPos = Vector3.zero;
                Vector3 currentPartParentPosition = toConnect.transform.parent.position;
                Vector3 coreParentPosition = transform.parent.position;
                Vector3 partConnectionPointInParent = toConnect.transform.localPosition;
                Vector3 coreConnectionPointInParent = transform.localPosition;

                newPos = coreParentPosition + coreConnectionPointInParent;
                newPos = newPos + partConnectionPointInParent;

                toConnect.transform.parent.position = newPos;
            }
        }

        public void DisconnectPart(Part part)
        {
            int index = connectedParts.FindIndex(c => c.GetType() == part.GetType());

            connectedParts.RemoveAt(index);
        }

        public void DisconnectAllParts()
        {
            connectedParts.Clear();
        }
    }
}