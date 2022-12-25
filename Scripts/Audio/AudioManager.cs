using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private string strAudioManagerName;
    [SerializeField] private Sound[] sound_Sounds;
    [SerializeField] private SO_AudioSettings so_AudioSettings;

    void Awake()
    {
        foreach (Sound s in sound_Sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            s.vInitSound(source);
        }
    }
}
