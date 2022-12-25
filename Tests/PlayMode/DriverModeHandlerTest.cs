using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DriverModeHandlerTest : MonoBehaviour
{
    [UnityTest]
    public IEnumerator DriverModeHandler_ShooterModeSwitchToDriverMode_DriverModeEnabled()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        GameObject driverGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Driver"));
        GameObject crosshairCanvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/CrosshairCanvas"));
        DriverModeHandler driverModeHandler = driverGO.GetComponent<DriverModeHandler>();

        yield return new WaitForSeconds(2f);

        driverModeHandler.PressInteractiveButton();

        yield return new WaitForSeconds(2f);

        Assert.IsTrue(driverModeHandler.currentMode == 1);

        yield return new WaitForSeconds(1f);

        Object.Destroy(playerGO);
        Object.Destroy(driverGO);
        Object.Destroy(crosshairCanvas);
    }

    [UnityTest]
    public IEnumerator DriverModeHandler_DriverModeSwitchToShooterMode_ShooterModeEnabled()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        GameObject driverGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Driver"));
        GameObject crosshairCanvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/CrosshairCanvas"));
        DriverModeHandler driverModeHandler = driverGO.GetComponent<DriverModeHandler>();

        yield return new WaitForSeconds(2f);

        driverModeHandler.PressInteractiveButton();

        yield return new WaitForSeconds(5f);

        driverModeHandler.PressInteractiveButton();

        yield return new WaitForSeconds(2f);

        Assert.IsTrue(driverModeHandler.currentMode == 0);

        yield return new WaitForSeconds(1f);

        Object.Destroy(playerGO);
        Object.Destroy(driverGO);
        Object.Destroy(crosshairCanvas);
    }
}
