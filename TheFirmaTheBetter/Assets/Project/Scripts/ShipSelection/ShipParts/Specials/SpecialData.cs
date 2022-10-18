using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts.Specials
{
    [CreateAssetMenu(fileName = "NewSpecialData", menuName = "Part/Create new SpecialData")]
    public class SpecialData : PartData
    {
        [Header("SpecialStats")]

        [SerializeField]
        [TextArea(2, 5)]
        private string description;

        [SerializeField]
        [Range(0, 20)]
        [Tooltip("Seconds between usage")]
        private int abilityCooldown;

        public string Description => description;

        public int AbilityCooldown => abilityCooldown;
    }
}
