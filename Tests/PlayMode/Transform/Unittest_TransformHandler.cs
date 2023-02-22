using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_TransformHandler
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_TransformHandler_SetPosition()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testTransform = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Transform/Test Player Transform"));
        TransformHandler testTransformHandler = testTransform.GetComponent<TransformHandler>();
        Vector3 newPosition = new Vector3(5f, 1f, 4f);

        //<-------------------------------- Test Execution ------------------------------>//
        testTransformHandler.SetPosition(newPosition);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testTransformHandler.GetPosition() == newPosition);
    }

    [UnityTest]
    public IEnumerator Unittest_TransformHandler_SetRotation()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testTransform = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Transform/Test Player Transform"));
        TransformHandler testTransformHandler = testTransform.GetComponent<TransformHandler>();
        Quaternion newRotation = new Quaternion(0.5f, 0.9f, 0.4f, 1f);

        //<-------------------------------- Test Execution ------------------------------>//
        testTransformHandler.SetRotation(newRotation);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testTransformHandler.GetRotation() == newRotation);
    }

    [UnityTest]
    public IEnumerator Unittest_TransformHandler_SetScale()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testTransform = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Transform/Test Player Transform"));
        TransformHandler testTransformHandler = testTransform.GetComponent<TransformHandler>();
        Vector3 newScale = new Vector3(5f, 1f, 2f);

        //<-------------------------------- Test Execution ------------------------------>//
        testTransformHandler.SetScale(newScale);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testTransformHandler.GetScale() == newScale);
    }
}
