using UnityEngine;
using UnityEngine.UI;
using EventSystem;

namespace Data
{
    public class VolumeOptionsSaveManager : MonoBehaviour
    {
        [Header("Sliders")]
        [SerializeField]
        private Slider MasterVolume;
        [SerializeField]
        private Slider MusicVolume;
        [SerializeField]
        private Slider SFXVolume;
        [SerializeField]
        private Slider VoiceVolume;
        [Header("Samples")]
        [SerializeField]
        private FMODUnity.EventReference sfxSample;
        [SerializeField]
        private FMODUnity.EventReference voiceSample;
        private void OnEnable()
        {
            LoadSettings();
        }

        public void SaveSettings()
        {
            SaveManager.Save(new VolumeSettingsData(MasterVolume.value, MusicVolume.value, SFXVolume.value, VoiceVolume.value), "volumesettings");
            Channels.OnAudioSettingsSaved?.Invoke(SaveManager.Load<VolumeSettingsData>("volumesettings.aa"));
        }

        public void LoadSettings()
        {
            VolumeSettingsData volumeSettings = SaveManager.Load<VolumeSettingsData>("volumesettings.aa");
            if (volumeSettings != null)
            {
                UpdateSettings(volumeSettings);
            }
        }

        public void UpdateSettings(VolumeSettingsData volumeSettings)
        {
            MasterVolume.value = volumeSettings.MasterVolume;
            MusicVolume.value = volumeSettings.MusicVolume;
            SFXVolume.value = volumeSettings.SfxVolume;
            VoiceVolume.value = volumeSettings.VoiceVolume;
        }

        public void UpdateMasterVolume()
        {
            Channels.OnMasterValueChanged?.Invoke(MasterVolume.value);
        }

        public void UpdateMusicVolume()
        {
            Channels.OnMusicChangedEvent?.Invoke(MusicVolume.value);
        }

        public void UpdateSFXVolume()
        {
            Channels.OnSFXChangedEvent?.Invoke(SFXVolume.value);
            FMODUnity.RuntimeManager.PlayOneShot(sfxSample);
        }

        public void UpdateVoiceVolume()
        {
            Channels.OnVoiceChangedEvent?.Invoke(VoiceVolume.value);
            FMODUnity.RuntimeManager.PlayOneShot(voiceSample);
        }
    }
}