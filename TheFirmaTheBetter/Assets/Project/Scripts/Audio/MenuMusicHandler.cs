using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace Audio
{
    public class MenuMusicHandler : MonoBehaviour
    {
        private FMODUnity.StudioEventEmitter fmodEvent;
        private FMOD.Studio.EventInstance buildingTheme;
        private FMOD.Studio.EventInstance battleTheme;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            fmodEvent = GetComponent<FMODUnity.StudioEventEmitter>();
            buildingTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_BuildTheme");
            battleTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_Battle");
            Channels.OnEveryPlayerReady += LoadBattleScene;
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnDestroy()
        {
            Channels.OnEveryPlayerReady -= LoadBattleScene;
        }

        public void LoadBuildingScene()
        {
            fmodEvent.Stop();
            buildingTheme.start();
        }

        public void LoadBattleScene(int playerCount)
        {
            buildingTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            battleTheme.start();
        }

        public void Replay()
        {
            battleTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            fmodEvent.Play();
        }
    }
}