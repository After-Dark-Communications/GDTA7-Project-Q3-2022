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
                float statPercent = (value - boundries[StatBoundries.lowestIndex]) / (boundries[StatBoundries.higestIndex] - boundries[StatBoundries.lowestIndex]);
                float divisionPercent = 1f / statBarDivisions;
                int barsToFill = (int)(statPercent / divisionPercent) + 1;
                statValueFill.fillAmount = divisionPercent * barsToFill;
            }
        }

        public TMP_Text StatName { get { return statName; } set { statName = value; } }
    }
}