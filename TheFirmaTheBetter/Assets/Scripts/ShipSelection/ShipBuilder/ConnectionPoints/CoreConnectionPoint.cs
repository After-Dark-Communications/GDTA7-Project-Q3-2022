using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints
{
    public class CoreConnectionPoint : ConnectionPoint
    {
        public override void ConnectPart(Part toConnect)
        {
            if (toConnect is not Core)
                return;

            base.ConnectPart(toConnect);
        }
    }
}
