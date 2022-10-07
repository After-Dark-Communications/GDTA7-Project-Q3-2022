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
        [SerializeField]
        private GameObject dashEndParticle;

        private float blinkRange = 15;

        protected override void HandleSpecial()
        {
            Vector3 newPosition = shipRoot.position + (shipRoot.forward * blinkRange);

            if (Physics.CheckBox(newPosition, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
            {
                CanDoSpecial = true;
                return;
            }

            SpawnDashParticle(dashParticle);

            ResetCooldowns(newPosition);

            shipRoot.position = newPosition;

            SpawnDashParticle(dashEndParticle);


            void SpawnDashParticle(GameObject dashParticleToSpawn)
            {
                GameObject spawned = Instantiate(dashParticleToSpawn);
                spawned.transform.position = transform.position;
                spawned.transform.rotation = transform.rotation;
            }

            void ResetCooldowns(Vector3 newPosition)
            {
                currentCooldown = 0;
                CanDoSpecial = false;
            }
        }
    }
}
