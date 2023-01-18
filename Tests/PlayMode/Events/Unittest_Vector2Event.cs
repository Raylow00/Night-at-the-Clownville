using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Vector2Event
{
    private GameObject testGameEventGameObject;

    [UnityTest]
    public IEnumerator Unittest_Vector2Event_RaiseEvent_ListenEvent()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        Vector2EventTrigger testEventTrigger = testGameEventGameObject.GetComponent<Vector2EventTrigger>();
        Vector2EventListener testEventListener = testGameEventGameObject.GetComponent<Vector2EventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Event trigger set to trigger on start -> value given in Editor
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }
}
