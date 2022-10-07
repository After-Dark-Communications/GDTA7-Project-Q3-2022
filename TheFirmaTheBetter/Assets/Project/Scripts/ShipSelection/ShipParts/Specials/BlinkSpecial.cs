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
        [SerializeField]
        private GameObject dashParticle;

        private float blinkRange = 8;

        protected override void HandleSpecial()
        {
            Vector3 newPosition = shipRoot.position + (shipRoot.forward * blinkRange);

            if (Physics.CheckBox(newPosition, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
            {
                CanDoSpecial = true;
                return;
            }

            currentCooldown = 0;
            CanDoSpecial = false;
            shipRoot.position = newPosition;
        }
    }
}
