using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class Unittest_AudioSettingsManager
{
    private GameObject testAudioSettingsManagerGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_AudioSettingsManager_InitAudioSettings()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/AudioSettingsManager/Test Audio Settings Manager"));
        AudioSettingsManager testAudioSettingsManager = testAudioSettingsManagerGameObject.GetComponent<AudioSettingsManager>();
        AudioMixer testAudioMixer = testAudioSettingsManager.GetAudioMixer();
        AudioSettingsScriptableObject testAudioSettingsScriptableObject = testAudioSettingsManager.GetAudioSettingsScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        //<-------------------------------- Test Expectation ---------------------------->//
        float OUT_audioMixerMasterVolume;
        float OUT_audioMixerMusicVolume;
        float OUT_audioMixerSFXVolume;
        float OUT_audioMixerAmbienceVolume;
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.masterVolumeParamName, out OUT_audioMixerMasterVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.musicVolumeParamName, out OUT_audioMixerMusicVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.sfxVolumeParamName, out OUT_audioMixerSFXVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.ambienceVolumeParamName, out OUT_audioMixerAmbienceVolume);
        yield return null;

        float EXP_masterVolume = 0.6f;
        float EXP_musicVolume = 0.6f;
        float EXP_sfxVolume = 0.6f;
        float EXP_ambienceVolume = 0.6f;
        Assert.That(testAudioSettingsScriptableObject.masterVolume, Is.EqualTo(EXP_masterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.musicVolume, Is.EqualTo(EXP_musicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.sfxVolume, Is.EqualTo(EXP_sfxVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.ambienceVolume, Is.EqualTo(EXP_ambienceVolume).Using(FloatEqualityComparer.Instance));

        float EXP_audioMixerMasterVolume = -20f;
        float EXP_audioMixerMusicVolume = -20f;
        float EXP_audioMixerSFXVolume = -20f;
        float EXP_audioMixerAmbienceVolume = -20f;
        Assert.That(OUT_audioMixerMasterVolume, Is.EqualTo(EXP_audioMixerMasterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerMusicVolume, Is.EqualTo(EXP_audioMixerMusicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerSFXVolume, Is.EqualTo(EXP_audioMixerSFXVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerAmbienceVolume, Is.EqualTo(EXP_audioMixerAmbienceVolume).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test TearDown ------------------------------->//

    }

    [UnityTest]
    public IEnumerator Unittest_AudioSettingsManager_SetMasterVolume()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/AudioSettingsManager/Test Audio Settings Manager"));
        AudioSettingsManager testAudioSettingsManager = testAudioSettingsManagerGameObject.GetComponent<AudioSettingsManager>();
        AudioMixer testAudioMixer = testAudioSettingsManager.GetAudioMixer();
        AudioSettingsScriptableObject testAudioSettingsScriptableObject = testAudioSettingsManager.GetAudioSettingsScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testAudioSettingsManager.SetMasterVolume(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        float OUT_audioMixerMasterVolume;
        float OUT_audioMixerMusicVolume;
        float OUT_audioMixerSFXVolume;
        float OUT_audioMixerAmbienceVolume;
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.masterVolumeParamName, out OUT_audioMixerMasterVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.musicVolumeParamName, out OUT_audioMixerMusicVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.sfxVolumeParamName, out OUT_audioMixerSFXVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.ambienceVolumeParamName, out OUT_audioMixerAmbienceVolume);
        yield return null;

        float EXP_masterVolume = 1f;
        float EXP_musicVolume = 0.6f;
        float EXP_sfxVolume = 0.6f;
        float EXP_ambienceVolume = 0.6f;
        Assert.That(testAudioSettingsScriptableObject.masterVolume, Is.EqualTo(EXP_masterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.musicVolume, Is.EqualTo(EXP_musicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.sfxVolume, Is.EqualTo(EXP_sfxVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.ambienceVolume, Is.EqualTo(EXP_ambienceVolume).Using(FloatEqualityComparer.Instance));

        float EXP_audioMixerMasterVolume = 20f;
        float EXP_audioMixerMusicVolume = -20f;
        float EXP_audioMixerSFXVolume = -20f;
        float EXP_audioMixerAmbienceVolume = -20f;
        Assert.That(OUT_audioMixerMasterVolume, Is.EqualTo(EXP_audioMixerMasterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerMusicVolume, Is.EqualTo(EXP_audioMixerMusicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerSFXVolume, Is.EqualTo(EXP_audioMixerSFXVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerAmbienceVolume, Is.EqualTo(EXP_audioMixerAmbienceVolume).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test TearDown ------------------------------->//

    }

    [UnityTest]
    public IEnumerator Unittest_AudioSettingsManager_SetMusicVolume()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/AudioSettingsManager/Test Audio Settings Manager"));
        AudioSettingsManager testAudioSettingsManager = testAudioSettingsManagerGameObject.GetComponent<AudioSettingsManager>();
        AudioMixer testAudioMixer = testAudioSettingsManager.GetAudioMixer();
        AudioSettingsScriptableObject testAudioSettingsScriptableObject = testAudioSettingsManager.GetAudioSettingsScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testAudioSettingsManager.SetMusicVolume(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        float OUT_audioMixerMasterVolume;
        float OUT_audioMixerMusicVolume;
        float OUT_audioMixerSFXVolume;
        float OUT_audioMixerAmbienceVolume;
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.masterVolumeParamName, out OUT_audioMixerMasterVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.musicVolumeParamName, out OUT_audioMixerMusicVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.sfxVolumeParamName, out OUT_audioMixerSFXVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.ambienceVolumeParamName, out OUT_audioMixerAmbienceVolume);
        yield return null;

        float EXP_masterVolume = 0.6f;
        float EXP_musicVolume = 1f;
        float EXP_sfxVolume = 0.6f;
        float EXP_ambienceVolume = 0.6f;
        Assert.That(testAudioSettingsScriptableObject.masterVolume, Is.EqualTo(EXP_masterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.musicVolume, Is.EqualTo(EXP_musicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.sfxVolume, Is.EqualTo(EXP_sfxVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.ambienceVolume, Is.EqualTo(EXP_ambienceVolume).Using(FloatEqualityComparer.Instance));

        float EXP_audioMixerMasterVolume = -20f;
        float EXP_audioMixerMusicVolume = 20f;
        float EXP_audioMixerSFXVolume = -20f;
        float EXP_audioMixerAmbienceVolume = -20f;
        Assert.That(OUT_audioMixerMasterVolume, Is.EqualTo(EXP_audioMixerMasterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerMusicVolume, Is.EqualTo(EXP_audioMixerMusicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerSFXVolume, Is.EqualTo(EXP_audioMixerSFXVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerAmbienceVolume, Is.EqualTo(EXP_audioMixerAmbienceVolume).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test TearDown ------------------------------->//

    }

    [UnityTest]
    public IEnumerator Unittest_AudioSettingsManager_SetSFXVolume()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/AudioSettingsManager/Test Audio Settings Manager"));
        AudioSettingsManager testAudioSettingsManager = testAudioSettingsManagerGameObject.GetComponent<AudioSettingsManager>();
        AudioMixer testAudioMixer = testAudioSettingsManager.GetAudioMixer();
        AudioSettingsScriptableObject testAudioSettingsScriptableObject = testAudioSettingsManager.GetAudioSettingsScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testAudioSettingsManager.SetSFXVolume(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        float OUT_audioMixerMasterVolume;
        float OUT_audioMixerMusicVolume;
        float OUT_audioMixerSFXVolume;
        float OUT_audioMixerAmbienceVolume;
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.masterVolumeParamName, out OUT_audioMixerMasterVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.musicVolumeParamName, out OUT_audioMixerMusicVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.sfxVolumeParamName, out OUT_audioMixerSFXVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.ambienceVolumeParamName, out OUT_audioMixerAmbienceVolume);
        yield return null;

        float EXP_masterVolume = 0.6f;
        float EXP_musicVolume = 0.6f;
        float EXP_sfxVolume = 1f;
        float EXP_ambienceVolume = 0.6f;
        Assert.That(testAudioSettingsScriptableObject.masterVolume, Is.EqualTo(EXP_masterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.musicVolume, Is.EqualTo(EXP_musicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.sfxVolume, Is.EqualTo(EXP_sfxVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.ambienceVolume, Is.EqualTo(EXP_ambienceVolume).Using(FloatEqualityComparer.Instance));

        float EXP_audioMixerMasterVolume = -20f;
        float EXP_audioMixerMusicVolume = -20f;
        float EXP_audioMixerSFXVolume = 20f;
        float EXP_audioMixerAmbienceVolume = -20f;
        Assert.That(OUT_audioMixerMasterVolume, Is.EqualTo(EXP_audioMixerMasterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerMusicVolume, Is.EqualTo(EXP_audioMixerMusicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerSFXVolume, Is.EqualTo(EXP_audioMixerSFXVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerAmbienceVolume, Is.EqualTo(EXP_audioMixerAmbienceVolume).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test TearDown ------------------------------->//

    }

    [UnityTest]
    public IEnumerator Unittest_AudioSettingsManager_SetAmbienceVolume()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/AudioSettingsManager/Test Audio Settings Manager"));
        AudioSettingsManager testAudioSettingsManager = testAudioSettingsManagerGameObject.GetComponent<AudioSettingsManager>();
        AudioMixer testAudioMixer = testAudioSettingsManager.GetAudioMixer();
        AudioSettingsScriptableObject testAudioSettingsScriptableObject = testAudioSettingsManager.GetAudioSettingsScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testAudioSettingsManager.SetAmbienceVolume(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        float OUT_audioMixerMasterVolume;
        float OUT_audioMixerMusicVolume;
        float OUT_audioMixerSFXVolume;
        float OUT_audioMixerAmbienceVolume;
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.masterVolumeParamName, out OUT_audioMixerMasterVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.musicVolumeParamName, out OUT_audioMixerMusicVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.sfxVolumeParamName, out OUT_audioMixerSFXVolume);
        testAudioMixer.GetFloat(testAudioSettingsScriptableObject.ambienceVolumeParamName, out OUT_audioMixerAmbienceVolume);
        yield return null;

        float EXP_masterVolume = 0.6f;
        float EXP_musicVolume = 0.6f;
        float EXP_sfxVolume = 0.6f;
        float EXP_ambienceVolume = 1f;
        Assert.That(testAudioSettingsScriptableObject.masterVolume, Is.EqualTo(EXP_masterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.musicVolume, Is.EqualTo(EXP_musicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.sfxVolume, Is.EqualTo(EXP_sfxVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testAudioSettingsScriptableObject.ambienceVolume, Is.EqualTo(EXP_ambienceVolume).Using(FloatEqualityComparer.Instance));

        float EXP_audioMixerMasterVolume = -20f;
        float EXP_audioMixerMusicVolume = -20f;
        float EXP_audioMixerSFXVolume = -20f;
        float EXP_audioMixerAmbienceVolume = 20f;
        Assert.That(OUT_audioMixerMasterVolume, Is.EqualTo(EXP_audioMixerMasterVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerMusicVolume, Is.EqualTo(EXP_audioMixerMusicVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerSFXVolume, Is.EqualTo(EXP_audioMixerSFXVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(OUT_audioMixerAmbienceVolume, Is.EqualTo(EXP_audioMixerAmbienceVolume).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test TearDown ------------------------------->//

    }

    [UnityTest]
    public IEnumerator Unittest_AudioSettingsManager_SaveAudioSettings()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testAudioSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/AudioSettingsManager/Test Audio Settings Manager"));
        AudioSettingsManager testAudioSettingsManager = testAudioSettingsManagerGameObject.GetComponent<AudioSettingsManager>();

        //<-------------------------------- Test Execution ------------------------------>//
        string testSaveFolderName = "testSaveFolder";
        string testSaveSlotName = "testSaveSlot";

        testAudioSettingsManager.SaveAudioSettings(Application.persistentDataPath + "/" + testSaveFolderName + "/" + testSaveSlotName);
        yield return null;

        bool isGameSuccessfullyLoaded = testAudioSettingsManager.LoadAudioSettings(testSaveSlotName);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(isGameSuccessfullyLoaded == true);

        //<-------------------------------- Test TearDown ------------------------------->//

    }
}
