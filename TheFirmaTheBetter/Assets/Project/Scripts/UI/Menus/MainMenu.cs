using Managers;
using UnityEngine;

namespace UI.Menus
{
    public class MainMenu : Menu
    {
        public void StartGame()
        {
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