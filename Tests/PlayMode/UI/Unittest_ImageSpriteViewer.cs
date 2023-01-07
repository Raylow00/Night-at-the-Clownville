using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class Unittest_ImageSpriteViewer
{
    private GameObject testCanvasGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_ImageSpriteViewer_SpriteTest()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testCanvasGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/UI/Canvas"));
        GameObject testSpriteEventTriggerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/UI/Test Sprite Event Trigger"));
        GameObject testComparedImageSpriteGameObject = testCanvasGameObject.GetComponent<Transform>().GetChild(4).gameObject;
        Sprite testComparedImageSprite = testComparedImageSpriteGameObject.GetComponent<Image>().sprite;
        ImageSpriteViewer testImageSpriteViewer = testCanvasGameObject.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<ImageSpriteViewer>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        //<-------------------------------- Test Expectation ---------------------------->//
        Sprite returnSprite = testImageSpriteViewer.GetImageSprite();
        Assert.IsTrue(returnSprite == testComparedImageSprite);
    }
}
