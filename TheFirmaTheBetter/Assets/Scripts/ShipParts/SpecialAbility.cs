using  ShipSelection.ShipBuilder.ConnectionPoints;
using System;
using UnityEngine;
using Util;

namespace Parts
{
    [AddComponentMenu("Parts/Special")]
    public class SpecialAbility : Part
    {
        [SerializeField]
        private SpecialData specialData;

        public override string PartCategoryName => "Special";

        public override bool IsMyType(Part part)
        {
            if (part is SpecialAbility)
                return true;

            return false;
        }

        public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
        {
            if (connectionPoint is SpecialConnectionPoint)
                return true;

            return false;
        }

        public override void Setup()
        {
            if (RootInputHanlder != null)
            {
                RootInputHanlder.OnPlayerSpecial.AddListener(PerformSpecial);
            }
        }

        private void PerformSpecial(ButtonStates state)
        {
            if (state != ButtonStates.NONE)
            {
                if (state.HasFlag(ButtonStates.STARTED))
                {
                    Debug.Log("Special Started");
                }
                if (state.HasFlag(ButtonStates.PERFORMED))
                {
                    Debug.Log("Special Performed");
                }
                if (state.HasFlag(ButtonStates.CANCELED))
                {
                    Debug.Log("Special Canceled");
                }
            }
        }
    }
}