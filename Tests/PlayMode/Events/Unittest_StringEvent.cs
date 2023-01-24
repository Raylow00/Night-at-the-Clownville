using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_StringEvent
{
    private GameObject testGameEventGameObject;
    static string[] arg_stringValues = new string[] {"This is a test string on UI",
                                                     "This is another test string",
                                                     "This is the final test string"};
    [UnityTest]
    public IEnumerator Unittest_StringEvent_RaiseEvent_ListenEvent([ValueSource(nameof(arg_stringValues))] string arg_value)
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameEventGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Events/Test Game Event Game Object"));
        StringEventTrigger testEventTrigger = testGameEventGameObject.GetComponent<StringEventTrigger>();
        StringEventListener testEventListener = testGameEventGameObject.GetComponent<StringEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEventTrigger.TriggerEvent(arg_value);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEventListener.GetAcknowledgement() == true);
    }
}
