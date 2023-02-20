using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_LockGameObjectManager
{
    private GameObject testLockGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LockGameObjectManager_InitLock()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testLockGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/KeyLock/Test_Lock_Game_Object"));
        LockHandler testLockGameObjectManager = testLockGameObject.GetComponent<LockHandler>();
        KeyLockStatusScriptableObject keyLockStatusSO = testLockGameObjectManager.GetScriptableObject();

        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        bool isLockOpened = testLockGameObjectManager.GetIsLockOpened();

        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(isLockOpened);
        Assert.IsFalse(keyLockStatusSO.isLockOpened);
    }

    [UnityTest]
    public IEnumerator LockGameObjectManager_OpenLock()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testLockGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/KeyLock/Test_Lock_Game_Object"));
        LockHandler testLockGameObjectManager = testLockGameObject.GetComponent<LockHandler>();
        KeyLockStatusScriptableObject keyLockStatusSO = testLockGameObjectManager.GetScriptableObject();

        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testLockGameObjectManager.OpenLock();

        bool isLockOpened = testLockGameObjectManager.GetIsLockOpened();

        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(isLockOpened);
        Assert.IsTrue(keyLockStatusSO.isLockOpened);
    }

    [UnityTest]
    public IEnumerator LockGameObjectManager_CloseLock()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testLockGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/KeyLock/Test_Lock_Game_Object"));
        LockHandler testLockGameObjectManager = testLockGameObject.GetComponent<LockHandler>();
        KeyLockStatusScriptableObject keyLockStatusSO = testLockGameObjectManager.GetScriptableObject();

        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testLockGameObjectManager.OpenLock();

        bool isLockOpened = testLockGameObjectManager.GetIsLockOpened();

        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(isLockOpened);
        Assert.IsTrue(keyLockStatusSO.isLockOpened);

        //<-------------------------------- Test Execution ------------------------------>//
        testLockGameObjectManager.CloseLock();

        isLockOpened = testLockGameObjectManager.GetIsLockOpened();

        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(isLockOpened);
        Assert.IsFalse(keyLockStatusSO.isLockOpened);
    }
}
