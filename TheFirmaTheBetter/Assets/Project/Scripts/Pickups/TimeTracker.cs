using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickups
{
    public class TimeTracker
    {
        public float _timeInterval;
        public float _timeLeft;
        public TimeTracker(float timeInterval)
        {
            ResetTimeTracker(timeInterval);
        }

        public void TimeReset()
        {
            _timeLeft = _timeInterval;
        }
        public bool TimerComplete()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                return true;
            }
            return false;
        }

        public void ResetTimeTracker (float timeInterval)
        {
            _timeInterval = timeInterval;
            _timeLeft = _timeInterval;
        }
    }
}
