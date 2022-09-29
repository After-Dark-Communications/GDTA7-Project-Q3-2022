﻿using Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ShipSelection.ShipBuilder.ConnectionPoints
{
    public class WeaponConnectionPoint : ConnectionPoint
    {
        public override void ConnectPart(Part toConnect)
        {
            if (toConnect is not Weapon)
                return;

            base.ConnectPart(toConnect);
        }
    }
}