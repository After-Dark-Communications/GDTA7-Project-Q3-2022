using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitchManager
{
    public static void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void SwitchToNextScene()
    {
        int totalAmountOfScenes = SceneManager.sceneCountInBuildSettings;
        int currentLoadedSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentLoadedSceneIndex == totalAmountOfScenes - 1)
            return;

        SceneManager.LoadScene(currentLoadedSceneIndex + 1);
    }

    // Additive loading
    //public static void LoadScene(string sceneName)
    //{
    //    SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    //}

    //public static void UnloadScene(int index)
    //{
    //    Scene scene = SceneManager.GetSceneByBuildIndex(index);
    //    if (scene.IsValid() && scene.isLoaded)
    //    {
    //        SceneManager.UnloadSceneAsync(index);
    //    }
    //}
}
