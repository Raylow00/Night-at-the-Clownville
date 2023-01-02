using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_KeyGameObjectManager
{
    private GameObject testKeyGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator KeyGameObjectManager_InitKey()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testKeyGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/KeyLock/Test_Key_Game_Object"));
        KeyGameObjectManager testKeyGameObjectManager = testKeyGameObject.GetComponent<KeyGameObjectManager>();
        KeyLockStatusScriptableObject keyLockStatusSO = testKeyGameObjectManager.GetScriptableObject();

        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        bool isKeyObtained = testKeyGameObjectManager.GetIsKeyObtained();

        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(isKeyObtained);
        Assert.IsFalse(keyLockStatusSO.isKeyObtained);
    }

    [UnityTest]
    public IEnumerator KeyGameObjectManager_ObtainKey()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testKeyGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/KeyLock/Test_Key_Game_Object"));
        KeyGameObjectManager testKeyGameObjectManager = testKeyGameObject.GetComponent<KeyGameObjectManager>();
        KeyLockStatusScriptableObject keyLockStatusSO = testKeyGameObjectManager.GetScriptableObject();

        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testKeyGameObjectManager.ObtainKey();

        yield return null;

        bool isKeyObtained = testKeyGameObjectManager.GetIsKeyObtained();

        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(isKeyObtained);
        Assert.IsTrue(keyLockStatusSO.isKeyObtained);
    }
}
