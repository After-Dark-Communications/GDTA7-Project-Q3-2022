using System.Collections.Generic;
using UnityEngine;

namespace ShipParts.Ship
{
    public class Ship : MonoBehaviour
    {
        private List<Part> lstParts = new List<Part>();

        public List<Part> LstParts { get => lstParts; set => lstParts = value; }
    }
}