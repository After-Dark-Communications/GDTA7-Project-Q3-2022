using System;
using UnityEngine;
using Util;

namespace Parts
{
    [AddComponentMenu("Parts/Special")]
    public class Special : Part
    {
        [SerializeField]
        private SpecialData specialData;

        public override string PartName => "Special";

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