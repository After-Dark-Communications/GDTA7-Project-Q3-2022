using ShipParts;
using ShipParts.Engines;

namespace ShipSelection.ShipBuilders.ConnectionPoints
{
    public class EngineConnectionPoint : ConnectionPoint
    {
        public override void ConnectPart(Part toConnect)
        {
            if (toConnect is not Engine)
                return;

            base.ConnectPart(toConnect);
        }
    }
}
