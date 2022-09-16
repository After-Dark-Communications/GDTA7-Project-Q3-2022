using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints
{
    public abstract class ConnectionPoint : MonoBehaviour
    {
        private Part connectedPart;

        public Part ConnectedPart => connectedPart;

        protected void PositionPart(Part toConnect)
        {
            connectedPart = toConnect;

            MovePartToConnect();

            void MovePartToConnect()
            {
                Vector3 targetPosition = transform.position;
                Vector3 currentChildPosition = toConnect.ConnectionPointCollection.GetConnectionPoint(toConnect).transform.localPosition;

                Vector3 childToTargetDistance = targetPosition - currentChildPosition;

                toConnect.transform.position = Vector3.zero;
                toConnect.transform.position += childToTargetDistance;
            }
        }

        public void DisconnectPartFromCategory(Part part)
        {
            if (connectedPart == null)
                return;

            if (part.PartCategoryName != connectedPart.PartCategoryName)
                return;

            connectedPart = null;
        }

        public virtual void ConnectPart(Part toConnect)
        {
            if (toConnect == null)
                return;

           // DisconnectPartFromCategory(toConnect);

            PositionPart(toConnect);
        }
    }
}