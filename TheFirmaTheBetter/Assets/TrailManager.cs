using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class TrailManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem trail;
    // Start is called before the first frame update
    void Start()
    {
        trail.Stop();
        Channels.OnEveryPlayerReady += SetTrail;
    }

    private void OnDisable()
    {
        trail.Stop();
        Channels.OnEveryPlayerReady -= SetTrail;
    }

    void SetTrail(int amountOfPlayers)
    {
        trail.Play();
    }


}
