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
        private Image positiveComparisonFill;
        [SerializeField]
        private Image negativeComparisonFill;
        [SerializeField]
        private int statBarDivisions = 5;
        [SerializeField]
        private TMP_Text statName;

        private float minValue;
        private float maxValue;

        public void SetValueFill(float actualValue, float[] boundries)
        {
            if (statValueFill == null || positiveComparisonFill == null)
                return;

            minValue = boundries[StatBoundries.LowestIndex];
            maxValue = boundries[StatBoundries.HigestIndex];

            if (minValue == StatBoundries.DefaultValue || maxValue == StatBoundries.DefaultValue)
            {
                Debug.LogWarning("The highest or lowest values of " + gameObject.name + " weren't calculated");
                return;
            }

            statValueFill.fillAmount = CalculateFillAmount(actualValue, minValue, maxValue);
            positiveComparisonFill.gameObject.SetActive(false);
            negativeComparisonFill.gameObject.SetActive(false);
        }

        public void SetValueFill(float actualValue, float comparisonValue, float[] boundries)
        {
            SetValueFill(actualValue, boundries);

            float actualFillAmount = statValueFill.fillAmount;
            float comparisonFillAmount = CalculateFillAmount(comparisonValue, minValue, maxValue);

            if (actualFillAmount <= comparisonFillAmount)
            {
                // Positive comparison
                statValueFill.fillAmount = actualFillAmount;
                positiveComparisonFill.fillAmount = comparisonFillAmount;
                positiveComparisonFill.gameObject.SetActive(true);
                negativeComparisonFill.gameObject.SetActive(false);
            }
            else
            {
                // Negative comparison
                statValueFill.fillAmount = comparisonFillAmount;
                negativeComparisonFill.fillAmount = actualFillAmount;
                negativeComparisonFill.gameObject.SetActive(true);
                positiveComparisonFill.gameObject.SetActive(false);
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