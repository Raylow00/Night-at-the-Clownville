using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_VideoSettingsManager
{
    private GameObject testVideoSettingsManagerGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_InitDefaultSettings()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        //<-------------------------------- Test Expectation ---------------------------->//
        bool isFullScreen = testVideoSettingsManager.GetFullscreen();
        int fullScreenMode = testVideoSettingsManager.GetFullscreenMode();
        int videoQualityIndex = testVideoSettingsManager.GetVideoQualityIndex();
        int resolutionWidth = testVideoSettingsManager.GetResolutionWidth();
        int resolutionHeight = testVideoSettingsManager.GetResolutionHeight();

        bool isFullScreenInSO = testVideoSettingsManager.GetVideoSettingsScriptableObject().fullScreen;

        Assert.IsTrue(isFullScreen == false);   // change this value when doing a build => it will be true
        Assert.IsTrue(fullScreenMode == 3);     // change this value when doing a build => it will be 0
        Assert.IsTrue(videoQualityIndex == 2);
        Assert.IsTrue(resolutionHeight == 1080);
        Assert.IsTrue(resolutionWidth == 1920);

        Assert.IsTrue(isFullScreenInSO == true);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetFullScreen()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetFullScreen(true);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        bool isFullScreen = testVideoSettingsManager.GetFullscreen();
        bool isFullScreenInSO = testVideoSettingsManager.GetVideoSettingsScriptableObject().fullScreen;

        Assert.IsTrue(isFullScreen == false);           // change this value when doing a build => it will be true
        Assert.IsTrue(isFullScreenInSO == true);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetFullScreenMode_0()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetFullScreenMode(0);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int fullScreenMode = testVideoSettingsManager.GetFullscreenMode();
        int fullScreenModeInSO = testVideoSettingsManager.GetVideoSettingsScriptableObject().fullScreenMode;

        Assert.IsTrue(fullScreenMode == 3);         // change this value when doing a build => it will be 0
        Assert.IsTrue(fullScreenModeInSO == 0);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetFullScreenMode_1()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetFullScreenMode(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int fullScreenMode = testVideoSettingsManager.GetFullscreenMode();
        int fullScreenModeInSO = testVideoSettingsManager.GetVideoSettingsScriptableObject().fullScreenMode;

        Assert.IsTrue(fullScreenMode == 3);                 // change this value when doing a build => it will be 0
        Assert.IsTrue(fullScreenModeInSO == 1);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetFullScreenMode_2()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetFullScreenMode(2);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int fullScreenMode = testVideoSettingsManager.GetFullscreenMode();
        int fullScreenModeInSO = testVideoSettingsManager.GetVideoSettingsScriptableObject().fullScreenMode;

        Assert.IsTrue(fullScreenMode == 3);                 // change this value when doing a build => it will be 0
        Assert.IsTrue(fullScreenModeInSO == 2);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetFullScreenMode_3()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetFullScreenMode(3);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int fullScreenMode = testVideoSettingsManager.GetFullscreenMode();
        int fullScreenModeInSO = testVideoSettingsManager.GetVideoSettingsScriptableObject().fullScreenMode;

        Assert.IsTrue(fullScreenMode == 3);                 // change this value when doing a build => it will be 0
        Assert.IsTrue(fullScreenModeInSO == 3);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetResolution()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetResolution(0);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int resolutionWidth = testVideoSettingsManager.GetResolutionWidth();
        int resolutionHeight = testVideoSettingsManager.GetResolutionHeight();

        Assert.IsTrue(resolutionWidth == 1920);     // change this value when doing a build => it will be the width of the first available resolution
        Assert.IsTrue(resolutionHeight == 1080);    // change this value when doing a build => it will be the height of the first available resolution
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetResolution_HighestResolution()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetResolution(-1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int resolutionWidth = testVideoSettingsManager.GetResolutionWidth();
        int resolutionHeight = testVideoSettingsManager.GetResolutionHeight();

        Assert.IsTrue(resolutionWidth == 1920);     // change this value when doing a build => it will be the width of the first available resolution
        Assert.IsTrue(resolutionHeight == 1080);    // change this value when doing a build => it will be the height of the first available resolution
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetVideoQualityIndex_0()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetVideoQualityIndex(0);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int videoQualityIndex = testVideoSettingsManager.GetVideoQualityIndex();

        Assert.IsTrue(videoQualityIndex == 0);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetVideoQualityIndex_1()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetVideoQualityIndex(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int videoQualityIndex = testVideoSettingsManager.GetVideoQualityIndex();

        Assert.IsTrue(videoQualityIndex == 1);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SetVideoQualityIndex_2()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testVideoSettingsManager.SetVideoQualityIndex(2);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        int videoQualityIndex = testVideoSettingsManager.GetVideoQualityIndex();

        Assert.IsTrue(videoQualityIndex == 2);
    }

    [UnityTest]
    public IEnumerator Unittest_VideoSettingsManager_SaveVideoSettings()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testVideoSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/VideoSettingsManager/Test Video Settings Manager"));
        VideoSettingsManager testVideoSettingsManager = testVideoSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        string testSaveFolderName = "testSaveFolder";
        string testSaveSlotName = "testSaveSlot";

        testVideoSettingsManager.SaveVideoSettings(Application.persistentDataPath + "/" + testSaveFolderName + "/" + testSaveSlotName);
        yield return null;

        bool isGameSuccessfullyLoaded = testVideoSettingsManager.LoadVideoSettings(testSaveSlotName);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(isGameSuccessfullyLoaded == true);

        //<-------------------------------- Test TearDown ------------------------------->//

    }
}
