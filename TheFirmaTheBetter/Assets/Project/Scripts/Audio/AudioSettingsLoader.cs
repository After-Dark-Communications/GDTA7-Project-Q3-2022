using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Data;

namespace Audio
{
    public class AudioSettingsLoader : MonoBehaviour
    {
        private FMOD.Studio.VCA Master;
        private FMOD.Studio.VCA Music;
        private FMOD.Studio.VCA SFX;

        private void Awake()
        {
            Master = RuntimeManager.GetVCA("vca:/Master");
            Music = RuntimeManager.GetVCA("vca:/Music");
            SFX = RuntimeManager.GetVCA("vca:/SFX");
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
}