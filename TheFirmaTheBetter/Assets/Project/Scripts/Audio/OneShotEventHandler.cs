using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class OneShotEventHandler : MonoBehaviour
    {
        [Header("Variables")]
        [Tooltip("The energy percentage at which the event should be played")]
        [SerializeField]
        private float energyPercentage;
        [Header("FMOD Events")]
        [SerializeField]
        private FMODUnity.EventReference fuelEmptyEvent;
        [SerializeField]
        private FMODUnity.EventReference fuelAlmostEmptyEvent;
        [SerializeField]
        private FMODUnity.EventReference hitmarker;
        private bool energyLowHasPlayed;

        private void Start()
        {
            Channels.OnEnergyEmpty += PlayEnergyEmpty;
            Channels.OnWeaponFired += PlayEvent;
            Channels.OnEnergyChanged += CompareEnergy;
            Channels.OnPlayerHit += PlayHitmarker;
            energyLowHasPlayed = false;
        }

        private void OnDisable()
        {
            Channels.OnEnergyEmpty -= PlayEnergyEmpty;
            Channels.OnWeaponFired -= PlayEvent;
            Channels.OnEnergyChanged -= CompareEnergy;
            Channels.OnPlayerHit -= PlayHitmarker;
        }

        private void PlayEvent(FMODUnity.EventReference fmodEvent)
        {
            FMODUnity.RuntimeManager.PlayOneShot(fmodEvent, transform.position);
        }

        private void PlayEnergyEmpty()
        {
            FMODUnity.RuntimeManager.PlayOneShot(fuelEmptyEvent, transform.position);
        }

        private void CompareEnergy(int playerNumber, float energy)
        {
            energy *= 100;
            if (energy <= energyPercentage && energy >= 0 && !energyLowHasPlayed)
            {
                PlayEnergyAlert();
                energyLowHasPlayed = true;
            }

            else if (energy == 100)
                energyLowHasPlayed = false;
        }

        private void PlayEnergyAlert()
        {
            FMODUnity.RuntimeManager.PlayOneShot(fuelAlmostEmptyEvent, transform.position);
        }

        private void PlayHitmarker()
        {
            FMODUnity.RuntimeManager.PlayOneShot(hitmarker, transform.position);
        }
    }
}