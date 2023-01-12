using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_GameplaySettingsManager
{
    private GameObject testGameplaySettingsManagerGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_InitKeyBindings()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.reloadKey == "r");
        Assert.IsTrue(testGameplaySettingsScriptableObject.crouchKey == "c");
        Assert.IsTrue(testGameplaySettingsScriptableObject.flashlightKey == "f");
        Assert.IsTrue(testGameplaySettingsScriptableObject.switchLastUsedWeaponKey == "q");
        Assert.IsTrue(testGameplaySettingsScriptableObject.equipCameraKey == "g");
        Assert.IsTrue(testGameplaySettingsScriptableObject.viewMapKey == "m"); 
    }

    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_OverrideKeyBindings_RemapReloadKey()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsManager testGameplaySettingsManager = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>();
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameplaySettingsManager.RemapReloadKey("d");
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.reloadKey == "d");
    }

    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_OverrideKeyBindings_RemapCrouchKey()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsManager testGameplaySettingsManager = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>();
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameplaySettingsManager.RemapCrouchKey("d");
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.crouchKey == "d");
    }

    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_OverrideKeyBindings_RemapFlashlightKey()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsManager testGameplaySettingsManager = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>();
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameplaySettingsManager.RemapFlashlightKey("d");
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.flashlightKey == "d");
    }

    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_OverrideKeyBindings_RemapSwitchLastUsedWeaponKey()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsManager testGameplaySettingsManager = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>();
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameplaySettingsManager.RemapSwitchLastUsedWeaponKey("d");
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.switchLastUsedWeaponKey == "d");
    }

    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_OverrideKeyBindings_RemapViewMapKey()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsManager testGameplaySettingsManager = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>();
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameplaySettingsManager.RemapViewMapKey("d");
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.viewMapKey == "d");
    }

    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_OverrideKeyBindings_RemapEquipCameraKey()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsManager testGameplaySettingsManager = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>();
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameplaySettingsManager.RemapEquipCameraKey("d");
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.equipCameraKey == "d");
    }

    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_OverrideKeyBindings_SetMouseSensitivity()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsManager testGameplaySettingsManager = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>();
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameplaySettingsManager.SetMouseSensitivity(15f);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.mouseSensitivityX == (1.6f * 15f));
        Assert.IsTrue(testGameplaySettingsScriptableObject.mouseSensitivityY == 15f);
    }

    [UnityTest]
    public IEnumerator Unittest_GameplaySettingsManager_OverrideKeyBindings_SetFieldOfView()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameplaySettingsManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/Test Gameplay Settings Manager"));
        GameplaySettingsManager testGameplaySettingsManager = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>();
        GameplaySettingsScriptableObject testGameplaySettingsScriptableObject = testGameplaySettingsManagerGameObject.GetComponent<GameplaySettingsManager>().GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameplaySettingsManager.SetFieldOfView(65f);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameplaySettingsScriptableObject.fieldOfView == 65f);
    }
}
