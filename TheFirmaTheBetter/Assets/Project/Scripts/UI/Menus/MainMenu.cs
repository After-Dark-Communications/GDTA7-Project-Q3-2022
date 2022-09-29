using UnityEngine;

public class MainMenu : MenuNavigator
{
    public void StartGame()
    {
        SceneSwitchManager.SwitchToNextScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
