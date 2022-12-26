using UnityEngine;
using UnityEngine.Audio;

/// <summary>
///     Enumeration for the types of audio mixer group used
/// </summary>
enum MixerGroup
{
    MASTER,
    MUSIC,
    SFX,
    AMBIENCE
}

/// <summary>
///     Sound class that encompasses properties of a sound
/// </summary>
[System.Serializable]
public class Sound
{
    #region Private Fields
    [SerializeField] private string audioName;

    [SerializeField] private AudioSettingsScriptableObject audioSettingsSO;
    [SerializeField] private MixerGroup mixerGroup;
    private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private AudioClip[] audioClips;
    
    [Range(0f, 1.01f)]
    [Tooltip("Setting audio volume to 1.01f forces it to use the volume from AudioSettingsSO")]
    [SerializeField] private float audioVolume;

    [Range(0.1f, 1f)]
    [SerializeField] private float audioPitch;

    [SerializeField] private bool toPlayRandomClip;
    [SerializeField] private bool isAudioLooping;
    [SerializeField] private bool toPlayOnAwake;
    #endregion

    #region Constructor
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="arg_audioSource">Audio source created at runtime for each sound</param>
    public Sound(AudioSource arg_audioSource)
    {
        InitAudioSource(arg_audioSource);
        InitAudioVolume();
        InitAudioPitch();
        InitAudioLooping();
        InitAudioMixerGroup();
    }
    #endregion

    #region Properties
    /// <summary>
    ///     Get audio name
    /// </summary>
    /// <returns>audioName</returns>
    public string GetAudioName()
    {
        return audioName;
    }
    #endregion

    #region Public Methods
    /// <summary>
    ///     Initialize each sound with their respective settings in the Awake function
    ///     in volume, pitch, audio mixer group and whether it is looping
    ///     If toPlayOnAwake is checked, play sound
    /// </summary>
    /// <param name="arg_audioSource"></param>
    public void InitSound(AudioSource arg_audioSource)
    {
        InitAudioSource(arg_audioSource);
        InitAudioVolume();
        InitAudioPitch();
        InitAudioLooping();
        InitAudioMixerGroup();

        if (toPlayOnAwake) PlaySound();
    }

    /// <summary>
    ///     Play sound
    /// </summary>
    public void PlaySound()
    {
        // Set audio clip
        int clipIndex = 0;
        if (toPlayRandomClip)
        {
            clipIndex = UnityEngine.Random.Range(0, audioClips.Length);
        }

        audioSource.clip = audioClips[clipIndex];

        // Set audio volume
        if (audioVolume < 1.01f)
        {
            audioSource.volume = audioVolume;
        }

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
    private void InitAudioSource(AudioSource arg_audioSource)
    {
        audioSource = arg_audioSource;
    }

    /// <summary>
    ///     Sets the mixer group to the mixer group assigned to this sound
    /// </summary>
    private void InitAudioMixerGroup()
    {
        audioSource.outputAudioMixerGroup = audioMixerGroup;
    }

    /// <summary>
    ///     Initializes the audio volume based on the mixer group type selected
    ///     Defaults to master volume
    /// </summary>
    private void InitAudioVolume()
    {
        switch (mixerGroup)
        {
            case MixerGroup.MASTER:
                audioSource.volume = audioSettingsSO.masterVolume;
                break;
            case MixerGroup.MUSIC:
                audioSource.volume = audioSettingsSO.musicVolume;
                break;
            case MixerGroup.SFX:
                audioSource.volume = audioSettingsSO.sfxVolume;
                break;
            case MixerGroup.AMBIENCE:
                audioSource.volume = audioSettingsSO.ambienceVolume;
                break;
            default:
                audioSource.volume = audioSettingsSO.masterVolume;
                break;
        }
    }

    /// <summary>
    ///     Initializes audio pitch to the value assigned to this sound
    /// </summary>
    private void InitAudioPitch()
    {
        audioSource.pitch = audioPitch;
    }

    /// <summary>
    ///     Sets whether this sound is looping
    /// </summary>
    private void InitAudioLooping()
    {
        audioSource.loop = isAudioLooping;
    }
    #endregion
}
