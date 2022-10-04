using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EndScreen : MenuPanel
{
    private void OnEnable()
    {
        OpenPanel();
    }

    public void Rematch()
    {
        SceneSwitchManager.LoadFirstScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
