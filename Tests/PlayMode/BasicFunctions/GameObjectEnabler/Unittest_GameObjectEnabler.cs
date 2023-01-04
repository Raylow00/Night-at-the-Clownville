using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_GameObjectEnabler
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_GameObjectEnabler_ToEnableOnStart()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/GameObjectEnabler/Test_Game_Object"));
        testGameObject.SetActive(false);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(testGameObject.activeSelf);

        //<-------------------------------- Test Execution ------------------------------>//
        GameObject testGameObjectEnablerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/GameObjectEnabler/Test_Game_Object_Enabler_ToEnableOnStart"));
        GameObjectEnabler testGameObjectEnabler = testGameObjectEnablerGameObject.GetComponent<GameObjectEnabler>();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameObjectEnabler.GetTargetGameObject().activeSelf);

    }

    [UnityTest]
    public IEnumerator Unittest_GameObjectEnabler_ToDisableOnStart()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/GameObjectEnabler/Test_Game_Object"));
        testGameObject.SetActive(true);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameObject.activeSelf);

        //<-------------------------------- Test Execution ------------------------------>//
        GameObject testGameObjectEnablerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/GameObjectEnabler/Test_Game_Object_Enabler_ToDisableOnStart"));
        GameObjectEnabler testGameObjectEnabler = testGameObjectEnablerGameObject.GetComponent<GameObjectEnabler>();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(testGameObjectEnabler.GetTargetGameObject().activeSelf);

    }

    [UnityTest]
    public IEnumerator Unittest_GameObjectEnabler_ToEnableAfterThisTime()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/GameObjectEnabler/Test_Game_Object"));
        testGameObject.SetActive(false);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(testGameObject.activeSelf);

        //<-------------------------------- Test Execution ------------------------------>//
        GameObject testGameObjectEnablerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/GameObjectEnabler/Test_Game_Object_Enabler_EnableAfterThisTime_5"));
        GameObjectEnabler testGameObjectEnabler = testGameObjectEnablerGameObject.GetComponent<GameObjectEnabler>();
        testGameObjectEnabler.EnableGameObject();
        yield return new WaitForSeconds(5f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameObjectEnabler.GetTargetGameObject().activeSelf);

    }

    [UnityTest]
    public IEnumerator Unittest_GameObjectEnabler_ToDisableAfterThisTime()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/GameObjectEnabler/Test_Game_Object"));
        testGameObject.SetActive(true);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameObject.activeSelf);

        //<-------------------------------- Test Execution ------------------------------>//
        GameObject testGameObjectEnablerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/GameObjectEnabler/Test_Game_Object_Enabler_DisableAfterThisTime_5"));
        GameObjectEnabler testGameObjectEnabler = testGameObjectEnablerGameObject.GetComponent<GameObjectEnabler>();
        testGameObjectEnabler.DisableGameObject();
        yield return new WaitForSeconds(5f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(testGameObjectEnabler.GetTargetGameObject().activeSelf);

    }
}
