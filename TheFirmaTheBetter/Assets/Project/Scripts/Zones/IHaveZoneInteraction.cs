using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project.Scripts.Zones
{
    public interface IHaveZoneInteraction
    {
        public void HandleZoneEnterInteraction(Zone enteredZone);
        public void HandleZoneExitInteraction(Zone enteredZone);
    }
}
