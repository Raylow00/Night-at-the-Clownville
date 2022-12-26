using System;
using UnityEngine;

/// <summary>
///     Audio manager class that manages/plays/stops audio
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private string audioManagerName;
    [SerializeField] private Sound[] sounds;
    #endregion

    /// <summary>
    ///     Initializes all sounds
    ///     To play sound if toPlayOnAwake in sound is checked
    /// </summary>
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.InitSound(source);
        }
    }

    #region Public Methods
    /// <summary>
    ///     Finds the sound in the array and play audio
    /// </summary>
    /// <param name="arg_audioName"></param>
    public void PlayAudio(string arg_audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.GetAudioName() == arg_audioName);
        if (s == null)
        {
            return;
        }
        else
        {
            s.PlaySound();
        }
    }

    /// <summary>
    ///     Finds the sound in the array and stop audio
    /// </summary>
    /// <param name="arg_audioName"></param>
    public void StopAudio(string arg_audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.GetAudioName() == arg_audioName);
        if (s == null)
        {
            return;
        }
        else
        {
            s.StopSound();
        }
    }
    #endregion
}
