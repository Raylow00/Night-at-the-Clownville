using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerWeaponHandlerTest
{
    // This test only test weapons that are pre-activated
    // Weapons like Shotgun, Rifle and Rocket Launcher are not tested as they are not activated
    //
    // TODO: To activate the weapons and test the toggle function before deactivating them back
    //
    [UnityTest]
    [TestCase(0, ExpectedResult=null)]
    [TestCase(1, ExpectedResult=null)]
    public IEnumerator PlayerWeaponHandler_ToggleUnlockedWeapon_WeaponActivated(int weaponNumber)
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerWeaponHandler playerWeaponHandler = playerGO.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<PlayerWeaponHandler>();

        playerWeaponHandler.ToggleWeapon(weaponNumber);

        yield return new WaitForSeconds(1f);

        // Debug.Log(playerWeaponHandler.CurrentPlayerWeaponNumber);

        Assert.AreEqual(playerWeaponHandler.CurrentPlayerWeaponNumber, weaponNumber);

        yield return new WaitForSeconds(1f);

        Object.Destroy(playerGO);
    }

    [UnityTest]
    [TestCase(0, 1, ExpectedResult=null)]
    [TestCase(1, 0, ExpectedResult=null)]
    public IEnumerator PlayerWeaponHandler_SwitchLastUsedWeapon_LastWeaponActivated(int current, int target)
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerWeaponHandler playerWeaponHandler = playerGO.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<PlayerWeaponHandler>();

        playerWeaponHandler.ToggleWeapon(current);

        yield return new WaitForSeconds(1f);

        playerWeaponHandler.ToggleWeapon(target);

        yield return new WaitForSeconds(1f);

        playerWeaponHandler.SwitchLastUsedWeapon();

        Assert.AreEqual(playerWeaponHandler.CurrentPlayerWeaponNumber, current);

        yield return new WaitForSeconds(1f);

        Object.Destroy(playerGO);
    }
}
