using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeOptionsSaveManager : MonoBehaviour
{
    [SerializeField]
    private Slider MasterVolume;
    [SerializeField]
    private Slider MusicVolume;
    [SerializeField]
    private Slider SFXVolume;

    public void Save()
    {
        SaveManager.Save(new VolumeSettingsData(MasterVolume.value, MusicVolume.value, SFXVolume.value), "volumesettings");
    }
}
