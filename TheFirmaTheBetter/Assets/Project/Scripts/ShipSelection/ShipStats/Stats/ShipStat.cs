using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShipSelection.Stats
{
    public class ShipStat : Stat
    {
        [SerializeField]
        private TMP_Text statValue;

        public TMP_Text StatValue { get { return statValue; } set { statValue = value; } }
    }
}