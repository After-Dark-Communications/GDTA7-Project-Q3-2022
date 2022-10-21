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
        private TextMeshProUGUI roundsWonValue;
        [SerializeField]
        private TextMeshProUGUI timeSurvivedVlaue;
        [SerializeField]
        private TextMeshProUGUI distanceTravelled;

        public void SetPlayerStats(PlayerResult playerResult)
        {
            playersKilledValue.text = playerResult.PlayersKilled.ToString();

            roundsWonValue.text = playerResult.RoundsWon.ToString();

            float minutes = Mathf.FloorToInt(playerResult.TimeSurvived / 60);
            float seconds = Mathf.FloorToInt(playerResult.TimeSurvived % 60);
            timeSurvivedVlaue.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            distanceTravelled.text = playerResult.DistanceTravelled.ToString("N0");
        }
    }
}