using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private bool toPlayRandomClip;
    [SerializeField] private bool toPlayOnAwake;
    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.playOnAwake = toPlayOnAwake;
            s.audioSource.volume = s.audioVolume;
            s.audioSource.pitch = s.audioPitch;
            s.audioSource.loop = s.isAudioLooping;
            s.audioSource.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }

    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == name);

        if(s == null)
        {
            // Debug.LogWarning("Sound " + name + " does not exist!");
            return;
        }
        else
        {
            if(toPlayRandomClip)
            {
                s.audioSource.clip = s.audioClips[UnityEngine.Random.Range(0, s.audioClips.Length)];
            }
            else
            {
                s.audioSource.clip = s.audioClips[0];
            }

            s.audioSource.Play();

            //Debug.Log("Audio played: " + name);
        }
    }

    public void StopAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == name);

        if(s == null)
        {
            // Debug.LogWarning("Sound " + name + " does not exist!");
            return;
        }
        else
        {
            s.audioSource.Stop();
        }
    }
}
