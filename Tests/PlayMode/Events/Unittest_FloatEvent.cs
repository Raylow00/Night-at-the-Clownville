using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_FloatEvent
{
    private GameObject testGameEventGameObject;
    static float[] arg_floatValues = new float[] { 0.1f, 0.5f, 0.6f, 1.5f };

    [UnityTest]
    public IEnumerator Unittest_FloatEvent_RaiseEvent_ListenEvent([ValueSource(nameof(arg_floatValues))] float arg_value)
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        FloatEventTrigger testEventTrigger = testGameEventGameObject.GetComponent<FloatEventTrigger>();
        FloatEventListener testEventListener = testGameEventGameObject.GetComponent<FloatEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEventTrigger.TriggerEvent(arg_value);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }
}
