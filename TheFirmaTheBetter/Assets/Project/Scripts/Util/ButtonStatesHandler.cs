using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class ButtonStatesHandler
    {
        /// <summary>
        /// Converts the input system input state bools to a <see cref="ButtonStates"/> flag
        /// </summary>
        public static ButtonStates ConvertBoolsToState(bool started = false, bool performed = false, bool canceled = false)
        {
            int state = 0;
            state += started ? (int)ButtonStates.STARTED : (int)ButtonStates.NONE;
            state += performed ? (int)ButtonStates.PERFORMED : (int)ButtonStates.NONE;
            state += canceled ? (int)ButtonStates.CANCELED : (int)ButtonStates.NONE;
            return (ButtonStates)state;
        }
    }
    /// <summary>Input system button states flags</summary>
    [Flags]
    public enum ButtonStates
    {
        NONE = 0,
        STARTED = 2,
        PERFORMED = 4,
        CANCELED = 8
    }
}
