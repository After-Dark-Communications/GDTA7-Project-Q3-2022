using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSystem
{
    public class InputChannel
    {
        public delegate void ShipCompletedInput(int playerNumber);
        public delegate void SelectionBarUpAndDownNaviagtedInput(int playerNumber, int indexOfSelectedNavigationBar);

        public ShipCompletedInput OnShipCompletedInputStarted;
        public ShipCompletedInput OnShipCompletedInputEnded;
        public ShipCompletedInput OnShipCompletedInput;
        public SelectionBarUpAndDownNaviagtedInput OnSelectionBarUpAndDownNaviagtedInput;
    }
}
