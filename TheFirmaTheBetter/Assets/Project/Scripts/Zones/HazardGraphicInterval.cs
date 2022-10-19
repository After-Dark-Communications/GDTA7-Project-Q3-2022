using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardGraphicInterval : MonoBehaviour
{
    [SerializeField]
    private float amount;
    [SerializeField]
    private ParticleSystem damageParticle;

    private float currentTime;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime < amount)
            return;

        currentTime = 0;

        damageParticle.Play();
    }
}
