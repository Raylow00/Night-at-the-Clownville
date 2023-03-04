using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapons
{
    public GameObject weaponGO;
}

public class WeaponManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Weapons[] availableWeapons;
    #endregion

    #region Private Fields
    private Weapon currentWeapon;
    private GameObject currentWeaponGO;
    private int lastUsedWeaponIndex;
    private int currentWeaponIndex;
    #endregion

    void Start()
    {
        InitWeapon();    
    }

    #region Public Methods
    public void FireOnce()
    {
        currentWeapon.FireOnce();
    }

    public void FireContinuous()
    {
        currentWeapon.FireContinuous();
    }

    public void ToggleWeapon(int arg_weaponIdx)
    {
        // If within the number of available weapons and
        // weapon is unlocked
        if (arg_weaponIdx <= availableWeapons.Length  && 
            availableWeapons[arg_weaponIdx].weaponGO.GetComponent<Weapon>().GetWeaponScriptableObject().isUnlocked)
        {
            // Save current weapon index to last used 
            // to be able to switch back easily
            lastUsedWeaponIndex = currentWeaponIndex;
            currentWeaponIndex = arg_weaponIdx;

            // Assign new weapon
            currentWeaponGO.SetActive(false);
            currentWeaponGO = availableWeapons[arg_weaponIdx].weaponGO;
            currentWeapon = currentWeaponGO.GetComponent<Weapon>();
            currentWeaponGO.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void SwitchLastUsedWeapon()
    {
        int temp;
        // Save the current weapon index for switching later
        temp = currentWeaponIndex;
        // Disable the current weapon
        currentWeaponGO.SetActive(false);
        // Switch to last used weapon
        currentWeaponGO = availableWeapons[lastUsedWeaponIndex].weaponGO;
        // Re-enable current weapon
        currentWeaponGO.SetActive(true);
        // Re-assign current weapon component
        currentWeapon = currentWeaponGO.GetComponent<Weapon>();
        // Previous current weapon now becomes last used weapon
        lastUsedWeaponIndex = temp;
    }

    public void InspectWeapon()
    {
        if (currentWeaponGO.GetComponent<Animator>() != null)
        {
            currentWeaponGO.GetComponent<Animator>().SetTrigger("inspect");
        }
        else
        {
            return;
        }
    }

    public void ReloadWeapon()
    {
        currentWeaponGO.GetComponent<Weapon>().ReloadWeapon();
    }
    #endregion

    #region Private Methods
    private void InitWeapon()
    {
        currentWeaponGO = availableWeapons[0].weaponGO;
        currentWeapon = availableWeapons[0].weaponGO.GetComponent<Weapon>();
    }
    #endregion
}
