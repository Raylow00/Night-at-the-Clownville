using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAudioSettings", menuName = "Player/Audio Settings")]
public class PlayerAudioSettingsSO : ScriptableObject
{
    [Range(0, 100f)]
    public float masterVolume = 90f;
    [Range(0f, 100f)]
    public float musicVolume = 90f;
    [Range(0f, 100f)]
    public float sfxVolume = 90f;
    [Range(0f, 100f)]
    public float ambienceVolume = 90f;
}
