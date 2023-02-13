using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Flashlight
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_Flashlight_Init()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testFlashlightGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/Flashlight/Test Flashlight"));
        Flashlight testFlashlight = testFlashlightGameObject.GetComponent<Flashlight>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testFlashlight.ToggleFlashlight(false);
        yield return new WaitForSeconds(0.1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testFlashlight.GetCurrentBatteryLevel() == 100f);
        Assert.IsTrue(testFlashlight.GetIsFlashlightDepleting() == false);

        Object.Destroy(testFlashlightGameObject);
    }

    [UnityTest]
    public IEnumerator Unittest_Flashlight_Recharge_Deplete_Recharge()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testFlashlightGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/GameObject/Flashlight/Test Flashlight"));
        Flashlight testFlashlight = testFlashlightGameObject.GetComponent<Flashlight>();

        //<-------------------------------- Test Execution ------------------------------>//
        float rechargeValue = Time.deltaTime * 1.3f;
        testFlashlight.RechargeFlashlight(rechargeValue);
        yield return new WaitForSeconds(10f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testFlashlight.GetCurrentBatteryLevel() == 100f);
        Assert.IsTrue(testFlashlight.GetIsFlashlightDepleted() == false);
        Assert.IsTrue(testFlashlight.GetIsFlashlightRecharged() == true);
        yield return new WaitForSeconds(2f);

        //<-------------------------------- Test Execution ------------------------------>//
        float depleteValue = Time.deltaTime * 50f;
        for (int i=0; i<1100; i++)
        {
            testFlashlight.DepleteFlashlight(depleteValue);
        }
        yield return new WaitForSeconds(2f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testFlashlight.GetCurrentBatteryLevel() == 0f);
        Assert.IsTrue(testFlashlight.GetIsFlashlightDepleted() == true);

        //<-------------------------------- Test Execution ------------------------------>//
        rechargeValue = Time.deltaTime * 50f;
        for (int i=0; i<1100; i++)
        {
            testFlashlight.RechargeFlashlight(rechargeValue);
        }
        yield return new WaitForSeconds(2f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testFlashlight.GetCurrentBatteryLevel() == 100f);
        Assert.IsTrue(testFlashlight.GetIsFlashlightDepleted() == false);
        Assert.IsTrue(testFlashlight.GetIsFlashlightRecharged() == true);

        Object.Destroy(testFlashlightGameObject);
    }
}
