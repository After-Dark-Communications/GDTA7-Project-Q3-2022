﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints
{
    public class EngineConnectionPoint : ConnectionPoint
    {
        public override void ConnectPart(Engine toConnect)
        {
            ConnectPart(toConnect as Part);
        }
    }
}
