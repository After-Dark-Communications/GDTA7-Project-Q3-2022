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
        timeSurvivedVlaue.text = endStatsData.TimeLasted.ToString();
    }
}
