using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    #region Serialized Fields
    [Header("Scriptable Object")]
    [SerializeField] private WeaponScriptableObject weaponSO;

    [Header("Melee weapon")]
    [SerializeField] private float meleeHitDelay;

    [Header("Projectile Weapon")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject dummyProjectilePrefab;

    [Header("Particle system")]
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Attackable layers")]
    [SerializeField] private LayerMask attackableLayers;

    [Header("Image")]
    [SerializeField] private Image crosshairImage;
    [SerializeField] private Image weaponIconImage;

    [Header("Audio")]
    [SerializeField] private string impactEffectClipName;
    [SerializeField] private string fireClipName;
    [SerializeField] private string reloadClipName;
    [SerializeField] private string clipOutClipName;

    [Header("Events")]
    [SerializeField] private VoidEvent onWeaponEnableEvent;
    [SerializeField] private VoidEvent onWeaponDisableEvent;

    [SerializeField] private VoidEvent onMeleeWeaponAttackEvent;

    [SerializeField] private VoidEvent onRaycastWeaponFireEvent;
    [SerializeField] private VoidEvent onRaycastHitDetectEvent;
    [SerializeField] private VoidEvent onRaycastWeaponReloadEvent;

    [SerializeField] private VoidEvent onProjectileWeaponFireEvent;
    [SerializeField] private VoidEvent onProjectileWeaponReloadEvent;

    [SerializeField] private IntEvent onCurrentAmmoChangeEvent;
    [SerializeField] private IntEvent onCurrentAmmoZeroEvent;
    [SerializeField] private IntEvent onAmmoRoundChangeEvent;
    #endregion

    #region Private Fields
    private float timeLastFired;
    private int currentAmmoRound;
    private int maxBullets;
    private float minDamage;
    private float maxDamage;
    private float attackRange;
    #endregion

    void Start()
    {
        InitWeapon();    
    }

    void OnEnable()
    {
        onCurrentAmmoChangeEvent.Raise(weaponSO.currentAmmoRound);
        onCurrentAmmoZeroEvent.Raise(weaponSO.currentAmmoRound);
        onAmmoRoundChangeEvent.Raise(weaponSO.maxBullets);
        onWeaponEnableEvent.Raise();
    }

    void OnDisable()
    {
        onWeaponDisableEvent.Raise();    
    }

    #region Public Methods
    /// <summary>
    ///     Provides a function for animator to use and disable the weapon game object
    /// </summary>
    public void DisableWeapon()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    ///     Get the scriptable object for this specific weapon
    /// </summary>
    /// <returns></returns>
    public WeaponScriptableObject GetWeaponScriptableObject()
    {
        return weaponSO;
    }

    /// <summary>
    ///     Handles one-time firing of the weapon based on the weapon type : MELEE, RAYCAST, PROJECTILE
    ///     Melee weapon needs a Coroutine due to a delay when the impact is received, be it a knife or baseball
    /// </summary>
    public void FireOnce()
    {
        switch (weaponSO.weaponType)
        {
            case WeaponType.MELEE:

                StartCoroutine(HandleMeleeHit(meleeHitDelay));

                break;

            case WeaponType.RANGE_RAYCAST:
                
                HandleRaycastHit();

                break;

            case WeaponType.RANGE_PROJECTILE:
                
                HandleProjectileHit();
                
                break;

            default:
                break;
        }
    }

    /// <summary>
    ///     Handles the continuous firing of the weapon based on the fire rate
    /// </summary>
    public void FireContinuous()
    {
        if (Time.time - timeLastFired > 1 / weaponSO.fireRate)
        {
            timeLastFired = Time.time;

            HandleRaycastHit();
        }
        else if (timeLastFired > Time.time)
        {
            timeLastFired = Time.time;

            muzzleFlash.Stop();
        }
    }

    public void ReloadWeapon()
    {
        switch (weaponSO.weaponType)
        {
            case WeaponType.RANGE_PROJECTILE:

                onProjectileWeaponReloadEvent.Raise();

                HandleWeaponReload();

                break;

            case WeaponType.RANGE_RAYCAST:

                onRaycastWeaponReloadEvent.Raise();

                HandleWeaponReload();

                break;

            default:
                break;
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Initialize the weapon properties like
    ///     - currentAmmoRound
    ///     - maxBullets
    ///     - weaponIconImage
    ///     - attackRange
    ///     - minDamage
    ///     - maxDamage
    /// </summary>
    private void InitWeapon()
    {
        currentAmmoRound = weaponSO.currentAmmoRound;
        maxBullets = weaponSO.maxBullets;
        crosshairImage.sprite = weaponSO.crosshairSprite;
        weaponIconImage.sprite = weaponSO.iconSprite;
        attackRange = weaponSO.attackRange;
        minDamage = weaponSO.minDamage;
        maxDamage = weaponSO.maxDamage;
    }
    
    /// <summary>
    ///     Handles the melee hit with a delay
    ///     Raise an event when hit is detected
    ///     Use this event to
    ///     - spawn particles from object pooler for impact effect
    ///     - spawn bullet holes / impact texture on surfaces
    ///     - break objects
    /// </summary>
    /// <param name="arg_delay"></param>
    /// <returns></returns>
    private IEnumerator HandleMeleeHit(float arg_delay)
    {
        yield return new WaitForSeconds(arg_delay);

        onMeleeWeaponAttackEvent.Raise();

        RaycastHit hit;
        bool isHitDetected = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, attackableLayers);

        if (isHitDetected)
        {
            // Use this event to 
            // spawn particles from object pooler for impact effect
            // spawn bullet holes
            // break objects
            onRaycastHitDetectEvent.Raise();

            // Take damage from enemy
        }

        // Plays muzzle flash for melee weapon
        muzzleFlash.Play();

        // Shakes camera
        CameraShaker.instance.ShakeCamera(weaponSO.cameraShakeIntensity, weaponSO.cameraShakeDuration);
    }

    /// <summary>
    ///     Handles the raycast hit effect
    ///     Only valid if current ammo round is not empty
    ///     Returns if out of ammo
    /// </summary>
    private void HandleRaycastHit()
    {
        onRaycastWeaponFireEvent.Raise();

        if (currentAmmoRound > 0)
        {
            RaycastHit hit;
            bool isHitDetected = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, attackableLayers);

            if (isHitDetected)
            {
                // Use this event to 
                // spawn particles from object pooler for impact effect
                // spawn bullet holes
                // break objects
                onRaycastHitDetectEvent.Raise();

                // Take damage from enemy
            }
        }
        else
        {
            // Return if out of ammo
            return;
        }

        // Plays muzzle flash
        muzzleFlash.Play();

        // Shakes camera
        CameraShaker.instance.ShakeCamera(weaponSO.cameraShakeIntensity, weaponSO.cameraShakeDuration);
    }

    /// <summary>
    ///     Handles projectile hit if the weapon is of projectile type
    ///     Checks for fire rate before firing / spawning projectile
    ///     Uses a dummy projectile - check comment on how it is used
    /// </summary>
    private void HandleProjectileHit()
    {
        if (Time.time - timeLastFired > 1 / weaponSO.fireRate)
        {
            timeLastFired = Time.time;

            if (projectilePrefab != null)
            {
                // Use this event to
                // trigger animation in weapon and arm
                onProjectileWeaponFireEvent.Raise();

                // Plays muzzle flash
                muzzleFlash.Play();

                // Shakes camera
                CameraShaker.instance.ShakeCamera(weaponSO.cameraShakeIntensity, weaponSO.cameraShakeDuration);
            }

            // When projectile is requested to fire
            // the dummy projectile is first disabled,
            // then a real projectile is spawned and forced towards Camera.main.transform.forward
            // When this real projectile is fired out, after 1s, re-enable the dummy projectile
            StartCoroutine(HandleProjectileReappearance());
        }
        else if (timeLastFired > Time.time)
        {
            timeLastFired = Time.time;

            muzzleFlash.Stop();
        }
    }

    /// <summary>
    ///     Handle the projectile reappearanc after firing
    ///     When projectile is requested to fire
    ///     The dummy projectile is first disabled
    ///     Then a real projectile is spawned and forced towards Camera.main.transform.forward
    ///     When this real projectile is fired out, after 1s (or some delay), re-enable this dummy projectile (reload)
    /// </summary>
    /// <returns></returns>
    private IEnumerator HandleProjectileReappearance()
    {
        dummyProjectilePrefab.SetActive(false);

        yield return new WaitForSeconds(1f);

        onProjectileWeaponReloadEvent.Raise();
        dummyProjectilePrefab.SetActive(true);
    }

    private void HandleWeaponReload()
    {
        // Reload calculation
        // If the remaining bullets is more than enough to swap the entire magazine
        int remainingAmmo = weaponSO.maxAmmoRound - weaponSO.currentAmmoRound;
        if (weaponSO.maxBullets > 0 && weaponSO.maxBullets >= remainingAmmo)
        {
            weaponSO.currentAmmoRound += remainingAmmo;
            currentAmmoRound = weaponSO.currentAmmoRound;
            weaponSO.maxBullets -= remainingAmmo;

            onCurrentAmmoChangeEvent.Raise(weaponSO.currentAmmoRound);
            onAmmoRoundChangeEvent.Raise(weaponSO.maxBullets);
        }
        // If the remaining bullets are less than the magazine ammo
        else if (weaponSO.maxBullets > 0 && weaponSO.maxBullets < remainingAmmo)
        {
            int reloadTimes;
            reloadTimes = weaponSO.maxBullets;
            weaponSO.currentAmmoRound += reloadTimes;
            currentAmmoRound = weaponSO.currentAmmoRound;
            weaponSO.maxBullets -= reloadTimes;

            onCurrentAmmoChangeEvent.Raise(weaponSO.currentAmmoRound);
            onAmmoRoundChangeEvent.Raise(weaponSO.maxBullets);
        }
        // If there are no remaining bullets
        else if (weaponSO.maxBullets <= 0)
        {
            // Debug.Log("Unable to reload. Insufficient bullets");
        }
    }

    /// <summary>
    ///     Decrements the ammo of the weapon 
    ///     Used in the animation tab 
    /// </summary>
    /// <param name="decrement"></param>
    private void DecrementAmmo(int decrement)
    {
        currentAmmoRound -= decrement;
        weaponSO.currentAmmoRound = currentAmmoRound;
        onCurrentAmmoChangeEvent.Raise(weaponSO.currentAmmoRound);

        CheckIfAmmoOut();
    }

    /// <summary>
    ///     Checks if the current ammo is empty
    ///     If not empty, do nothing and return
    ///     Otherwise, raise events
    /// </summary>
    private void CheckIfAmmoOut()
    {
        if (currentAmmoRound > 0 || weaponSO.maxBullets > 0)
        {
            return;
        }

        currentAmmoRound = 0;
        weaponSO.currentAmmoRound = currentAmmoRound;

        onCurrentAmmoChangeEvent.Raise(weaponSO.currentAmmoRound);
        onCurrentAmmoZeroEvent.Raise(weaponSO.currentAmmoRound);
        onAmmoRoundChangeEvent.Raise(weaponSO.maxBullets);
    }
    #endregion
}
