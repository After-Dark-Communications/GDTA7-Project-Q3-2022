using EventSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using EventSystem;
using UnityEngine;
using System;

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
        [SerializeField]
        private bool isKingOfTheHill;

        private void Awake()
        {
            Channels.OnCountdownDone += OnCountdownDone;
        }

        private void OnDestroy()
        {
            Channels.OnCountdownDone -= OnCountdownDone;
        }

        private void OnCountdownDone()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            if (isKingOfTheHill)
            {
                timeSinceStart = kothTimeInSec;
            }
            else
            {
                timeSinceStart = 0;
            }
            timerRunning = true;
            DisplayTime(timeSinceStart);
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