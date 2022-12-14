using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts.Specials
{
    [CreateAssetMenu(fileName = "NewSpecialData", menuName = "Astrofire/Part/Create new SpecialData")]
    public class SpecialData : PartData
    {
        [Header("SpecialStats")]

        [SerializeField]
        [TextArea(2, 5)]
        private string specialDescription;

        [SerializeField]
        [Range(0, 20)]
        [Tooltip("Seconds between usage")]
        private int abilityCooldown;

        public string SpecialDescription => specialDescription;

        public int AbilityCooldown => abilityCooldown;
    }
}
