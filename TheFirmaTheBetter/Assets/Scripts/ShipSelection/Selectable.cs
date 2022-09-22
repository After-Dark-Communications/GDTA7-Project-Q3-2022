using Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  ShipSelection
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField]
        private Part part;

        public Part Part { get => part; set => part = value; }
    }
}
