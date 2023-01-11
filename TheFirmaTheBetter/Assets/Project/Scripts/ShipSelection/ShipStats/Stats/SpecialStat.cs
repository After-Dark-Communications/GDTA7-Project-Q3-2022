using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShipSelection.Stats
{
    public class SpecialStat : Stat
    {
        [SerializeField]
        private TMP_Text description;

        public TMP_Text Description { get { return description; } set { description = value; } }
    }
}