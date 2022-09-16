using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
