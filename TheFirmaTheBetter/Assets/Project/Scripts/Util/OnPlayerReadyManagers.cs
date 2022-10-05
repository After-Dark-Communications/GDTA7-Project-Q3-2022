using EventSystem;
using UnityEngine;

public class OnPlayerReadyManagers : MonoBehaviour
{
    private void OnEnable()
    {
        Channels.OnEveryPlayerReady += OnEveryPlayerReady;
    }

    private void OnEveryPlayerReady()
    {
        SceneSwitchManager.SwitchToNextScene();
    }
}
