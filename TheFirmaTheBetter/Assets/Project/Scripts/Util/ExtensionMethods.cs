using System;
using UnityEngine;

namespace Util
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Remaps the float from range 1 to range 2
        /// </summary>
        /// <param name="from1">the lowest value of your current range.</param>
        /// <param name="to1">the highest value of your current range.</param>
        /// <param name="from2">the lowest value of the range that you're changing to</param>
        /// <param name="to2">the highest value of the range that you're changing to</param>
        /// <returns></returns>
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        /// <summary>
        /// Clamps each axis of the given <see cref="Vector3"/> between the given minimum float and maximum float values. Returns the axis value if it is within the minimum and maximum range.
        /// </summary>
        /// <param name="val">The <see cref="Vector3"/> to restrict inside the ragne defined by the minimum and maximum values</param>
        /// <param name="min">The minimum floating point value to compare each axis against</param>
        /// <param name="max">The maximum floating point value to compare each axis against</param>
        /// <returns>The <see cref="Vector3"/> result between the minimum and maximum values</returns>
        public static Vector3 Clamp(this Vector3 val, float min, float max)
        {
            float X = val.x;
            float Y = val.y;
            float Z = val.z;
            if (X > max || X < min)
            {
                X = Mathf.Clamp(X, min, max);
            }
            if (Y > max || Y < min)
            {
                Y = Mathf.Clamp(Y, min, max);
            }
            if (Z > max || Z < min)
            {
                Z = Mathf.Clamp(Z, min, max);
            }
            return new Vector3(X, Y, Z);
        }

        public static int RoundToPropper(float f)
        {
            return (int)Math.Round(f, 0, MidpointRounding.AwayFromZero);
        }
    }
}
