using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Master Volume")]
    [SerializeField] private string masterVolumeParam;

    [Header("Music Volume")]
    [SerializeField] private string musicVolumeParam;

    [Header("SFX Volume")]
    [SerializeField] private string effectsVolumeParam;

    [Header("Ambience Volume")]
    [SerializeField] private string ambienceVolumeParam;

    [Header("Player Audio Settings")]
    [SerializeField] private PlayerAudioSettingsSO playerAudioSettingsSO;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        SetMasterVolume(playerAudioSettingsSO.masterVolume);
        SetMusicVolume(playerAudioSettingsSO.musicVolume);
        SetEffectsVolume(playerAudioSettingsSO.sfxVolume);
        SetAmbienceVolume(playerAudioSettingsSO.ambienceVolume);
    }

    public void SetMasterVolume(float value)
    {
        playerAudioSettingsSO.masterVolume = value;
        audioMixer.SetFloat(masterVolumeParam, MapValue(value, 0f, 100f, -60f, 10f));
    }

    public void SetMusicVolume(float value)
    {
        playerAudioSettingsSO.musicVolume = value;
        audioMixer.SetFloat(musicVolumeParam, MapValue(value, 0f, 100f, -60f, 10f));
    }

    public void SetEffectsVolume(float value)
    {
        playerAudioSettingsSO.sfxVolume = value;
        audioMixer.SetFloat(effectsVolumeParam, MapValue(value, 0f, 100f, -60f, 10f));
    }

    public void SetAmbienceVolume(float value)
    {
        playerAudioSettingsSO.ambienceVolume = value;
        audioMixer.SetFloat(ambienceVolumeParam, MapValue(value, 0f, 100f, -60f, 10f));
    }

    private float MapValue(float val, float originalMin, float originalMax, float finalMin, float finalMax)
    {
        return Mathf.Lerp(finalMin, finalMax, Mathf.InverseLerp(originalMin, originalMax, val));
    }
}
