using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints
{
    public class SpecialConnectionPoint : ConnectionPoint
    {
        public override void ConnectPart(Special toConnect)
        {
            ConnectPart(toConnect as Part);
        }
    }
}
