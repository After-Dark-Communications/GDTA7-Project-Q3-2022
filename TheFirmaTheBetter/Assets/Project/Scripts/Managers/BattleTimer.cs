using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class BattleTimer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timerText;

        private float timeSinceStart;
        private bool timerRunning;

        private void OnEnable()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            timeSinceStart = 0;
            timerRunning = true;
        }

        public void PauseUnpauseTimer()
        {
            timerRunning = !timerRunning;
        }

        private void Update()
        {
            if (timerRunning)
            {
                timeSinceStart += Time.deltaTime;
                DisplayTime(timeSinceStart);
            }
        }
        private void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public float TimeSinceStart { get { return timeSinceStart; } }

    }
}