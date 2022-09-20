using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Helper
{
    public static class ListLooper
    {
        public static int SelectNext<T>(List<T> listToLoop, int currentIndex)
        {
            currentIndex++;

            if (currentIndex >= listToLoop.Count)
            {
                currentIndex = 0;
            }

            return currentIndex;
        }

        public static int SelectPrevious<T>(List<T> listToLoop, int currentIndex)
        {
            currentIndex--;

            if (currentIndex < 0)
            {
                currentIndex = listToLoop.Count - 1;
            }

            return currentIndex;
        }
    }
}
