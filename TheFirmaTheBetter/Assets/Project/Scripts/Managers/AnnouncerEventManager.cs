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
        private FMODUnity.EventReference firstBloodEvent;
        [SerializeField]
        private FMODUnity.EventReference doubleKillEvent;
        [SerializeField]
        private FMODUnity.EventReference gameOverEvent;
        [SerializeField]
        private FMODUnity.EventReference playerEliminatedEvent;
        [SerializeField]
        private FMODUnity.EventReference energyZoneMovedEvent;
        [SerializeField]
        private FMODUnity.EventReference createShipEvent;

        private void Start()
        {
            Channels.Announcer.OnGameBoot += PlayGameStartUpEvent;
            Channels.Announcer.OnPlayGameStart += PlayGameStartEvent;
            Channels.Announcer.OnPlayGameStop += PlayGameOverEvent;
            Channels.Announcer.OnPlayFirstBlood += PlayFirstBloodEvent;
            Channels.Announcer.OnPlayPlayerEliminated += PlayPlayerEliminatedEvent;
            Channels.Announcer.OnPlayEnergyZoneMoved += PlayEnergyZoneMovedEvent;
            Channels.Announcer.OnPlayDoubleKill += PlayDoubleKillEvent;
            Channels.Announcer.OnShipSelection += PlayShipSelectionEvent;
        }

        private void OnDisable()
        {
            Channels.Announcer.OnGameBoot -= PlayGameStartUpEvent;
            Channels.Announcer.OnPlayGameStart -= PlayGameStartEvent;
            Channels.Announcer.OnPlayGameStop -= PlayGameOverEvent;
            Channels.Announcer.OnPlayFirstBlood -= PlayFirstBloodEvent;
            Channels.Announcer.OnPlayPlayerEliminated -= PlayPlayerEliminatedEvent;
            Channels.Announcer.OnPlayEnergyZoneMoved -= PlayEnergyZoneMovedEvent;
            Channels.Announcer.OnPlayDoubleKill -= PlayDoubleKillEvent;
            Channels.Announcer.OnShipSelection -= PlayShipSelectionEvent;
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
            PlayEvent(gameOverEvent);
        }

        public void PlayFirstBloodEvent()
        {
            PlayEvent(firstBloodEvent);
        }

        public void PlayDoubleKillEvent()
        {
            PlayEvent(doubleKillEvent);
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

        public void PlayShipSelectionEvent()
        {
            PlayEvent(createShipEvent);
        }
    }
}