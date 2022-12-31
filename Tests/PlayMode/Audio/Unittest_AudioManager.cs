using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

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

    [UnityTest]
    public IEnumerator Unittest_AudioManager_FadeAudioMixer()
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Audio/Test_Audio_Manager_SoundFadeAudioMixer"));
        testAudioManager = testAudioGameObject.GetComponent<AudioManager>();
        AudioMixer testAudioMixer = testAudioManager.GetAudioMixerGroup().audioMixer;
        //<-------------------------------- Test Execution ------------------------------>//
        testAudioManager.PlayAudio();
        testAudioManager.StartAudioMixerFadeCoroutine(testAudioMixer, "MasterVolume", 1f, 0f);

        yield return new WaitForSeconds(2f);

        //<-------------------------------- Test Expectation ---------------------------->//

        float OUT_currentVol;
        testAudioMixer.GetFloat("MasterVolume", out OUT_currentVol);

        float EXP_testAudioMixerVolume = -80f;
        Assert.That(OUT_currentVol, Is.EqualTo(EXP_testAudioMixerVolume).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test TearDown ------------------------------->//
        Object.DestroyImmediate(testAudioGameObject);

    }

    [UnityTest]
    public IEnumerator Unittest_AudioManager_FadeAudioSource()
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Audio/Test_Audio_Manager_SoundFadeAudioMixer"));
        testAudioManager = testAudioGameObject.GetComponent<AudioManager>();
        AudioMixerGroup testAudioMixerGroup = Resources.Load<AudioMixerGroup>("TestResources/EditMode/Audio/TestAudioMixer");

        //<-------------------------------- Test Execution ------------------------------>//
        testAudioManager.PlayAudio();
        testAudioManager.StartAudioSourceFadeCoroutine(1f, 0f);

        yield return new WaitForSeconds(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        float EXP_testAudioVolume = 0f;
        Assert.That(testAudioManager.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test TearDown ------------------------------->//
        Object.DestroyImmediate(testAudioGameObject);

    }
}
