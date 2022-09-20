using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : Manager
{
    #region Singleton
    public static SceneSwitchManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public static void SwitchScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void SwitchToNextScene()
    {
        int totalAmountOfScenes = SceneManager.sceneCountInBuildSettings;
        int currentLoadedSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentLoadedSceneIndex == totalAmountOfScenes - 1)
            return;
        
        SceneManager.LoadScene(currentLoadedSceneIndex++);
    }
}
