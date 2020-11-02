using Aura2API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeVolumeMaster : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public void SetMusicVolume(float musicVolume)
    {
        _audioMixer.SetFloat("Music", musicVolume);
    }

    public void SetSfxVolume(float sfxVolume)
    {
        _audioMixer.SetFloat("SFX", sfxVolume);
    }
}
