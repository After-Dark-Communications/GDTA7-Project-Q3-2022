using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipSelection
{
    public class ShipInfo : MonoBehaviour
    {
        [SerializeField]
        private int playerNumber;

        public int PlayerNumber => playerNumber;
    }
}