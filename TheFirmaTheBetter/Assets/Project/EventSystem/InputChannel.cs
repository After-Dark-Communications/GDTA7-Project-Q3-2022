using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem
{
    public class InputChannel
    {
        /// <summary>
        /// <para><see cref="int" /> Player number</para>
        /// </summary>
        public Action<int> OnShipCompletedInput;
    }
}
