using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShipSelection.Stats
{
    public class ShipStat : Stat
    {
        [SerializeField]
        private Image statValueFill;
        [SerializeField]
        private int statBarDivisions = 5;
        [SerializeField]
        private TMP_Text statValue;

        public void SetValueFill(float statValue, float minValue, float maxValue)
        {
            if (statValueFill != null)
            {
                float statPercent = (statValue - minValue) / (maxValue - minValue);
                float divisionPercent = 1f / statBarDivisions;
                int barsToFill = (int)(statPercent / divisionPercent) + 1;
                statValueFill.fillAmount = divisionPercent * barsToFill;
                Debug.Log(statValueFill.fillAmount);
            }
        }

        public TMP_Text StatValue { get { return statValue; } set { statValue = value; } }
    }
}