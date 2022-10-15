using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUGSceneSwitcher : MonoBehaviour
{
    public void SwitchToSceneWithIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
