using System;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
///     Audio manager class that manages/plays/stops audio
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private string audioManagerName;
    [SerializeField] private string audioName;

    [SerializeField] private AudioSettingsScriptableObject audioSettingsSO;

    [SerializeField] private MixerGroup mixerGroup;
    private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private AudioClip[] audioClips;

    [Range(0f, 1.01f)]
    [Tooltip("Setting audio volume to 1.01f forces it to use the volume from AudioSettingsSO")]
    [SerializeField] private float audioVolume = 0.5f;

    [Range(0.1f, 1f)]
    [SerializeField] private float audioPitch = 0.5f;

    [SerializeField] private bool toPlayRandomClip = false;
    [SerializeField] private bool isAudioLooping = false;
    [SerializeField] private bool toPlayOnAwake = false;
    #endregion

    #region Private Fields
    private Sound sound;
    #endregion

    /// <summary>
    ///     Initializes all sounds
    ///     To play sound if toPlayOnAwake in sound is checked
    /// </summary>
    void Awake()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        sound = new Sound(source, 
                          audioSettingsSO,
                          audioMixerGroup,
                          mixerGroup,
                          audioVolume,
                          audioPitch,
                          isAudioLooping,
                          toPlayOnAwake);
    }

    #region Public Methods
    /// <summary>
    ///     Finds the sound in the array and play audio
    /// </summary>
    public void PlayAudio()
    {
        sound.PlaySound(audioClips, toPlayRandomClip);
    }

    /// <summary>
    ///     Finds the sound in the array and stop audio
    /// </summary>
    public void StopAudio()
    {
        sound.StopSound();
    }
    #endregion
}
