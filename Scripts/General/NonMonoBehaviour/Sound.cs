using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public string audioName;
    [HideInInspector] public AudioSource audioSource;
    public AudioMixerGroup audioMixerGroup;
    public AudioClip[] audioClips;

    [Range(0f, 1f)]
    public float audioVolume;

    [Range(0.1f, 3f)]
    public float audioPitch;

    public bool isAudioLooping;
}
