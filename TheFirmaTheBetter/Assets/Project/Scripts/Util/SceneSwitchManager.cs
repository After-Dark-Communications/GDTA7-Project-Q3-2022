using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitchManager
{
    public static void SwitchScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void SwitchToNextScene()
    {
        int totalAmountOfScenes = SceneManager.sceneCountInBuildSettings;
        int currentLoadedSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //if (currentLoadedSceneIndex == totalAmountOfScenes - 1)
            //return;
        
        SceneManager.LoadScene(currentLoadedSceneIndex+1);
    }
}
