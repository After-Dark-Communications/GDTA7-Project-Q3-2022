using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private MenuPanel startingPanel;

    private MenuPanel currentPanel;

    private void OnEnable()
    {
        OpenMenuPanel(startingPanel);
    }

    public void StartGame()
    {
        SceneSwitchManager.SwitchToNextScene();
    }

    public void OpenMenuPanel(MenuPanel menuPanel)
    {
        if (currentPanel != null) currentPanel.ClosePanel();
        menuPanel.OpenPanel();
        currentPanel = menuPanel;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
