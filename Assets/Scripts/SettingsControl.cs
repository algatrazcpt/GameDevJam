using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsControl : MonoBehaviour
{

    public AudioMixer audioMixer;
    public void MusicVolumeSet(float value)
    {
        audioMixer.SetFloat("musicVolume", value);
    }
    public void VfxVolumeSet(float value)
    {
        audioMixer.SetFloat("vfxVolume", value);
    }
}
