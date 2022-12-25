using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerCameraHandlerTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerCameraHandler_CameraNotActiveEquipCamera_CameraIsEquipped()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerCameraHandler playerCameraHandler = playerGO.transform.GetChild(2).GetChild(2).GetChild(0).gameObject.GetComponent<PlayerCameraHandler>();

        yield return new WaitForSeconds(2f);

        playerCameraHandler.EquipCamera();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(playerCameraHandler.isCameraEquipped);

        yield return new WaitForSeconds(1f);

        Object.Destroy(playerGO);
    }

    [UnityTest]
    public IEnumerator PlayerCameraHandler_CameraActiveUnequipCamera_CameraIsUnequipped()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerCameraHandler playerCameraHandler = playerGO.transform.GetChild(2).GetChild(2).GetChild(0).gameObject.GetComponent<PlayerCameraHandler>();
        // Debug.Log(playerCameraHandler);

        yield return new WaitForSeconds(2f);

        playerCameraHandler.EquipCamera();

        yield return new WaitForSeconds(1f);

        playerCameraHandler.EquipCamera();

        Assert.IsFalse(playerCameraHandler.isCameraEquipped);

        yield return new WaitForSeconds(1f);

        Object.Destroy(playerGO);

        // Debug.Log("Object destroyed");
    }

    [UnityTest]
    public IEnumerator PlayerCameraHandler_CameraEquippedTakeScreenshot_ScreenshotTaken()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerCameraHandler playerCameraHandler = playerGO.transform.GetChild(2).GetChild(2).GetChild(0).gameObject.GetComponent<PlayerCameraHandler>();

        yield return new WaitForSeconds(2f);

        playerCameraHandler.EquipCamera();

        yield return new WaitForSeconds(1f);

        playerCameraHandler.TakeScreenshot(1920, 1080);

        yield return new WaitForSeconds(2f);

        Assert.IsTrue(playerCameraHandler.isCameraEquipped);
        Assert.IsTrue(playerCameraHandler.isScreenshotTaken);

        yield return new WaitForSeconds(1f);

        Object.Destroy(playerGO);
    }

    [UnityTest]
    public IEnumerator PlayerCameraHandler_CameraUnequippedTakeScreenshot_ScreenshotNotTaken()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerCameraHandler playerCameraHandler = playerGO.transform.GetChild(2).GetChild(2).GetChild(0).gameObject.GetComponent<PlayerCameraHandler>();

        yield return new WaitForSeconds(2f);

        playerCameraHandler.EquipCamera();

        yield return new WaitForSeconds(1f);

        playerCameraHandler.EquipCamera();

        yield return new WaitForSeconds(1f);

        playerCameraHandler.TakeScreenshot(1920, 1080);

        yield return new WaitForSeconds(1f);

        Assert.IsFalse(playerCameraHandler.isCameraEquipped);
        Assert.IsFalse(playerCameraHandler.isScreenshotTaken);

        yield return new WaitForSeconds(1f);

        Object.Destroy(playerGO);
    }
}
