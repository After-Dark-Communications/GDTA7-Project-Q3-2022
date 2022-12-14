using ShipParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts
{
    public abstract class PartData : ScriptableObject
    {
        [Header("PartStats")]
        [SerializeField]
        private string partName = "part";

        [SerializeField]
        [TextArea(2, 5)]
        private string partDescription;

        [SerializeField]
        private Vector3 connectionPoint;

        [SerializeField]
        private int partWeight;

        [Header("Modifiers")]
        [SerializeField]
        private DataModifier dragModifier;

        public string PartName => partName;

        public string PartDescription => partDescription;

        public Vector3 ConnectionPoint => connectionPoint;

        public int PartWeight => partWeight;

        public DataModifier DragModifier => dragModifier;
    }
}