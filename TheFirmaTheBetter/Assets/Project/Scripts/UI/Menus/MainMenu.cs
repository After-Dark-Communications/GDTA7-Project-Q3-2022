using Managers;
using UnityEngine;
using EventSystem;
namespace UI.Menus
{
    public class MainMenu : Menu
    {
        public void StartGame()
        {
            Channels.OnLoadBuildingScene?.Invoke();
            SceneSwitchManager.SwitchToNextScene();
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}