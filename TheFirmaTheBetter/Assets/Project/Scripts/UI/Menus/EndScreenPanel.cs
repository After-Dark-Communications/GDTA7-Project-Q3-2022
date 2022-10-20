using Managers;
using TMPro;
using UnityEngine;

namespace UI.Menus
{
    public class EndScreenPanel : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField]
        private TextMeshProUGUI playersKilledValue;
        [SerializeField]
        private TextMeshProUGUI timeSurvivedVlaue;
        [SerializeField]
        private TextMeshProUGUI distanceTravelled;

        public void SetPlayerStats(PlayerResult playerStatistics)
        {
            playersKilledValue.text = playerStatistics.PlayersKilled.ToString();

            float minutes = Mathf.FloorToInt(playerStatistics.TimeSurvived / 60);
            float seconds = Mathf.FloorToInt(playerStatistics.TimeSurvived % 60);
            timeSurvivedVlaue.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            distanceTravelled.text = playerStatistics.DistanceTravelled.ToString("N0");
        }
    }
}