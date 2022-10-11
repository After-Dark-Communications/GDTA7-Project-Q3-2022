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

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            fmodEvent = GetComponent<FMODUnity.StudioEventEmitter>();
            buildingTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_BuildTheme");
            Channels.OnEveryPlayerReady += LoadBattleScene;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void LoadBuildingScene()
        {
            fmodEvent.Stop();
            buildingTheme.start();
        }

        public void LoadBattleScene(int playerCount)
        {
            //todo: call battle music
            buildingTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}