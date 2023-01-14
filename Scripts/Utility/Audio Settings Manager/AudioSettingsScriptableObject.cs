using UnityEngine;

/// <summary>
///     Audio settings to be stored in memory for use in any part of the program
/// </summary>
[CreateAssetMenu(fileName = "AudioSettingsScriptableObject", menuName = "Utility/Audio/Audio Settings")]
public class AudioSettingsScriptableObject : ScriptableObject
{
    public float minimumVolume = 0f;
    public float maximumVolume = 100f;
    public float minimumdB = -80f;
    public float maximumdB = 20f;

    public string masterVolumeParamName = "masterVolume";
    [Range(0f, 100f)]
    public float masterVolume;
    public float default_MasterVolume { get => 60f; }

    public string musicVolumeParamName = "musicVolume";
    [Range(0f, 100f)]
    public float musicVolume;
    public float default_MusicVolume { get => 60f; }

    public string sfxVolumeParamName = "sfxVolume";
    [Range(0f, 100f)]
    public float sfxVolume;
    public float default_SFXVolume { get => 60f; }

    public string ambienceVolumeParamName = "ambienceVolume";
    [Range(0f, 100f)]
    public float ambienceVolume;
    public float default_AmbienceVolume { get => 60f; }
}
