using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ImpactParticle : MonoBehaviour, IObjectPoolItem
{
    private ObjectPool particlesPool;
    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ResetPoolItem()
    {
        particleSystem.Clear();
    }

    public ParticleSystem ParticleSystem { get { return particleSystem; } }

}
