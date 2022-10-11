using EventSystem;
using Managers;
using UnityEngine;

namespace Util
{
    public class OnPlayerReadyManagers : MonoBehaviour
    {
        private void OnEnable()
        {
            Channels.OnEveryPlayerReady += OnEveryPlayerReady;
        }

        private void OnDisable()
        {
            Channels.OnEveryPlayerReady -= OnEveryPlayerReady;
        }

        private void OnEveryPlayerReady(int amountOfPlayers)
        {
            SceneSwitchManager.SwitchToNextScene();
        }
    }
}