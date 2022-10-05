using ShipParts;
using ShipParts.Weapons;

namespace ShipSelection.ShipBuilders.ConnectionPoints
{
    public class WeaponConnectionPoint : ConnectionPoint
    {
        public override void ConnectPart(Part toConnect)
        {
            if (toConnect is not Weapon)
                return;

            base.ConnectPart(toConnect);
        }
        private void OnDrawGizmosSelected()
        {
            UnityEngine.Gizmos.color = UnityEngine.Color.green;
            base.OnDrawGizmosSelected();
        }
    }
}
