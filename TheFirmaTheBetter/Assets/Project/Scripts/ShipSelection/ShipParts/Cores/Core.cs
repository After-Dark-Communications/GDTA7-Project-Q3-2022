using Collisions;
using ShipParts.Ship;
using ShipSelection.ShipBuilders.ConnectionPoints;
using ShipSelection.Stats;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Util;

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
                ShipBody body = shipRoot.GetComponent<ShipBody>();
                body.OnPlayerCrash.AddListener(CrashShip);
            }

            CalculateHighestAndLowest();
        }

        private void CrashShip(Vector3 Velocity, GameObject other)
        {
            ICollidable collisionObject = other.GetComponentInChildren<ICollidable>();
            if (collisionObject != null)
            {//bump ship
                collisionObject.HandleCollision(thisCollision, Stats);
            }
        }

        protected override void CalculateHighestAndLowest()
        {
            base.CalculateHighestAndLowest();
            StatBoundries.SetHighestAndLowest(coreData.Health, ref StatBoundries.HEALTH_BOUNDRIES);
            StatBoundries.SetHighestAndLowest(coreData.EnergyCapacity, ref StatBoundries.ENERGY_CAPACITY_BOUNDRIES);
        }
    }
}