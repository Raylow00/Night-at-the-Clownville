using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private AudioSettingsScriptableObject audioSettingsSO;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private FloatEvent onMasterVolumeChangeEvent;
    [SerializeField] private FloatEvent onMusicVolumeChangeEvent;
    [SerializeField] private FloatEvent onSFXVolumeChangeEvent;
    [SerializeField] private FloatEvent onAmbienceVolumeChangeEvent;
    #endregion

    #region Private Fields
    private static AudioSettingsManager instance;
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);

        InitDefaultAudioSettings();
    }

    #region Public Methods
    /// <summary>
    ///     Get the audio settings scriptable object 
    ///     as this is the place where its values are changed
    /// </summary>
    /// <returns></returns>
    public AudioSettingsScriptableObject GetAudioSettingsScriptableObject()
    {
        return audioSettingsSO;
    }

    /// <summary>
    ///     Get the audio mixer 
    ///     so that the actual dB volume can be accessed and assessed
    /// </summary>
    /// <returns></returns>
    public AudioMixer GetAudioMixer()
    {
        return audioMixer;
    }

    /// <summary>
    ///     Sets the master volume in the audio settings scriptable object
    ///     and map it to the audio mixer
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetMasterVolume(float arg_value)
    {
        // Set the value to SO
        audioSettingsSO.masterVolume = arg_value;
        // Set the value to audio mixer
        audioMixer.SetFloat(audioSettingsSO.masterVolumeParamName, MapValue(audioSettingsSO.masterVolume, audioSettingsSO.minimumVolume, audioSettingsSO.maximumVolume, audioSettingsSO.minimumDB, audioSettingsSO.maximumDB));
        // Send out event
        onMasterVolumeChangeEvent.Raise(audioSettingsSO.masterVolume);
    }

    /// <summary>
    ///     Sets the music volume in the audio settings scriptable object
    ///     and map it to the audio mixer
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetMusicVolume(float arg_value)
    {
        // Set the value to SO
        audioSettingsSO.musicVolume = arg_value;
        // Set the value to audio mixer
        audioMixer.SetFloat(audioSettingsSO.musicVolumeParamName, MapValue(audioSettingsSO.musicVolume, audioSettingsSO.minimumVolume, audioSettingsSO.maximumVolume, audioSettingsSO.minimumDB, audioSettingsSO.maximumDB));
        // Send out event
        onMusicVolumeChangeEvent.Raise(audioSettingsSO.musicVolume);
    }

    /// <summary>
    ///     Sets the SFX volume in the audio settings scriptable object
    ///     and map it to the audio mixer
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetSFXVolume(float arg_value)
    {
        // Set the value to SO
        audioSettingsSO.sfxVolume = arg_value;
        // Set the value to audio mixer
        audioMixer.SetFloat(audioSettingsSO.sfxVolumeParamName, MapValue(audioSettingsSO.sfxVolume, audioSettingsSO.minimumVolume, audioSettingsSO.maximumVolume, audioSettingsSO.minimumDB, audioSettingsSO.maximumDB));
        // Send out event
        onSFXVolumeChangeEvent.Raise(audioSettingsSO.sfxVolume);
    }

    /// <summary>
    ///     Sets the ambience volume in the audio settings scriptable object
    ///     and map it to the audio mixer
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetAmbienceVolume(float arg_value)
    {
        // Set the value to SO
        audioSettingsSO.ambienceVolume = arg_value;
        // Set the value to audio mixer
        audioMixer.SetFloat(audioSettingsSO.ambienceVolumeParamName, MapValue(audioSettingsSO.ambienceVolume, audioSettingsSO.minimumVolume, audioSettingsSO.maximumVolume, audioSettingsSO.minimumDB, audioSettingsSO.maximumDB));
        // Send out event
        onAmbienceVolumeChangeEvent.Raise(audioSettingsSO.ambienceVolume);
    }

    /// <summary>
    ///     Save audio settings by serializing into JSON format
    ///     TODO: To raise question whether to overwrite saved file
    /// </summary>
    /// <param name="arg_saveDir"></param>
    /// <returns></returns>
    public void SaveAudioSettings(string arg_saveDir)
    {
        string saveDir = arg_saveDir + "/playerAudioSettings";
        string[] tempSplit = arg_saveDir.Split('/');
        audioSettingsSO.saveFolder = tempSplit[tempSplit.Length - 2];
        string saveFilePath = saveDir + "/audioSettingsSO.txt";

        BinaryFormatter bf = new BinaryFormatter();

        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }
        
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }

        // Serialize Video Settings data
        FileStream file = File.Create(saveFilePath);
        var json = JsonUtility.ToJson(audioSettingsSO);
        bf.Serialize(file, json);
        file.Close();
    }

    /// <summary>
    ///     Load audio settings into the existing scriptable object
    /// </summary>
    /// <param name="arg_savedSlotName"></param>
    /// <returns></returns>
    public bool LoadAudioSettings(string arg_savedSlotName)
    {
        Debug.Log("Save folder name: " + audioSettingsSO.saveFolder);

        BinaryFormatter bf = new BinaryFormatter();

        string savedFile = Application.persistentDataPath + "/" + audioSettingsSO.saveFolder + "/" + arg_savedSlotName + "/playerAudioSettings/audioSettingsSO.txt";
        if (File.Exists(savedFile))
        {
            FileStream file = File.Open(savedFile, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), audioSettingsSO);

            return true;
        }
        else
        {
            Debug.LogError("Error opening: " + savedFile + "\nNo such file exists!");
            return false;
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Initialize the audio settings in audio mixer using the default values 
    ///     in the audio settings scriptable object
    /// </summary>
    private void InitDefaultAudioSettings()
    {
        audioSettingsSO.masterVolume = audioSettingsSO.default_MasterVolume;
        audioSettingsSO.musicVolume = audioSettingsSO.default_MusicVolume;
        audioSettingsSO.sfxVolume = audioSettingsSO.default_SFXVolume;
        audioSettingsSO.ambienceVolume = audioSettingsSO.default_AmbienceVolume;

        audioMixer.SetFloat(audioSettingsSO.masterVolumeParamName, MapValue(audioSettingsSO.masterVolume, audioSettingsSO.minimumVolume, audioSettingsSO.maximumVolume, audioSettingsSO.minimumDB, audioSettingsSO.maximumDB));
        audioMixer.SetFloat(audioSettingsSO.musicVolumeParamName, MapValue(audioSettingsSO.musicVolume, audioSettingsSO.minimumVolume, audioSettingsSO.maximumVolume, audioSettingsSO.minimumDB, audioSettingsSO.maximumDB));
        audioMixer.SetFloat(audioSettingsSO.sfxVolumeParamName, MapValue(audioSettingsSO.sfxVolume, audioSettingsSO.minimumVolume, audioSettingsSO.maximumVolume, audioSettingsSO.minimumDB, audioSettingsSO.maximumDB));
        audioMixer.SetFloat(audioSettingsSO.ambienceVolumeParamName, MapValue(audioSettingsSO.ambienceVolume, audioSettingsSO.minimumVolume, audioSettingsSO.maximumVolume, audioSettingsSO.minimumDB, audioSettingsSO.maximumDB));
    }

    /// <summary>
    ///     Map the input values to the output in audio mixer in dB
    /// </summary>
    /// <param name="val"></param>
    /// <param name="originalMin"></param>
    /// <param name="originalMax"></param>
    /// <param name="finalMin"></param>
    /// <param name="finalMax"></param>
    /// <returns></returns>
    private float MapValue(float val, float originalMin, float originalMax, float finalMin, float finalMax)
    {
        return Mathf.Lerp(finalMin, finalMax, Mathf.InverseLerp(originalMin, originalMax, val));
    }
    #endregion
}
