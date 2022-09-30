using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ImpactParticle : MonoBehaviour
    {
        private ParticleSystem particleSystem;
        // Start is called before the first frame update
        private void OnEnable()
        {
            particleSystem = GetComponent<ParticleSystem>();
            particleSystem.Play();
        }

        public void ResetPoolItem()
        {
            particleSystem.Clear();
        }

        public ParticleSystem ParticleSystem { get { return particleSystem; } }

    }
}