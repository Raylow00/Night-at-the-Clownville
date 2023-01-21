using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools.Utils;

public class Unittest_Sound
{
    private GameObject testAudioGameObject;
    private AudioSource testAudioSource;
    private AudioMixerGroup testAudioMixerGroup;
    private AudioSettingsScriptableObject testAudioSettingsSO;

    [SetUp]
    public void Setup()
    {
        testAudioGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/EditMode/Audio/Test_Audio_Manager_1"));
        testAudioSource = testAudioGameObject.AddComponent<AudioSource>();
        testAudioMixerGroup = Resources.Load<AudioMixerGroup>("TestResources/EditMode/Audio/TestAudioMixer");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(testAudioGameObject);
    }

    /// <summary>
    ///     To test sound game object playing
    ///     - single sound clip
    ///     - using volume from player input
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.3f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Master
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(0.3f, 1f, true, false, MixerGroup.MASTER)]
    public void Unittest_Sound_Initialization_SingleSound_PlayerInputVolume_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                     float INP_testPitch,
                                                                                                     bool INP_isLooping,
                                                                                                     bool INP_toPlayOnAwake,
                                                                                                     MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip[] testAudioClips = new AudioClip[] { Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip") };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.3f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }


    /// <summary>
    ///     To test sound game object playing
    ///     - single sound clip
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.1f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Master
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, true, false, MixerGroup.MASTER)]
    public void Unittest_Sound_Initialization_SingleSound_DefaultVolumeFromScriptableObjectMaster_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                                           float INP_testPitch,
                                                                                                                           bool INP_isLooping,
                                                                                                                           bool INP_toPlayOnAwake,
                                                                                                                           MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip[] testAudioClips = new AudioClip[] { Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip") };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - single sound clip
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.2f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Music
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, true, false, MixerGroup.MUSIC)]
    public void Unittest_Sound_Initialization_SingleSound_DefaultVolumeFromScriptableObjectMusic_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                                          float INP_testPitch,
                                                                                                                          bool INP_isLooping,
                                                                                                                          bool INP_toPlayOnAwake,
                                                                                                                          MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip[] testAudioClips = new AudioClip[] { Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip") };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - single sound clip
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.3f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = SFX
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, true, false, MixerGroup.SFX)]
    public void Unittest_Sound_Initialization_SingleSound_DefaultVolumeFromScriptableObjectSFX_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                                        float INP_testPitch,
                                                                                                                        bool INP_isLooping,
                                                                                                                        bool INP_toPlayOnAwake,
                                                                                                                        MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip[] testAudioClips = new AudioClip[] { Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip") };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - single sound clip
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.4f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Ambience
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, true, false, MixerGroup.AMBIENCE)]
    public void Unittest_Sound_Initialization_SingleSound_DefaultVolumeFromScriptableObjectAmbience_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                                             float INP_testPitch,
                                                                                                                             bool INP_isLooping,
                                                                                                                             bool INP_toPlayOnAwake,
                                                                                                                             MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip[] testAudioClips = new AudioClip[] { Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip") };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");
        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - single sound clip
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is not looping
    ///     - is playing on awake
    ///     Expected output:
    ///     - volume = 0.4f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Ambience
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, false, true, MixerGroup.AMBIENCE)]
    public void Unittest_Sound_Initialization_SingleSound_DefaultVolumeFromScriptableObjectAmbience_LoopingFalse_PlayOnAwakeTrue(float INP_testVolume,
                                                                                                                                 float INP_testPitch,
                                                                                                                                 bool INP_isLooping,
                                                                                                                                 bool INP_toPlayOnAwake,
                                                                                                                                 MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip[] testAudioClips = new AudioClip[] { Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip") };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioLooping());
        Assert.IsTrue(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - multipe sound clips
    ///     - using volume from player input
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.55f;
    ///     - pitch = 0.5f;
    ///     - isLooping = false;
    ///     - toPlayOnAwake = true;
    ///     - MixerGroup = Master
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(0.55f, 0.5f, true, false, MixerGroup.MASTER)]
    public void Unittest_Sound_Initialization_MultipleSound_PlayerInputVolume_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                       float INP_testPitch,
                                                                                                       bool INP_isLooping,
                                                                                                       bool INP_toPlayOnAwake,
                                                                                                       MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip testAudioClip = Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip");
        AudioClip[] testAudioClips = new AudioClip[] { testAudioClip, testAudioClip, testAudioClip };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.55f;
        float EXP_testAudioPitch = 0.5f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - multiple sound clips
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.1f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Master
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, true, false, MixerGroup.MASTER)]
    public void Unittest_Sound_Initialization_MultipleSound_DefaultVolumeFromScriptableObjectMaster_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                                             float INP_testPitch,
                                                                                                                             bool INP_isLooping,
                                                                                                                             bool INP_toPlayOnAwake,
                                                                                                                             MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip testAudioClip = Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip");
        AudioClip[] testAudioClips = new AudioClip[] { testAudioClip, testAudioClip, testAudioClip };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - multiple sound clips
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.2f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Music
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, true, false, MixerGroup.MUSIC)]
    public void Unittest_Sound_Initialization_MultipleSound_DefaultVolumeFromScriptableObjectMusic_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                                            float INP_testPitch,
                                                                                                                            bool INP_isLooping,
                                                                                                                            bool INP_toPlayOnAwake,
                                                                                                                            MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip testAudioClip = Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip");
        AudioClip[] testAudioClips = new AudioClip[] { testAudioClip, testAudioClip, testAudioClip };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - multiple sound clips
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.3f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = SFX
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, true, false, MixerGroup.SFX)]
    public void Unittest_Sound_Initialization_MultipleSound_DefaultVolumeFromScriptableObjectSFX_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                                          float INP_testPitch,
                                                                                                                          bool INP_isLooping,
                                                                                                                          bool INP_toPlayOnAwake,
                                                                                                                          MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip testAudioClip = Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip");
        AudioClip[] testAudioClips = new AudioClip[] { testAudioClip, testAudioClip, testAudioClip };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - multiple sound clips
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is looping
    ///     - is not playing on awake
    ///     Expected output:
    ///     - volume = 0.4f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Ambience
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, true, false, MixerGroup.AMBIENCE)]
    public void Unittest_Sound_Initialization_MultipleSound_DefaultVolumeFromScriptableObjectAmbience_Looping_PlayOnAwakeFalse(float INP_testVolume,
                                                                                                                               float INP_testPitch,
                                                                                                                               bool INP_isLooping,
                                                                                                                               bool INP_toPlayOnAwake,
                                                                                                                               MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip testAudioClip = Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip");
        AudioClip[] testAudioClips = new AudioClip[] { testAudioClip, testAudioClip, testAudioClip };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsTrue(testSound.GetBoolIsAudioLooping());
        Assert.IsFalse(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }

    /// <summary>
    ///     To test sound game object playing
    ///     - multiple sound clips
    ///     - using volume from scriptable object (volume > 1f)
    ///     - is not looping
    ///     - is playing on awake
    ///     Expected output:
    ///     - volume = 0.4f;
    ///     - pitch = 1f;
    ///     - isLooping = true;
    ///     - toPlayOnAwake = false;
    ///     - MixerGroup = Ambience
    /// </summary>
    /// <param name="INP_testVolume"></param>
    /// <param name="INP_testPitch"></param>
    /// <param name="INP_isLooping"></param>
    /// <param name="INP_toPlayOnAwake"></param>
    /// <param name="INP_mixerGroup""></param>
    [Test]
    [TestCase(1.01f, 1f, false, true, MixerGroup.AMBIENCE)]
    public void Unittest_Sound_Initialization_MultipleSound_DefaultVolumeFromScriptableObjectAmbience_LoopingFalse_PlayOnAwakeTrue(float INP_testVolume,
                                                                                                                                   float INP_testPitch,
                                                                                                                                   bool INP_isLooping,
                                                                                                                                   bool INP_toPlayOnAwake,
                                                                                                                                   MixerGroup INP_mixerGroup)
    {
        // Use the Assert class to test conditions

        //<-------------------------------- Test Setup ---------------------------------->//

        AudioClip testAudioClip = Resources.Load<AudioClip>("TestResources/EditMode/Audio/test_audio_clip");
        AudioClip[] testAudioClips = new AudioClip[] { testAudioClip, testAudioClip, testAudioClip };
        testAudioSettingsSO = Resources.Load<AudioSettingsScriptableObject>("TestResources/EditMode/Audio/TestAudioSettingsScriptableObject_Sound");

        Sound testSound = new Sound(testAudioSource,
                                    testAudioSettingsSO,
                                    testAudioMixerGroup,
                                    INP_mixerGroup,
                                    testAudioClips,
                                    INP_testVolume,
                                    INP_testPitch,
                                    INP_isLooping,
                                    INP_toPlayOnAwake);

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.PlaySound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioLooping());
        Assert.IsTrue(testSound.GetBoolToPlayOnAwake());
        Assert.IsTrue(testSound.GetBoolIsAudioPlaying());

        float EXP_testAudioVolume = 0.6f;
        float EXP_testAudioPitch = 1f;

        Assert.That(testSound.GetAudioVolume, Is.EqualTo(EXP_testAudioVolume).Using(FloatEqualityComparer.Instance));
        Assert.That(testSound.GetAudioPitch, Is.EqualTo(EXP_testAudioPitch).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//

        testSound.StopSound();

        //<-------------------------------- Test Expectation ---------------------------->//

        Assert.IsFalse(testSound.GetBoolIsAudioPlaying());

    }
}
