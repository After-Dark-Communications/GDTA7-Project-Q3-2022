using UnityEngine;

namespace ShipParts.Engines
{
    [AddComponentMenu("Parts/Very Fast Engine")]
    public class VeryFastEngine : Engine
    {
        private const float MinimumTimeToFallOut = 8f;
        private const float MaximumTimeToFallOut = 10f;
        private const float FaloutDuration = 1f;

        private float currentFaloutTime;
        private float currentTime;

        private void Start()
        {
            SetRandomFaloutTime();
        }

        private void SetRandomFaloutTime()
        {
            currentFaloutTime = Random.Range(MinimumTimeToFallOut, MaximumTimeToFallOut);
        }

        protected override void Update()
        {
            base.Update();

            currentTime += Time.deltaTime;

            NeedToFaloutLoop();
            IsInFaloutLoop();

        }

        private void IsInFaloutLoop()
        {
            if (currentTime < FaloutDuration)
                return;

            currentTime = 0;

        }

        private void NeedToFaloutLoop()
        {
            if (currentTime > currentFaloutTime)
                return;

            currentTime = 0;
            SetRandomFaloutTime();
        }
    }
}
