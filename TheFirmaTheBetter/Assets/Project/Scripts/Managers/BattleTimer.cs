using EventSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using EventSystem;
using UnityEngine;

namespace Managers
{
    public class BattleTimer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timerText;
        [SerializeField]
        private float kothTimeInSec;

        private float timeSinceStart;
        private bool timerRunning;
        private bool isKingOfTheHill;

        private void Awake()
        {
            Channels.OnControllerShemeHidden += StartTimer;
        }

        private void OnDestroy()
        {
            Channels.OnControllerShemeHidden -= StartTimer;
        }

        public void StartTimer()
        {
            if (isKingOfTheHill)
            {
                timeSinceStart = 0;
            }
            else
            {
                timeSinceStart = kothTimeInSec;
            }
            timerRunning = true;
        }

        public void StartKoth(List<int> ints)
        {
            isKingOfTheHill = true;
        }

        public void PauseUnpauseTimer()
        {
            timerRunning = !timerRunning;
        }

        private void Update()
        {
            if (timerRunning)
            {
                if (isKingOfTheHill)
                {
                    timeSinceStart -= Time.deltaTime;
                    if (timeSinceStart == 0)
                    {
                        Channels.KingOfTheHill.OnKingOfTheHillEnd?.Invoke();
                    }
                }
                else
                {
                    timeSinceStart += Time.deltaTime;
                }
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