using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    #region Fields
    [SerializeField] private string strAudioName;

    [SerializeField] private AudioSource audioSource_AudioSource;
    [SerializeField] private AudioMixerGroup audioMixerGroup_AudioMixerGroup;
    [SerializeField] private AudioClip[] audioClip_AudioClips;

    [Range(0f, 1f)]
    [SerializeField] private float f8audioVolume;

    [Range(0.1f, 3f)]
    [SerializeField] private float f8audioPitch;

    [SerializeField] private bool boIsAudioLooping;
    [SerializeField] private bool boToPlayOnAwake;
    #endregion

    #region Public Methods
    public void vInitSound(AudioSource audioSource)
    {
        vSetAudioSource(audioSource);
        vSetAudioVolume();
        vSetAudioPitch();
        vSetAudioLooping();
        vSetAudioMixerGroup();
    }
    #endregion

    #region Private Methods
    private void vSetAudioSource(AudioSource audioSource)
    {
        audioSource_AudioSource = audioSource;
    }

    private void vSetAudioVolume()
    {
        audioSource_AudioSource.volume = f8audioVolume;
    }

    private void vSetAudioPitch()
    {
        audioSource_AudioSource.pitch = f8audioPitch;
    }

    private void vSetAudioLooping()
    {
        audioSource_AudioSource.loop = boIsAudioLooping;
    }

    private void vSetAudioMixerGroup()
    {
        audioSource_AudioSource.outputAudioMixerGroup = audioMixerGroup_AudioMixerGroup;
    }
    #endregion
}
