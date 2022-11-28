using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Data;
using EventSystem;

namespace Audio
{
    public class AudioSettingsLoader : MonoBehaviour
    {
        private FMOD.Studio.VCA Master;
        private FMOD.Studio.VCA Music;
        private FMOD.Studio.VCA SFX;
        private FMOD.Studio.VCA Voice;

        private void Awake()
        {
            Channels.OnAudioSettingsSaved += SetAudioLevels;
            Channels.OnMasterValueChanged += SetMasterVolume;
            Channels.OnMusicChangedEvent += SetMusicVolume;
            Channels.OnSFXChangedEvent += SetSFXVolume;
            Channels.OnVoiceChangedEvent += SetVoiceVolume;
            Master = RuntimeManager.GetVCA("vca:/Master");
            Music = RuntimeManager.GetVCA("vca:/Music");
            SFX = RuntimeManager.GetVCA("vca:/SFX");
            Voice = RuntimeManager.GetVCA("vca:/Voice");
        }

        private void OnDestroy()
        {
            Channels.OnAudioSettingsSaved -= SetAudioLevels;
        }

        // Start is called before the first frame update
        void Start()
        {
            VolumeSettingsData data = SaveManager.Load<VolumeSettingsData>("volumesettings.aa");
            if (data == null)
            {
                data = new VolumeSettingsData(1f, 1f, 1f, 1f);
                SaveManager.Save(data, "volumesettings");
            }
            SetAudioLevels(data);
        }

        void SetAudioLevels(VolumeSettingsData data)
        {
            Master.setVolume(data.MasterVolume);
            Music.setVolume(data.MusicVolume);
            SFX.setVolume(data.SfxVolume);
            Voice.setVolume(data.VoiceVolume);
        }

        void SetMusicVolume(float volume)
        {
            Music.setVolume(volume);
        }

        void SetSFXVolume(float volume)
        {
            SFX.setVolume(volume);
        }

        void SetVoiceVolume(float volume)
        {
            Voice.setVolume(volume);
        }

        void SetMasterVolume(float volume)
        {
            Master.setVolume(volume);
        }
    }
}