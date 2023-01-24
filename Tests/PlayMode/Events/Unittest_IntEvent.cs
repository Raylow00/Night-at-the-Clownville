using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_IntEvent
{
    private GameObject testGameEventGameObject;
    static int[] arg_intValues = new int[] { 1, 5, 6 };

    [UnityTest]
    public IEnumerator Unittest_IntEvent_RaiseEvent_ListenEvent([ValueSource(nameof(arg_intValues))] int arg_value)
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        IntEventTrigger testEventTrigger = testGameEventGameObject.GetComponent<IntEventTrigger>();
        IntEventListener testEventListener = testGameEventGameObject.GetComponent<IntEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEventTrigger.TriggerEvent(arg_value);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }
}
