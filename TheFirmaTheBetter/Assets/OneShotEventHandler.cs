using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotEventHandler : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.EventReference fuelEmptyEvent;
    public void Start()
    {
        Channels.OnEnergyEmpty += PlayEnergyEmpty;
    }

    public void PlayEnergyEmpty()
    {
        FMODUnity.RuntimeManager.PlayOneShot(fuelEmptyEvent, transform.position);
    }
}