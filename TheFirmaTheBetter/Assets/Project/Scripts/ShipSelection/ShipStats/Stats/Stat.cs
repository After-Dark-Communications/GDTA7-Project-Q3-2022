using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace ShipSelection.Stats
{
    public class Stat : MonoBehaviour
    {
        [SerializeField]
        private Image statValueFill;
        [SerializeField]
        private int statBarDivisions = 5;
        [SerializeField]
        private TMP_Text statName;

        public void SetValueFill(float value, float[] boundries)
        {
            if (statValueFill != null)
            {
                float minValue = boundries[StatBoundries.LowestIndex];
                float maxValue = boundries[StatBoundries.HigestIndex];

                if (minValue == StatBoundries.DefaultValue || maxValue == StatBoundries.DefaultValue)
                {
                    Debug.LogWarning("The highest or lowest values of " + gameObject.name + " weren't calculated");
                    return;
                }

                statValueFill.fillAmount = CalculateFillAmount(value, minValue, maxValue);
            }
        }

        private float CalculateFillAmount(float value,  float minValue, float maxValue)
        {
            if (minValue == maxValue && value == minValue)
            {
                return 1;
            }

            float statPercent = (value - minValue) / (maxValue - minValue);

            float divisionPercent = 1f / statBarDivisions;
            int barsToFill = (int)(statPercent / divisionPercent) + 1;

            return divisionPercent * barsToFill;

        }

        public TMP_Text StatName { get { return statName; } set { statName = value; } }
    }
}