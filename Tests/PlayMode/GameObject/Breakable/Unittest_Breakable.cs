using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Breakable
{
    private GameObject testBreakableGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_Breakable_BrokenObjectNotActive()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testBreakableGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/Breakable/Test_Breakable"));
        Breakable testBreakable = testBreakableGameObject.GetComponent<Breakable>();
        yield return new WaitForSeconds(1f);

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testBreakableGameObject);
    }

    [UnityTest]
    public IEnumerator Unittest_Breakable_BrokenObjectActive()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testBreakableGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/Breakable/Test_Breakable"));
        Breakable testBreakable = testBreakableGameObject.GetComponent<Breakable>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testBreakable.BreakObject();

        yield return new WaitForSeconds(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testBreakable.GetBrokenObject());
        Assert.IsFalse(testBreakableGameObject);
    }
}
