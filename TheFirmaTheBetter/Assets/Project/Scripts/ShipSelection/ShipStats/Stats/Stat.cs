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
        private Image comparisonValueFill;
        [SerializeField]
        private int statBarDivisions = 5;
        [SerializeField]
        private TMP_Text statName;

        [Tooltip("0 is the negative color, 1 is the positive color")]
        [SerializeField]
        private Gradient comparisonColors;

        private float minValue;
        private float maxValue;

        public void SetValueFill(float actualValue, float[] boundries)
        {
            if (statValueFill == null || comparisonValueFill == null)
                return;

            minValue = boundries[StatBoundries.LowestIndex];
            maxValue = boundries[StatBoundries.HigestIndex];

            if (minValue == StatBoundries.DefaultValue || maxValue == StatBoundries.DefaultValue)
            {
                Debug.LogWarning("The highest or lowest values of " + gameObject.name + " weren't calculated");
                return;
            }

            statValueFill.fillAmount = CalculateFillAmount(actualValue, minValue, maxValue);
            comparisonValueFill.fillAmount = 0;
        }

        public void SetValueFill(float actualValue, float comparisonValue, float[] boundries)
        {
            SetValueFill(actualValue, boundries);

            float actualFillAmount = statValueFill.fillAmount;
            float comparisonFillAmount = CalculateFillAmount(comparisonValue, minValue, maxValue);

            if (actualFillAmount <= comparisonFillAmount)
            {
                statValueFill.fillAmount = actualFillAmount;
                comparisonValueFill.fillAmount = comparisonFillAmount;
                comparisonValueFill.color = comparisonColors.Evaluate(1);
            }
            else
            {
                statValueFill.fillAmount = comparisonFillAmount;
                comparisonValueFill.fillAmount = actualFillAmount;
                comparisonValueFill.color = comparisonColors.Evaluate(0);
            }
        }

        private float CalculateFillAmount(float value, float minValue, float maxValue)
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