using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotEventHandler : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("The energy percentage at which the event should be played")]
    [SerializeField]
    private float energyPercentage;
    [Header("FMOD Events")]
    [SerializeField]
    private FMODUnity.EventReference fuelEmptyEvent;
    [Header("FMOD Events")]
    [SerializeField]
    private FMODUnity.EventReference fuelAlmostEmptyEvent;
    public void Start()
    {
        Channels.OnEnergyEmpty += PlayEnergyEmpty;
        Channels.OnWeaponFired += PlayEvent;
        Channels.OnEnergyChanged += CompareEnergy;
    }

    public void PlayEvent(FMODUnity.EventReference fmodEvent)
    {
        FMODUnity.RuntimeManager.PlayOneShot(fmodEvent, transform.position);
    }

    public void PlayEnergyEmpty()
    {
        FMODUnity.RuntimeManager.PlayOneShot(fuelEmptyEvent, transform.position);
    }

    public void CompareEnergy(int playerNumber, float energy)
    {
        if (energy * 100 <= energyPercentage)
            PlayEnergyAlert();
    }

    public void PlayEnergyAlert()
    {
        FMODUnity.RuntimeManager.PlayOneShot(fuelAlmostEmptyEvent, transform.position);
    }
}