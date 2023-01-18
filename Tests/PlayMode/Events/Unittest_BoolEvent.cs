using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_BoolEvent
{
    private GameObject testGameEventGameObject;

    [UnityTest]
    public IEnumerator Unittest_BoolEvent_RaiseEventTrue_ListenEvent()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        BoolEventTrigger testEventTrigger = testGameEventGameObject.GetComponent<BoolEventTrigger>();
        BoolEventListener testEventListener = testGameEventGameObject.GetComponent<BoolEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEventTrigger.TriggerEvent(true);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }

    [UnityTest]
    public IEnumerator Unittest_BoolEvent_RaiseEventFalse_ListenEvent()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        BoolEventTrigger testEventTrigger = testGameEventGameObject.GetComponent<BoolEventTrigger>();
        BoolEventListener testEventListener = testGameEventGameObject.GetComponent<BoolEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEventTrigger.TriggerEvent(false);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }
}
