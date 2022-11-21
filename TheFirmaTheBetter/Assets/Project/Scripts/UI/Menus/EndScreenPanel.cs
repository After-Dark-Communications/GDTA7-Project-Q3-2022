using Managers;
using TMPro;
using UnityEngine;

namespace UI.Menus
{
    public class EndScreenPanel : MonoBehaviour
    {
        [Header("Optional Stats")]
        [SerializeField]
        private GameObject pointsScoredStats;
        [SerializeField]
        private GameObject roundsWonStats;

        [Header("Stats")]
        [SerializeField]
        private TextMeshProUGUI pointsScoredValue;
        [SerializeField]
        private TextMeshProUGUI playersKilledValue;
        [SerializeField]
        private TextMeshProUGUI roundsWonValue;
        [SerializeField]
        private TextMeshProUGUI timeSurvivedVlaue;
        [SerializeField]
        private TextMeshProUGUI distanceTravelled;

        public void SetPlayerStats(PlayerResult playerResult)
        {
            if (GameModeManager.Instance.GameModeType == "FFA")
            {
                pointsScoredStats.SetActive(false);
                roundsWonStats.SetActive(true);
                roundsWonValue.text = playerResult.RoundsWon.ToString();
            }
            else
            {
                roundsWonStats.SetActive(false);
                pointsScoredStats.SetActive(true);
                pointsScoredValue.text = playerResult.PointsScored.ToString();
            }
            playersKilledValue.text = playerResult.PlayersKilled.ToString();

            float minutes = Mathf.FloorToInt(playerResult.TimeSurvived / 60);
            float seconds = Mathf.FloorToInt(playerResult.TimeSurvived % 60);
            timeSurvivedVlaue.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            distanceTravelled.text = playerResult.DistanceTravelled.ToString("N0");
        }
    }
}