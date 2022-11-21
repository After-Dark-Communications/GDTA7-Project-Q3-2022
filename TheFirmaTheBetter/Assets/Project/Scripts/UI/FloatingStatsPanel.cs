using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FloatingStatsPanel : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField]
        private List<ShipStatBar> statBars;
        [SerializeField]
        private CrownShower crownShower;
        [SerializeField]
        private SpecialCooldownShower specialCooldownShower;
        [Header("Objects")]
        [SerializeField]
        private GameObject objectToFollow;

        [SerializeField]
        [Tooltip("The x axis offset of the stats panel (in pixels)")]
        private int xOffsetPixels;

        [SerializeField]
        [Tooltip("The y axis offset of the stats panel (in pixels)")]
        private int yOffsetPixels;

        private void Update()
        {
            if (objectToFollow == null)
                return;

            if (objectToFollow.activeInHierarchy)
            {
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectToFollow.transform.position);
                transform.position = new Vector3(screenPosition.x + xOffsetPixels, screenPosition.y + yOffsetPixels);
            }
        }

        private void OnDrawGizmos()
        {
            // Creates an outline for where the player will be placed compared to the panel
            Vector3 reverseOffset = new Vector3(transform.position.x - xOffsetPixels, transform.position.y - yOffsetPixels);
            Gizmos.DrawWireCube(reverseOffset, Vector3.one * 50);
        }

        public GameObject ObjectToFollow
        {
            get { return objectToFollow; }
            set { objectToFollow = value; }
        }

        public ShipStatBar[] StatBars
        {
            get { return statBars.ToArray(); }
        }

        public CrownShower CrownShower { get => crownShower; set => crownShower = value; }
        public SpecialCooldownShower SpecialCooldownShower { get => specialCooldownShower; set => specialCooldownShower = value; }
    }
}