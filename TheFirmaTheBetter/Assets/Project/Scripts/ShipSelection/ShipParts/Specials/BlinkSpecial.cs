using ShipParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace  ShipParts.Specials
{
    public class BlinkSpecial : SpecialAbility
    {
        private float blinkRange = 8;

        protected override void HandleSpecial()
        {
            if(Physics.Raycast(transform.position, transform.forward, blinkRange))
            {
                currentCooldown = 0;
                CanDoSpecial = false;
                return;
            }

            shipRoot.position += shipRoot.forward * blinkRange;
        }
    }
}
