using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;

public class Unittest_ScreenshotHandler
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_ScreenshotHandler_TakeScreenshot()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testCamera = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/ScreenshotHandler/Test Camera"));
        GameObject testScreenshotHandlerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/ScreenshotHandler/Test Screenshot Handler"));
        ScreenshotHandler testScreenshotHandler = testScreenshotHandlerGameObject.GetComponent<ScreenshotHandler>();
        testScreenshotHandler.SetCamera(testCamera.GetComponent<Camera>());

        //<-------------------------------- Test Execution ------------------------------>//
        testScreenshotHandler.TakeScreenshot(Screen.width, Screen.height);
        yield return new WaitForSeconds(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        string savedPath = Application.dataPath + "/CameraSreenshot_000.png";
        Assert.IsTrue(testScreenshotHandler.GetIsScreenshotTaken() == true);
        Assert.IsTrue(File.Exists(savedPath) == true);
    }
}
