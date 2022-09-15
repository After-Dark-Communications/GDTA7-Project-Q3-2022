using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints
{
    public abstract class ConnectionPoint : MonoBehaviour
    {
        private List<Part> connectedParts = new List<Part>();

        protected void ConnectPart(Part toConnect)
        {
            DisconnectPartFromCategory(toConnect);

            connectedParts.Add(toConnect);
            MovePartToConnect();

            void MovePartToConnect()
            {
                Vector3 targetPosition = transform.position;
                Vector3 currentChildPosition = toConnect.transform.position;

                Vector3 childToTargetDistance = targetPosition - currentChildPosition;

                toConnect.transform.parent.position += childToTargetDistance;
            }
        }

        public void DisconnectPartFromCategory(Part part)
        {
            int index = connectedParts.FindIndex(c => c.partCategoryName == part.partCategoryName);

            connectedParts.RemoveAt(index);
        }

        public void DisconnectAllParts()
        {
            connectedParts.Clear();
        }

        public virtual void ConnectPart(Engine toConnect) { }
        public virtual void ConnectPart(Core toConnect) { }
        public virtual void ConnectPart(Special toConnect) { }
        public virtual void ConnectPart(Weapon toConnect) { }
    }
}