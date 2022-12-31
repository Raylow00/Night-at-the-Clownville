using System.Collections;
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

    /// <summary>
    ///     Get the audio mixer group
    /// </summary>
    /// <returns></returns>
    public AudioMixerGroup GetAudioMixerGroup()
    {
        return audioMixerGroup;
    }

    /// <summary>
    ///     Get audio volume
    /// </summary>
    /// <returns></returns>
    public float GetAudioVolume()
    {
        return sound.GetAudioVolume();
    }

    /// <summary>
    ///     Get whether the audio from the sound is looping
    /// </summary>
    /// <returns>
    ///     True if audio is set to looping
    ///     False otherwise
    /// </returns>
    public bool GetBoolIsAudioLooping()
    {
        return sound.GetBoolIsAudioLooping();
    }

    /// <summary>
    ///     Get whether audio is set to play on awake
    /// </summary>
    /// <returns>
    ///     True if audio is set to play on awake
    ///     False otherwise
    /// </returns>
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

    /// <summary>
    ///     Start the coroutine of fading the audio mixer
    /// </summary>
    /// <param name="arg_audioMixer"></param>
    /// <param name="arg_exposedParam"></param>
    /// <param name="arg_duration"></param>
    /// <param name="arg_targetVolume"></param>
    public void StartAudioMixerFadeCoroutine(AudioMixer arg_audioMixer, string arg_exposedParam, float arg_duration, float arg_targetVolume)
    {
        StartCoroutine(StartAudioMixerFade(arg_audioMixer, arg_exposedParam, arg_duration, arg_targetVolume));
    }

    /// <summary>
    ///     Start the coroutine of fading the audio source volume
    /// </summary>
    /// <param name="arg_duration"></param>
    /// <param name="arg_targetVolume"></param>
    public void StartAudioSourceFadeCoroutine(float arg_duration, float arg_targetVolume)
    {
        StartCoroutine(StartAudioSourceFade(arg_duration, arg_targetVolume));
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Fade the audio mixer group based on the parameter name set in the Audio Mixer tab
    /// </summary>
    /// <param name="arg_exposedParam"></param>
    /// <param name="arg_duration"></param>
    /// <param name="arg_targetVolume"></param>
    /// <returns></returns>
    private IEnumerator StartAudioMixerFade(AudioMixer arg_audioMixer, string arg_exposedParam, float arg_duration, float arg_targetVolume)
    {
        float currentTime = 0;

        float currentVol;

        arg_audioMixer.GetFloat(arg_exposedParam, out currentVol);

        currentVol = Mathf.Pow(10, currentVol / 20);

        float targetValue = Mathf.Clamp(arg_targetVolume, 0.0001f, 1);

        while (currentTime < arg_duration)
        {
            currentTime += Time.deltaTime;

            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / arg_duration);

            arg_audioMixer.SetFloat(arg_exposedParam, Mathf.Log10(newVol) * 20);

            yield return null;
        }

        yield break;
    }

    /// <summary>
    ///     Fade only the audio source volume for a sound
    /// </summary>
    /// <param name="arg_duration"></param>
    /// <param name="arg_targetVolume"></param>
    /// <returns></returns>
    private IEnumerator StartAudioSourceFade(float arg_duration, float arg_targetVolume)
    {
        // Debug.Log("Fading audio source");
        float currentTime = 0;
        float start = sound.GetAudioSource().volume;
        while (currentTime < arg_duration)
        {
            currentTime += Time.deltaTime;
            sound.GetAudioSource().volume = Mathf.Lerp(start, arg_targetVolume, currentTime / arg_duration);
            yield return null;
        }
        yield break;
    }
    #endregion
}
