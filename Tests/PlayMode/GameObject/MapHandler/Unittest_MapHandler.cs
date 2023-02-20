using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_MapHandler
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_MapHandler_InitMap()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testMap = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/MapHandler/Test Player Map"));
        GameObject testMapHandlerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/MapHandler/Test Player Map Handler"));
        MapHandler testMapHandler = testMapHandlerGameObject.GetComponent<MapHandler>();

        //<-------------------------------- Test Execution ------------------------------>//
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testMapHandler.GetCurrentMapState() == MapState.MAP_DEACTIVATED);
    }

    [UnityTest]
    public IEnumerator Unittest_MapHandler_ActivateMap()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testMap = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/MapHandler/Test Player Map"));
        GameObject testMapHandlerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/MapHandler/Test Player Map Handler"));
        MapHandler testMapHandler = testMapHandlerGameObject.GetComponent<MapHandler>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testMapHandler.UseMap();
        yield return new WaitForSeconds(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testMapHandler.GetCurrentMapState() == MapState.MAP_ACTIVATED);
    }

    [UnityTest]
    public IEnumerator Unittest_MapHandler_FlipMap()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testMap = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/MapHandler/Test Player Map"));
        GameObject testMapHandlerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/MapHandler/Test Player Map Handler"));
        MapHandler testMapHandler = testMapHandlerGameObject.GetComponent<MapHandler>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testMapHandler.UseMap();
        yield return new WaitForSeconds(1f);
        testMapHandler.UseMap();

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testMapHandler.GetCurrentMapState() == MapState.MAP_FLIPPED);
    }
}
