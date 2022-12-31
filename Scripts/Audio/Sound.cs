using UnityEngine;
using UnityEngine.Audio;

/// <summary>
///     Enumeration for the types of audio mixer group used
/// </summary>
public enum MixerGroup
{
    MASTER,
    MUSIC,
    SFX,
    AMBIENCE
}

/// <summary>
///     Sound class that encompasses properties of a sound
/// </summary>
public class Sound
{
    #region Private Fields
    private string audioName;
    private MixerGroup mixerGroup;
    private AudioSource audioSource;
    private AudioClip[] audioClips;
    #endregion

    #region Constructor
    /// <summary>
    ///     Initialize each sound with their respective settings in the Awake function
    ///     in volume, pitch, audio mixer group and whether it is looping
    ///     If toPlayOnAwake is checked, play sound
    /// </summary>
    /// <param name="arg_audioSource"></param>
    /// /// <param name="arg_audioSettingsSO"></param>
    public Sound(AudioSource arg_audioSource, 
                 AudioSettingsScriptableObject arg_audioSettingsSO,
                 AudioMixerGroup arg_audioMixerGroup,
                 MixerGroup arg_mixerGroup,
                 AudioClip[] arg_audioClips,
                 float arg_audioVolume,
                 float arg_audioPitch,
                 bool arg_isAudioLooping,
                 bool arg_toPlayOnAwake)
    {
        SetAudioSource(arg_audioSource);
        SetAudioMixerGroup(arg_audioMixerGroup, arg_mixerGroup);
        SetAudioClips(arg_audioClips);
        SetAudioVolume(arg_audioVolume, arg_audioSettingsSO);
        SetAudioPitch(arg_audioPitch);
        SetAudioLooping(arg_isAudioLooping);
        SetPlayOnAwake(arg_toPlayOnAwake);
    }
    #endregion

    #region Properties
    /// <summary>
    ///     Get audio name
    /// </summary>
    /// <returns>
    ///     Audio name in string
    /// </returns>
    public string GetAudioName()
    {
        if (audioName != null)
        {
            return audioName;
        }

        return "Audio name is empty";
    }

    /// <summary>
    ///     Get the audio source assigned to the sound
    /// </summary>
    /// <returns></returns>
    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    /// <summary>
    ///     Get the volume of the audio source
    /// </summary>
    /// <returns>
    ///     Audio volume in float
    /// </returns>
    public float GetAudioVolume()
    {
        return audioSource.volume;
    }

    /// <summary>
    ///     Get the pitch of the audio source
    /// </summary>
    /// <returns>
    ///     Audio pitch in float
    /// </returns>
    public float GetAudioPitch()
    {
        return audioSource.pitch;
    }

    /// <summary>
    ///     Get the property of audio source whether it is set to play on loop
    /// </summary>
    /// <returns>
    ///     True if audio is to play on loop
    ///     False otherwise
    /// </returns>
    public bool GetBoolIsAudioLooping()
    {
        return audioSource.loop;
    }

    /// <summary>
    ///     Get the property from audio source whether it is set to play on awake
    /// </summary>
    /// <returns>
    ///     True if audio is to play on awake
    ///     False otherwise
    /// </returns>
    public bool GetBoolToPlayOnAwake()
    {
        return audioSource.playOnAwake;
    }

    /// <summary>
    ///     Get whether the audio source is currently playing
    /// </summary>
    /// <returns>
    ///     True if audio is playing sound
    ///     False otherwise
    /// </returns>
    public bool GetBoolIsAudioPlaying()
    {
        return audioSource.isPlaying;
    }
    #endregion

    #region Public Methods
    /// <summary>
    ///     Play sound
    ///     If there are multiple clips, play random clip
    /// </summary>
    public void PlaySound()
    {
        // Set audio clip
        int clipIndex = 0;
        if (audioClips.Length > 1)
        {
            clipIndex = UnityEngine.Random.Range(0, audioClips.Length);
        }
        audioSource.clip = audioClips[clipIndex];

        audioSource.Play();
    }

    /// <summary>
    ///     Stop playing sound
    /// </summary>
    public void StopSound()
    {
        audioSource.Stop();
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Sets the audio source of a sound to the source passed in
    /// </summary>
    /// <param name="arg_audioSource"></param>
    private void SetAudioSource(AudioSource arg_audioSource)
    {
        audioSource = arg_audioSource;
    }

    /// <summary>
    ///     Sets the mixer group to the mixer group assigned to this sound
    /// </summary>
    /// <param name="arg_audioMixerGroup"></param>
    private void SetAudioMixerGroup(AudioMixerGroup arg_audioMixerGroup, MixerGroup arg_mixerGroup)
    {
        audioSource.outputAudioMixerGroup = arg_audioMixerGroup;
        mixerGroup = arg_mixerGroup;
    }

    /// <summary>
    ///     Initializes the audio volume based on the mixer group type selected
    ///     Defaults to master volume
    /// </summary>
    /// <param name="arg_audioVolume"></param>
    /// <param name="arg_audioSettingsSO"></param>
    private void SetAudioVolume(float arg_audioVolume, AudioSettingsScriptableObject arg_audioSettingsSO)
    {
        if (arg_audioVolume <= 1f)
        {
            audioSource.volume = arg_audioVolume;
        }
        else
        {
            switch (mixerGroup)
            {
                case MixerGroup.MASTER:
                    audioSource.volume = arg_audioSettingsSO.masterVolume;
                    break;
                case MixerGroup.MUSIC:
                    audioSource.volume = arg_audioSettingsSO.musicVolume;
                    break;
                case MixerGroup.SFX:
                    audioSource.volume = arg_audioSettingsSO.sfxVolume;
                    break;
                case MixerGroup.AMBIENCE:
                    audioSource.volume = arg_audioSettingsSO.ambienceVolume;
                    break;
                default:
                    audioSource.volume = arg_audioSettingsSO.masterVolume;
                    break;
            }
        }
    }

    /// <summary>
    ///     Initializes audio pitch to the value assigned to this sound
    /// </summary>
    /// <param name="arg_audioPitch"></param>
    private void SetAudioPitch(float arg_audioPitch)
    {
        audioSource.pitch = arg_audioPitch;
    }

    /// <summary>
    ///     Sets whether this sound is looping
    /// </summary>
    /// <param name="arg_isAudioLooping"></param>
    private void SetAudioLooping(bool arg_isAudioLooping)
    {
        audioSource.loop = arg_isAudioLooping;
    }

    /// <summary>
    ///     Sets whether audio starts playing on awake
    ///     When sound object is instantiated, it will automatically play the sound
    /// </summary>
    /// <param name="arg_toPlayOnAwake"></param>
    private void SetPlayOnAwake(bool arg_toPlayOnAwake)
    {
        audioSource.playOnAwake = arg_toPlayOnAwake;
        if (arg_toPlayOnAwake != false)
        {
            PlaySound();
        }
    }

    /// <summary>
    ///     Sets the audio clips to the class private variable
    /// </summary>
    /// <param name="arg_audioClips"></param>
    private void SetAudioClips(AudioClip[] arg_audioClips)
    {
        audioClips = arg_audioClips;
    }
    #endregion
}
