using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Vector2Event
{
    private GameObject testGameEventGameObject;
    static Vector2[] arg_vector2Values = new Vector2[] { new Vector2(1, 2), new Vector2(0.1f, 0.2f), new Vector2(10, 20) };
    [UnityTest]
    public IEnumerator Unittest_Vector2Event_RaiseEvent_ListenEvent([ValueSource(nameof(arg_vector2Values))] Vector2 arg_value)
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        Vector2EventTrigger testEventTrigger = testGameEventGameObject.GetComponent<Vector2EventTrigger>();
        Vector2EventListener testEventListener = testGameEventGameObject.GetComponent<Vector2EventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEventTrigger.TriggerEvent(arg_value);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }
}
