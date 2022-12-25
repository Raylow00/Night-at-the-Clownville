using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour, IPurchasable
{
    [System.Serializable]
    public class Weapons
    {
        public GameObject playerWeapon;
        public GameObject[] playerWeaponVariants;
    }

    [Header("Camera")]
    private Transform cameraTransform;

    [Header("Interactable Layers")]
    [SerializeField] private LayerMask interactableLayers;

    [Header("Empty Hands")]
    [SerializeField] private GameObject emptyHandsGO;

    [Header("Show Coin Hand")]
    [SerializeField] private GameObject showCoinHandGO;

    [Header("Weapons")]
    [SerializeField] private Weapons[] weapons;
    private GameObject currentWeaponGO;
    [HideInInspector] public GameObject CurrentWeaponGO { get { return currentWeaponGO; } }
    private Weapon currentPlayerWeapon;
    private int currentPlayerWeaponNumber;
    [HideInInspector] public int CurrentPlayerWeaponNumber => currentPlayerWeaponNumber;
    private int lastUsedPlayerWeaponNumber;

    [Header("Weapon Variants")]
    private int currentWeaponVariantNumber;
    [HideInInspector] public int CurrentWeaponVariantNumber => currentWeaponVariantNumber;

    [Header("Camera Handler")]
    [SerializeField] private PlayerCameraHandler cameraHandler;

    [Header("Events")]
    [SerializeField] private IntEvent onAmmoChangeEvent;
    [SerializeField] private IntEvent onMaxBulletsChangeEvent;
    [SerializeField] private VoidEvent onBaseballBatActivatedEvent;
    [SerializeField] private VoidEvent onBaseballBatDeactivatedEvent;
    [SerializeField] private VoidEvent onEnemyDetectedEvent;
    [SerializeField] private VoidEvent onEnemyExitDetectorEvent;
    [SerializeField] private StringEvent onWeaponSkinNotPurchasableEvent;

    void Awake()
    {
        cameraTransform = Camera.main.transform;

        weapons[0].playerWeapon.SetActive(true);
        currentWeaponGO = weapons[0].playerWeapon;
        currentPlayerWeapon = weapons[0].playerWeapon.GetComponent<Weapon>();
        currentPlayerWeaponNumber = 0;
        currentWeaponVariantNumber = 0;

        emptyHandsGO.SetActive(false);
        showCoinHandGO.SetActive(false);
    }

    void Start()
    {
        if(onAmmoChangeEvent != null) onAmmoChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponCurrentAmmo);
        if(onMaxBulletsChangeEvent != null) onMaxBulletsChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponMaxBullets);
        if(onBaseballBatActivatedEvent != null) onBaseballBatActivatedEvent.Raise();
    }

    void Update()
    {
        RaycastHit hit;
                
        bool isHitDetected = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactableLayers);

        if(isHitDetected)
        {
            // Enemy layer
            if(hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                onEnemyDetectedEvent.Raise();
            }
            else
            {
                onEnemyExitDetectorEvent.Raise();
            }
        }
        else
        {
            onEnemyExitDetectorEvent.Raise();
        }
    }

    public void Attack(string fireType)
    {
        if(fireType == "manual")
        {
            currentPlayerWeapon.FireOnce(cameraTransform, interactableLayers);
        }
        else if(fireType == "automatic")
        {
            currentPlayerWeapon.FireContinuous(cameraTransform, interactableLayers);
        }
    }

    public bool CheckIfPurchasable(string id)
    {
        bool purchasable = false;

        foreach(Weapons weapon in weapons)
        {
            if(weapon.playerWeapon.GetComponent<Weapon>().weaponStats.weaponID == id)
            {
                if(weapon.playerWeapon.GetComponent<Weapon>().weaponStats.unlocked)
                {
                    purchasable = true;
                }
            }

            foreach(GameObject weaponVariant in weapon.playerWeaponVariants)
            {
                if(weaponVariant.GetComponent<Weapon>().weaponStats.weaponID == id)
                {
                    if(weaponVariant.GetComponent<Weapon>().weaponStats.unlocked)
                    {
                        purchasable = true;
                    }
                    else
                    {
                        onWeaponSkinNotPurchasableEvent.Raise("The base weapon is not unlocked yet!");
                    }
                }
            }
        }
        
        return purchasable;
    }

    public void LockItem(string id)
    {
        foreach(Weapons weapon in weapons)
        {
            if(weapon.playerWeapon.GetComponent<Weapon>().weaponStats.weaponID == id)
            {
                weapon.playerWeapon.GetComponent<Weapon>().weaponStats.unlocked = false;
            }
        }
    }

    public void UnlockItem(string id)
    {
        foreach(Weapons weapon in weapons)
        {
            if(weapon.playerWeapon.GetComponent<Weapon>().weaponStats.weaponID == id)
            {
                weapon.playerWeapon.GetComponent<Weapon>().weaponStats.unlocked = true;
            }
            
            foreach(GameObject weaponVariant in weapon.playerWeaponVariants)
            {
                if(weaponVariant.GetComponent<Weapon>().weaponStats.weaponID == id)
                {
                    weaponVariant.GetComponent<Weapon>().weaponStats.unlocked = true;
                }
            }
        }
    }

    public void ToggleWeapon(int weaponNumber)
    {
        if(!cameraHandler.isCameraEquipped)
        {
            if(weaponNumber <= weapons.Length)
            {
                if(weapons[weaponNumber].playerWeapon.GetComponent<Weapon>().weaponStats.unlocked)
                {
                    lastUsedPlayerWeaponNumber = currentPlayerWeaponNumber;
                    currentWeaponGO.SetActive(false);

                    currentWeaponGO = weapons[weaponNumber].playerWeapon;
                    currentPlayerWeapon = weapons[weaponNumber].playerWeapon.GetComponent<Weapon>();
                    currentWeaponGO.SetActive(true);
                    currentPlayerWeaponNumber = weaponNumber;

                    if(weaponNumber != 0)
                    {
                        if(onBaseballBatDeactivatedEvent != null) onBaseballBatDeactivatedEvent.Raise();
                        if(onAmmoChangeEvent != null) onAmmoChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponCurrentAmmo);
                        if(onMaxBulletsChangeEvent != null) onMaxBulletsChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponMaxBullets);
                    }
                    else
                    {
                        if(onBaseballBatActivatedEvent != null) onBaseballBatActivatedEvent.Raise();
                    }
                }
                else
                {
                    return;
                } 
            }
        }
    }

    public void SwitchLastUsedWeapon()
    {
        int temp;
        temp = currentPlayerWeaponNumber;
        currentWeaponGO.SetActive(false);

        currentWeaponGO = weapons[lastUsedPlayerWeaponNumber].playerWeapon;
        currentWeaponGO.SetActive(true);
        currentPlayerWeaponNumber = lastUsedPlayerWeaponNumber;
        currentPlayerWeapon = weapons[lastUsedPlayerWeaponNumber].playerWeapon.GetComponent<Weapon>();

        lastUsedPlayerWeaponNumber = temp;

        if(onAmmoChangeEvent != null) onAmmoChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponCurrentAmmo);
        if(onMaxBulletsChangeEvent != null) onMaxBulletsChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponMaxBullets);
    }

    public void EquipSkin()
    {
        // Switch back to base weapon variant
        if(currentWeaponVariantNumber >= weapons[currentPlayerWeaponNumber].playerWeaponVariants.Length)
        {
            currentWeaponGO.SetActive(false);

            currentWeaponVariantNumber = 0;

            currentWeaponGO = weapons[currentPlayerWeaponNumber].playerWeapon;
            currentPlayerWeapon = currentWeaponGO.GetComponent<Weapon>();
            currentWeaponGO.SetActive(true);

            if(onAmmoChangeEvent != null) onAmmoChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponCurrentAmmo);
            if(onMaxBulletsChangeEvent != null) onMaxBulletsChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponMaxBullets);
        }
        // Only if the variant is unlocked
        else if(currentWeaponVariantNumber < weapons[currentPlayerWeaponNumber].playerWeaponVariants.Length)
        {
            if(weapons[currentPlayerWeaponNumber].playerWeaponVariants[currentWeaponVariantNumber].GetComponent<Weapon>().weaponStats.unlocked)
            {
                currentWeaponGO.SetActive(false);
                
                currentWeaponGO = weapons[currentPlayerWeaponNumber].playerWeaponVariants[currentWeaponVariantNumber];
                currentPlayerWeapon = currentWeaponGO.GetComponent<Weapon>();
                currentWeaponGO.SetActive(true);

                if(onAmmoChangeEvent != null) onAmmoChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponCurrentAmmo);
                if(onMaxBulletsChangeEvent != null) onMaxBulletsChangeEvent.Raise(currentPlayerWeapon.weaponStats.weaponMaxBullets);

                currentWeaponVariantNumber++;
            }
            else
            {
                currentWeaponVariantNumber++;
                EquipSkin();
            }
        }
    }

    public void UnloadWeapon()
    {
        // Debug.Log("Disabling current weapon");
        currentWeaponGO.GetComponent<Animator>().SetTrigger("unequip");
    }

    public Weapon GetActiveWeapon()
    {
        return currentPlayerWeapon;
    }

    public void InspectWeapon()
    {
        currentWeaponGO.GetComponent<Animator>().SetTrigger("inspect");
    }

    public void ReloadWeapon()
    {
        currentWeaponGO.GetComponent<Weapon>().ReloadWeapon();
    }
}
