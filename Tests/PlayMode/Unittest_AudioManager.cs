using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;

public class Unittest_AudioManager
{
    private GameObject testAudioGameObject;
    private AudioManager testAudioManager;

    // A Test behaves as an ordinary method
    [Test]
    public void Unittest_AudioManager_SoundIsPlayingTrue_LoopingFalse()
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Audio/Test_Audio_Manager_SoundIsPlayingTrue_LoopingFalse"));
        testAudioManager = testAudioGameObject.GetComponent<AudioManager>();

        //<-------------------------------- Test Execution ------------------------------>//
        testAudioManager.PlayAudio();

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testAudioManager.GetBoolIsAudioPlaying());
        Assert.IsFalse(testAudioManager.GetBoolIsAudioLooping());

        //<-------------------------------- Test TearDown ------------------------------->//
        Object.DestroyImmediate(testAudioGameObject);
    }

    [Test]
    public void Unittest_AudioManager_SoundIsPlayingTrue_LoopingTrue()
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Audio/Test_Audio_Manager_SoundIsPlayingTrue_LoopingTrue"));
        testAudioManager = testAudioGameObject.GetComponent<AudioManager>();

        //<-------------------------------- Test Execution ------------------------------>//
        testAudioManager.PlayAudio();

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testAudioManager.GetBoolIsAudioPlaying());
        Assert.IsTrue(testAudioManager.GetBoolIsAudioLooping());

        //<-------------------------------- Test TearDown ------------------------------->//
        Object.DestroyImmediate(testAudioGameObject);
    }

    [Test]
    public void Unittest_AudioManager_SoundStopsPlaying()
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Audio/Test_Audio_Manager_SoundStopsPlaying"));
        testAudioManager = testAudioGameObject.GetComponent<AudioManager>();

        //<-------------------------------- Test Execution ------------------------------>//
        testAudioManager.PlayAudio();
        testAudioManager.StopAudio();

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(testAudioManager.GetBoolIsAudioPlaying());

        //<-------------------------------- Test TearDown ------------------------------->//
        Object.DestroyImmediate(testAudioGameObject);

    }
}
