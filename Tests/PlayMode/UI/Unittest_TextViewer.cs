using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_TextViewer
{
    private GameObject testCanvasGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_TextViewer_StringTest()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testCanvasGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/UI/Canvas"));
        GameObject testStringEventTriggerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/UI/Test String Event Trigger"));
        TextViewer testStringTextViewer = testCanvasGameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextViewer>();

        string arg_stringToSend = "This is a test string on UI";

        //<-------------------------------- Test Execution ------------------------------>//
        StringEventTrigger testStringEventTrigger = testStringEventTriggerGameObject.GetComponent<StringEventTrigger>();
        testStringEventTrigger.TriggerEvent(arg_stringToSend);

        //<-------------------------------- Test Expectation ---------------------------->//

        yield return new WaitForSeconds(0.1f);
        string returnText = testStringTextViewer.GetText();
        Assert.IsTrue(returnText == arg_stringToSend);
    }

    [UnityTest]
    public IEnumerator Unittest_TextViewer_FloatTest()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testCanvasGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/UI/Canvas"));
        GameObject testFloatEventTriggerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/UI/Test Float Event Trigger"));
        TextViewer testFloatTextViewer = testCanvasGameObject.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<TextViewer>();

        float arg_floatToSend = 1.5f;

        //<-------------------------------- Test Execution ------------------------------>//
        FloatEventTrigger testFloatEventTrigger = testFloatEventTriggerGameObject.GetComponent<FloatEventTrigger>();
        testFloatEventTrigger.TriggerEvent(arg_floatToSend);

        //<-------------------------------- Test Expectation ---------------------------->//

        yield return new WaitForSeconds(0.1f);
        string returnText = testFloatTextViewer.GetText();
        
        Assert.IsTrue(returnText == Mathf.RoundToInt(arg_floatToSend).ToString());
    }

    [UnityTest]
    public IEnumerator Unittest_TextViewer_IntTest()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testCanvasGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/UI/Canvas"));
        GameObject testIntEventTriggerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/UI/Test Int Event Trigger"));
        TextViewer testIntTextViewer = testCanvasGameObject.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<TextViewer>();

        int arg_intToSend = 1;

        //<-------------------------------- Test Execution ------------------------------>//
        IntEventTrigger testIntEventTrigger = testIntEventTriggerGameObject.GetComponent<IntEventTrigger>();
        testIntEventTrigger.TriggerEvent(arg_intToSend);

        //<-------------------------------- Test Expectation ---------------------------->//

        yield return new WaitForSeconds(0.1f);
        string returnText = testIntTextViewer.GetText();
        Assert.IsTrue(returnText == arg_intToSend.ToString());
    }
}
