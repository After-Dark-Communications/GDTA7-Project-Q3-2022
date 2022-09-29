using UnityEngine;

public class PauseMenu : Menu
{
    public void PauseGame()
    {
        //gameObject.SetActive(true);
    }

    public void UnpauseGame()
    {
        SceneSwitchManager.UnloadScene(gameObject.scene.buildIndex);
        //gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
