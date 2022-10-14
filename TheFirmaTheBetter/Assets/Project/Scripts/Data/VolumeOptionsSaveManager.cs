using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    public class VolumeOptionsSaveManager : MonoBehaviour
    {
        [SerializeField]
        private Slider MasterVolume;
        [SerializeField]
        private Slider MusicVolume;
        [SerializeField]
        private Slider SFXVolume;
        [SerializeField]
        private Slider VoiceVolume;

        private void OnEnable()
        {
            LoadSettings();
        }

        public void SaveSettings()
        {
            SaveManager.Save(new VolumeSettingsData(MasterVolume.value, MusicVolume.value, SFXVolume.value, VoiceVolume.value), "volumesettings");
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
    }
}