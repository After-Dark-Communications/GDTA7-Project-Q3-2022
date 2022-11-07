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
            Channels.OnEveryPlayerReady += LoadBattleScene;
            Channels.OnPlayerBecomesDeath += PlayerDeath;
            Channels.OnGameOver += EndGame;
            Channels.OnReturnToTitleScreen += Replay;
            Channels.OnLoadBuildingScene += LoadBuildingScene;
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
            Channels.OnGameOver -= EndGame;
            Channels.OnReturnToTitleScreen -= Replay;
            Channels.OnLoadBuildingScene -= LoadBuildingScene;
        }

        public void LoadBuildingScene()
        {
            FMOD.RESULT res = titleTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Debug.Log(res);
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
            battleTheme.start();
        }

        public void EndGame()
        {
            battleTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            battleTheme.release();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Music/Mus_Jingle", transform.position);
        }

        public void Replay()
        {
            FMOD.RESULT res = titleTheme.start();
            Debug.Log(res);
        }

        public void PlayerDeath(ShipBuilder builder, int playercount)
        {
            playersLeft -= 1;
            if (playersLeft! <= 1 && playercount < playersLeft)
                battleTheme.setParameterByName("Players_Left", playersLeft);
        }
    }
}