using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_VoidEvent
{
    private GameObject testGameEventGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_VoidEvent_RaiseEvent_ListenEvent()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        VoidEventTrigger testEventTrigger = testGameEventGameObject.GetComponent<VoidEventTrigger>();
        VoidEventListener testEventListener = testGameEventGameObject.GetComponent<VoidEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Event trigger set to trigger on start -> value given in Editor
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }
}
