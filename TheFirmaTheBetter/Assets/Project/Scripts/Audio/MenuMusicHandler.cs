using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using ShipParts.Ship;

namespace Audio
{
    public class MenuMusicHandler : MonoBehaviour
    {
        #region Singleton
        public static MenuMusicHandler Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);  
            }
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            SubscribeToEvents();
        }
        #endregion

        private FMOD.Studio.EventInstance titleTheme;
        private FMOD.Studio.EventInstance buildingTheme;
        private FMOD.Studio.EventInstance battleTheme;
        private float playersLeft;
        // Start is called before the first frame update
        void Start()
        {
            battleTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_Battle");
            titleTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_MainTheme");
            titleTheme.start();
            buildingTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_BuildTheme");
        }

        void SubscribeToEvents()
        {
            Channels.OnEveryPlayerReady += LoadBattleScene;
            Channels.OnPlayerBecomesDeath += PlayerDeath;
            Channels.OnGameOver += Replay;
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnApplicationQuit()
        {
            Channels.OnQuitGame?.Invoke();
        }
        private void OnDisable()
        {
            Channels.OnEveryPlayerReady -= LoadBattleScene;
            Channels.OnPlayerBecomesDeath -= PlayerDeath;
            Channels.OnGameOver -= Replay;
        }

        public void LoadBuildingScene()
        {
            titleTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            titleTheme.release();
            buildingTheme.start();
        }

        public void LoadBattleScene(int playerCount)
        {
            playersLeft = playerCount;
            if (playersLeft <= 2)
            {
                battleTheme.setParameterByName("Players_Left", float.MaxValue);
            }
            else
            {
                battleTheme.setParameterByName("Players_Left", playersLeft);
            }
            buildingTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            buildingTheme.release();
            battleTheme.start();
        }

        public void Replay()
        {
            battleTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            titleTheme.start();
        }

        public void PlayerDeath(ShipBuilder builder, int playercount)
        {
            playersLeft -= 1;
            if (playersLeft! <= 1)
                battleTheme.setParameterByName("Players_Left", playersLeft);
        }
    }
}