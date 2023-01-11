using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class VolumeSettingsData
    {
        private float musicVolume;
        private float sfxVolume;
        private float masterVolume;
        private float voiceVolume;
        public VolumeSettingsData(float masterVolume, float musicVolume, float sfxVolume,float voiceVolume)
        {
            this.masterVolume = masterVolume;
            this.musicVolume = musicVolume;
            this.sfxVolume = sfxVolume;
            this.voiceVolume = voiceVolume;
        }

        public float MusicVolume { get => musicVolume; }
        public float SfxVolume { get => sfxVolume; }
        public float MasterVolume { get => masterVolume; }
        public float VoiceVolume { get => voiceVolume; }
    }
}