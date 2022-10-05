using Collisions;
using ShipParts.Ship;
using ShipSelection.ShipBuilders.ConnectionPoints;
using System;
using UnityEngine;

namespace ShipParts.Cores
{
    [AddComponentMenu("Parts/Core")]
    public class Core : Part
    {
        [SerializeField]
        private CoreData coreData;

        public override string PartCategoryName => "Core";

        public override PartData GetData()
        {
            return coreData;
        }

        public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
        {
            if (connectionPoint is CoreConnectionPoint)
                return true;

            return false;
        }

        public override bool IsMyType(Part part)
        {
            if (part is Core)
                return true;

            return false;
        }
        //TODO: determine drag based on weight
        protected override void Setup()
        {
            if (rootInputHandler != null)
            {
                shipRoot.GetComponent<ShipBody>().OnPlayerCrash.AddListener(CrashShip);
            }
        }

        private void CrashShip(Vector3 Velocity, GameObject other)
        {
            ICollidable collisionObject = other.GetComponentInChildren<ICollidable>();
            if (collisionObject != null)
            {//bump ship
                collisionObject.HandleCollision(thisCollision);
            }
        }
    }
}