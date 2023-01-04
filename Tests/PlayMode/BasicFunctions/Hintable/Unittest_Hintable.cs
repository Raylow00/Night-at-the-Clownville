using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Hintable
{
    private GameObject testHintableGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_Hintable_SetAndGetHint()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testHintableGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/Hintable/Test_Hintable"));
        Hintable testHintable = testHintableGameObject.GetComponent<Hintable>();

        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testHintable.SetHint("This is a test hint");

        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        string EXP_hint = "This is a test hint";
        Assert.AreEqual(testHintable.GetHint(), EXP_hint);
    }
}
