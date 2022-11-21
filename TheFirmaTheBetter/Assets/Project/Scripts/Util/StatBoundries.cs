using ShipParts;
using ShipParts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class StatBoundries
    {
        public const int DefaultValue = -1;
        public const int LowestIndex = 0;
        public const int HigestIndex = 1;

        public static float[] HEALTH_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };
        public static float[] SPEED_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };
        public static float[] HANDLING_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };
        public static float[] ENERGY_CAPACITY_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };

        public static float[] FIRE_RATE_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };
        public static float[] ENERGY_COST_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };
        public static float[] RANGE_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };
        public static float[] DPS_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };
        public static float[] DAMAGE_BOUNDRIES = new float[2] { DefaultValue, DefaultValue };

        public static void SetHighestAndLowest(float value, ref float[] currentBoundries)
        {
            if (currentBoundries[LowestIndex] == DefaultValue || value < currentBoundries[LowestIndex])
            {
                currentBoundries[LowestIndex] = value;
            }
            if (currentBoundries[HigestIndex] == DefaultValue || value > currentBoundries[HigestIndex])
            {
                currentBoundries[HigestIndex] = value;
            }
        }
    }
}