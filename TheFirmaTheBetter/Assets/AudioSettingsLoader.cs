using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioSettingsLoader : MonoBehaviour
{
    private FMOD.Studio.Bus Master;
    private FMOD.Studio.Bus Music;
    private FMOD.Studio.Bus SFX;

    private void Awake()
    {
        //Master = RuntimeManager.GetBus("bus:/Master Bus");
        Music = RuntimeManager.GetBus("bus:/Music");
        SFX = RuntimeManager.GetBus("bus:/SFX");
    }

    // Start is called before the first frame update
    void Start()
    {
        VolumeSettingsData data = SaveManager.Load<VolumeSettingsData>("volumesettings.aa");
        if (data == null)
        {
            data = new VolumeSettingsData(1f, 1f, 1f);
            SaveManager.Save(data, "volumesettings");
        }
        SetAudioLevels(data);
    }

    void SetAudioLevels(VolumeSettingsData data)
    {
        Master.setVolume(data.MasterVolume);
        Music.setVolume(data.MusicVolume);
        SFX.setVolume(data.SfxVolume);
    }
}
