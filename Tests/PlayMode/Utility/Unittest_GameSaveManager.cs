using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_GameSaveManager
{
    private GameObject testGameSaveManagerGameObject;
    private GameObject testGameSaveSettingsManagerGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_GameSaveManager_SaveGame_LoadGame()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameSaveManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameSaveManager/Test Game Save Manager"));
        testGameSaveSettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameSaveManager/Test Game Save Settings Manager"));
        GameSaveManager testGameSaveManager = testGameSaveManagerGameObject.GetComponent<GameSaveManager>();
        AudioSettingsManager testAudioSettingsManager = testGameSaveSettingsManagerGameObject.GetComponent<AudioSettingsManager>();
        VideoSettingsManager testVideoSettingsManager = testGameSaveSettingsManagerGameObject.GetComponent<VideoSettingsManager>();
        GameplaySettingsManager testGameplaySettingsManager = testGameSaveSettingsManagerGameObject.GetComponent<GameplaySettingsManager>();

        //<-------------------------------- Test Execution ------------------------------>//
        string testSaveSlotName = "testSaveSlot";
        testGameSaveManager.SaveGame(testSaveSlotName);
        yield return new WaitForSeconds(2f);

        //<-------------------------------- Test Expectation ---------------------------->//
        bool isAudioSettingsSaved = testAudioSettingsManager.LoadAudioSettings(testSaveSlotName);
        bool isVideoSettingsSaved = testVideoSettingsManager.LoadVideoSettings(testSaveSlotName);
        bool isGameplaySettingsSaved = testGameplaySettingsManager.LoadGameplaySettings(testSaveSlotName);

        Assert.IsTrue(isAudioSettingsSaved == true);
        Assert.IsTrue(isVideoSettingsSaved == true);
        Assert.IsTrue(isGameplaySettingsSaved == true);
    }
}
