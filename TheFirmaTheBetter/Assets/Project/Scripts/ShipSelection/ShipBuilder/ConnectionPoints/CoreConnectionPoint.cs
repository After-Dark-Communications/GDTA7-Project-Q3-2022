using ShipParts;
using ShipParts.Cores;

namespace ShipSelection.ShipBuilders.ConnectionPoints
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
