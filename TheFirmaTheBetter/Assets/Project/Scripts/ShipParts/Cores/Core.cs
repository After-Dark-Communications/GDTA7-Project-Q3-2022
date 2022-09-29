using  ShipSelection.ShipBuilder.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parts
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
        protected override void Setup() { }
    }
}