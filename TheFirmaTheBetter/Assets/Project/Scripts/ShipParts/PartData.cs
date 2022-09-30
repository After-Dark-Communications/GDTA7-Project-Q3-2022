using ShipParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts
{
    [Header("PartStats")]
    [SerializeField]
    private string partName = "part";
    [SerializeField]
    private Vector3 connectionPoint;

        [SerializeField]
        private int partWeight;

        [Header("Modifiers")]
        [SerializeField]
        private DataModifier dragModifier;

    public string PartName => partName;

    public Vector3 ConnectionPoint => connectionPoint;

        public int PartWeight => partWeight;

        public DataModifier DragModifier => dragModifier;
    }
}