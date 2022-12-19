using EventSystem;
using Managers;
using UnityEngine;

namespace Managers
{
    public class EveryPlayerReadyManagers : MonoBehaviour
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