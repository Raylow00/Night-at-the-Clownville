using UnityEngine;

/// <summary>
///     Audio settings to be stored in memory for use in any part of the program
/// </summary>
[CreateAssetMenu(fileName = "AudioSettingsScriptableObject", menuName = "Audio/Audio Settings")]
public class AudioSettingsScriptableObject : ScriptableObject
{
    [Range(0, 1f)]
    public float masterVolume = 0.5f;
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    [Range(0f, 1f)]
    public float sfxVolume = 0.5f;
    [Range(0f, 1f)]
    public float ambienceVolume = 0.5f;
}
