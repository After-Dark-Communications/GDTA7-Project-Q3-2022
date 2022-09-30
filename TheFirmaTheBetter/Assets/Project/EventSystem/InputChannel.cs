using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.EventSystem
{
    public class InputChannel
    {
        /// <summary>
        /// <para><see cref="int" /> Player number</para>
        /// </summary>
        public Action<int> OnShipCompletedInput;

        /// <summary>
        /// <para><see cref="int" /> Player number</para>
        /// <para><see cref="int" /> Index of Selection Bar</para>
        /// </summary>
        public Action<int,int> OnSelectionBarUpAndDownNaviagtedInput;
    }
}
