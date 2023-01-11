using EventSystem;
using TMPro;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roundCounterText;

    private void OnEnable()
    {
        Channels.OnRoundStarted += UpdateRoundCounter;
    }

    private void OnDisable()
    {
        Channels.OnRoundStarted -= UpdateRoundCounter;
    }

    private void UpdateRoundCounter(int roundIndex, int numberOfRounds)
    {
        roundCounterText.text = $"Round {roundIndex}/{numberOfRounds}";
    }
}
