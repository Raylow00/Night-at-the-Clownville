using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_AudioSettings", menuName = "Audio/Audio Settings")]
public class SO_AudioSettings : ScriptableObject
{
    [Range(0, 100f)]
    public float f8MasterVolume = 50f;
    [Range(0f, 100f)]
    public float f8MusicVolume = 50f;
    [Range(0f, 100f)]
    public float f8SfxVolume = 50f;
    [Range(0f, 100f)]
    public float f8AmbienceVolume = 50f;
}
