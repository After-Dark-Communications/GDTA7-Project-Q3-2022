using ShipParts;
using ShipParts.Specials;

namespace ShipSelection.ShipBuilders.ConnectionPoints
{
    public class SpecialConnectionPoint : ConnectionPoint
    {
        public override void ConnectPart(Part toConnect)
        {
            if (toConnect is not SpecialAbility)
                return;

            base.ConnectPart(toConnect);
        }
        private void OnDrawGizmosSelected()
        {
            UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
            base.OnDrawGizmosSelected();
        }
    }
}
