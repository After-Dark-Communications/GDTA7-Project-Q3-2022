using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ShipSelection
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField]
        private Part part;

        public Part Part { get => part; set => part = value; }
    }
}
