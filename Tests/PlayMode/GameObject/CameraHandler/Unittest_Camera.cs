using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Camera
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_Camera_Enable()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testCameraHandlerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/Camera/Test Camera Handler"));
        CameraHandler testCameraHandler = testCameraHandlerGameObject.GetComponent<CameraHandler>();

        //<-------------------------------- Test Execution ------------------------------>//
        testCameraHandler.EnableCameraGameObject(true);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testCameraHandler.GetCameraPrefab().activeSelf == testCameraHandler.GetIsCameraEquipped());
    }

    [UnityTest]
    public IEnumerator Unittest_Camera_Disable()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testCameraHandlerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/Camera/Test Camera Handler"));
        CameraHandler testCameraHandler = testCameraHandlerGameObject.GetComponent<CameraHandler>();

        //<-------------------------------- Test Execution ------------------------------>//
        testCameraHandler.EnableCameraGameObject(false);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testCameraHandler.GetCameraPrefab().activeSelf == testCameraHandler.GetIsCameraEquipped());
    }
}
