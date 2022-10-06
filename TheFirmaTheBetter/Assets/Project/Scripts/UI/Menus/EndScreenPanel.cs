using TMPro;
using UnityEngine;

public class EndScreenPanel : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private TextMeshProUGUI playersKilledValue;
    [SerializeField]
    private TextMeshProUGUI timeSurvivedVlaue;

    public void SetPlayerStats(EndStatsData endStatsData)
    {
        playersKilledValue.text = endStatsData.PlayersKilled.ToString();

        float minutes = Mathf.FloorToInt(endStatsData.TimeSurvived / 60);
        float seconds = Mathf.FloorToInt(endStatsData.TimeSurvived % 60);
        timeSurvivedVlaue.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
