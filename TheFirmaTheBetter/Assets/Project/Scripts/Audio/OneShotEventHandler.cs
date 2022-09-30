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
        Channels.OnWeaponFired += PlayEvent;
    }

    public void PlayEvent(FMODUnity.EventReference fmodEvent)
    {
        FMODUnity.RuntimeManager.PlayOneShot(fmodEvent, transform.position);
    }

    public void PlayEnergyEmpty()
    {
        FMODUnity.RuntimeManager.PlayOneShot(fuelEmptyEvent, transform.position);
    }
}