using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace Managers
{
    public class AnnouncerEventManager : MonoBehaviour
    {
        [Header("FMOD Events")]
        [SerializeField]
        private FMODUnity.EventReference titleScreenEvent;
        [SerializeField]
        private FMODUnity.EventReference gameStartEvent;
        [SerializeField]
        private FMODUnity.EventReference gameOverEvent;
        [SerializeField]
        private FMODUnity.EventReference playerEliminatedEvent;
        [SerializeField]
        private FMODUnity.EventReference energyZoneMovedEvent;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            Channels.Announcer.OnGameBoot += PlayGameStartUpEvent;
            Channels.Announcer.OnPlayGameStart += PlayGameStartEvent;
            Channels.Announcer.OnPlayGameStop += PlayGameOverEvent;
            Channels.Announcer.OnPlayPlayerEliminated += PlayPlayerEliminatedEvent;
            Channels.Announcer.OnPlayEnergyZoneMoved += PlayEnergyZoneMovedEvent;
        }

        private void OnDisable()
        {
            Channels.Announcer.OnGameBoot -= PlayGameStartUpEvent;
            Channels.Announcer.OnPlayGameStart -= PlayGameStartEvent;
            Channels.Announcer.OnPlayGameStop -= PlayGameOverEvent;
            Channels.Announcer.OnPlayPlayerEliminated -= PlayPlayerEliminatedEvent;
            Channels.Announcer.OnPlayEnergyZoneMoved -= PlayEnergyZoneMovedEvent;
        }

        public void PlayEvent(FMODUnity.EventReference fmodEvent)
        {
            FMODUnity.RuntimeManager.PlayOneShot(fmodEvent);
        }

        public void PlayGameStartUpEvent()
        {
            PlayEvent(titleScreenEvent);
        }

        public void PlayGameStartEvent()
        {
            PlayEvent(gameStartEvent);
        }

        public void PlayGameOverEvent()
        {
            PlayEvent(gameOverEvent);
        }

        public void PlayPlayerEliminatedEvent()
        {
            PlayEvent(playerEliminatedEvent);
        }

        public void PlayEnergyZoneMovedEvent()
        {
            PlayEvent(energyZoneMovedEvent);
        }
    }
}