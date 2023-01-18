using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_FloatEvent
{
    private GameObject testGameEventGameObject;

    [UnityTest]
    public IEnumerator Unittest_FloatEvent_RaiseEvent_ListenEvent()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        FloatEventTrigger testEventTrigger = testGameEventGameObject.GetComponent<FloatEventTrigger>();
        FloatEventListener testEventListener = testGameEventGameObject.GetComponent<FloatEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Event trigger set to trigger on start -> value given in Editor
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }
}
