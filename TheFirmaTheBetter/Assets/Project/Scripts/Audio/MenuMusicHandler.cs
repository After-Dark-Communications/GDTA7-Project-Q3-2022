using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using ShipParts.Ship;
using System;

namespace Audio
{
    public class MenuMusicHandler : MonoBehaviour
    {
        #region Singleton
        public static MenuMusicHandler Instance;
        private bool isKoth;

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
            Channels.OnRoundStarted += RestartRound;
            Channels.KingOfTheHill.OnKingOfTheHillStart += StartKingOfTheHill;

        }
        #endregion
        [SerializeField]
        private FMODUnity.EventReference jingle;
        private FMOD.Studio.Bus master;
        private FMOD.Studio.EventInstance titleTheme;
        private FMOD.Studio.EventInstance buildingTheme;
        private FMOD.Studio.EventInstance battleTheme;
        private int playersInGame;
        private float playersLeft;
        // Start is called before the first frame update
        void Start()
        {
            isKoth = false;
            battleTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_Battle");
            titleTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_MainTheme");
            titleTheme.start();
            buildingTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Mus_BuildTheme");
            master = FMODUnity.RuntimeManager.GetBus("Bus:/");
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
            Channels.OnRoundStarted -= RestartRound;
            Channels.KingOfTheHill.OnKingOfTheHillStart -= StartKingOfTheHill;
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
            playersInGame = playerCount;
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
            master.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
            battleTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMODUnity.RuntimeManager.PlayOneShot(jingle, transform.position);
        }

        public void Replay()
        {
            master.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMOD.RESULT res = titleTheme.start();
            Debug.Log(res);
        }

        public void PlayerDeath(ShipBuilder builder, int playercount)
        {
            if (!isKoth)
            {
                playersLeft -= 1;
                if (playersLeft !<= 1 && playercount < playersLeft)
                    battleTheme.setParameterByName("Players_Left", playersLeft);
            }
        }

        public void RestartRound(int currentRound, int maxRounds)
        {
            if (!isKoth)
            {
                playersLeft = playersInGame;
                if (playersLeft <= 2)
                {
                    battleTheme.setParameterByName("Players_Left", float.MaxValue);

                }
                else
                {
                    battleTheme.setParameterByName("Players_Left", playersLeft);
                }
                battleTheme.setParameterByName("Rounds", maxRounds - currentRound + 1);
            }
        }

        public void StartKingOfTheHill(List<int> playerNumbers)
        {
            float rounds;
            float players;
            isKoth = true;
            battleTheme.setParameterByName("Rounds", float.MaxValue);
            battleTheme.getParameterByName("Rounds", out rounds);
            battleTheme.getParameterByName("Players_Left", out players);
            Debug.Log(rounds);
            Debug.Log(players);
        }
    }
}