using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettingsData
{
    private float musicVolume;
    private float sfxVolume;
    private float masterVolume;
    public VolumeSettingsData(float masterVolume, float musicVolume, float sfxVolume)
    {
        this.masterVolume = masterVolume;
        this.musicVolume = musicVolume;
        this.sfxVolume = sfxVolume;
    }

    public float MusicVolume { get => musicVolume; }
    public float SfxVolume { get => sfxVolume; }
    public float MasterVolume { get => masterVolume; }
}
