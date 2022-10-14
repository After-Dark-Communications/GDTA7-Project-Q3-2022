using ShipParts;
using ShipParts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class StatBoundries
    {
        private const int defaultValue = -1;
        public const int lowestIndex = 0;
        public const int higestIndex = 1;

        public static float[] HEALTH_BOUNDRIES = new float[2] { defaultValue, defaultValue };
        public static float[] SPEED_BOUNDRIES = new float[2] { defaultValue, defaultValue };
        public static float[] HANDLING_BOUNDRIES = new float[2] { defaultValue, defaultValue };
        public static float[] ENERGY_CAPACITY_BOUNDRIES = new float[2] { defaultValue, defaultValue };

        public static float[] FIRE_RATE_BOUNDRIES = new float[2] { defaultValue, defaultValue };
        public static float[] ENERGY_COST_BOUNDRIES = new float[2] { defaultValue, defaultValue };
        public static float[] RANGE_BOUNDRIES = new float[2] { defaultValue, defaultValue };

        public static void SetHighestAndLowest(float value, ref float[] currentBoundries)
        {
            if (currentBoundries[lowestIndex] == defaultValue || value < currentBoundries[lowestIndex])
            {
                currentBoundries[lowestIndex] = value;
            }
            if (currentBoundries[higestIndex] == defaultValue || value > currentBoundries[higestIndex])
            {
                currentBoundries[higestIndex] = value;
            }
        }
    }
}