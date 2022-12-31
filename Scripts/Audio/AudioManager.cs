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
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private AudioClip[] audioClips;

    [Range(0f, 1.01f)]
    [Tooltip("Setting audio volume to 1.01f forces it to use the volume from AudioSettingsSO")]
    [SerializeField] private float audioVolume = 0.5f;

    [Range(0.1f, 1f)]
    [SerializeField] private float audioPitch = 0.5f;

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
                          audioClips,
                          audioVolume,
                          audioPitch,
                          isAudioLooping,
                          toPlayOnAwake);
    }

    #region Properties
    /// <summary>
    ///     Get whether the sound is playing audio
    /// </summary>
    /// <returns>
    ///     True if sound is playing audio
    ///     False otherwise
    /// </returns>
    public bool GetBoolIsAudioPlaying()
    {
        return sound.GetBoolIsAudioPlaying();
    }

    /// <summary>
    ///     Get audio name
    /// </summary>
    /// <returns>Name of audio</returns>
    public string GetAudioName()
    {
        return sound.GetAudioName();
    }

    public bool GetBoolIsAudioLooping()
    {
        return sound.GetBoolIsAudioLooping();
    }

    public bool GetBoolToPlayOnAwake()
    {
        return sound.GetBoolToPlayOnAwake();
    }
    #endregion

    #region Public Methods
    /// <summary>
    ///     Finds the sound in the array and play audio
    /// </summary>
    public void PlayAudio()
    {
        sound.PlaySound();
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
