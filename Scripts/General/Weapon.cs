using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Properties")]
    public WeaponStats weaponStats;
    private int currentAmmo;

    [Header("Projectile Prefab")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPos;
    [SerializeField] private GameObject dummyProjectileToDisable;

    [Header("Crosshair")]
    [SerializeField] private Image crosshairImage;
    
    [Header("Weapon Icon on HUD")]
    [SerializeField] private Image weaponIconImage;

    [Header("Fire Rate")]
    private float timeLastFired;

    [Header("Muzzle Flash")]
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Impact Effect")]
    [SerializeField] private string impactEffectTag;

    [Header("SFX")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string baseballBatImpactEffectClipName;
    [SerializeField] private string weaponFireClipName;
    [SerializeField] private string weaponReloadClipName;
    [SerializeField] private string weaponClipOutClipName;

    [Header("Animations")]
    [SerializeField] public Animator weaponAnimator;
    [SerializeField] private Animator armAnimator;

    [Header("Events")]
    [SerializeField] private IntEvent onCurrentAmmoChangeEvent;
    [SerializeField] private IntEvent onCurrentAmmoZeroEvent;
    [SerializeField] private IntEvent onMaxBulletsChangeEvent;

    private int reloadTimes;
    public int reloadNumber;

    void OnEnable()
    {
        if(!weaponStats.isMeleeWeapon) currentAmmo = weaponStats.weaponCurrentAmmo;
        if(crosshairImage != null) crosshairImage.sprite = weaponStats.weaponCrosshairSprite;
        if(weaponIconImage != null) weaponIconImage.sprite = weaponStats.weaponIconSprite;
    }

    public void FireOnce(Transform cameraTransform, LayerMask interactableLayers)
    {
        if(weaponStats.isMeleeWeapon)       // baseball bat (for now)
        {
            weaponAnimator.SetTrigger("fire");
            armAnimator.SetTrigger("fire");

            StartCoroutine(BaseballBatHit(cameraTransform, interactableLayers));
        }
        else
        {
            if(weaponStats.weaponCurrentAmmo > 0)
            {
                if(projectilePrefab == null)       // weapons that use raycast 
                {
                    RaycastHit hit;
                
                    bool isHitDetected = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, interactableLayers);

                    weaponAnimator.SetTrigger("fire");
                    armAnimator.SetTrigger("fire");

                    if(muzzleFlash != null) muzzleFlash.Play();     // Play muzzle flash of weapon - VFX

                    if(isHitDetected)
                    {
                        // Debug.Log("Hit: " + hit.collider.gameObject.name);

                        CameraShaker.instance.ShakeCamera(weaponStats.cameraShakeIntensity, weaponStats.cameraShakeDuration);    // Camera shake

                        if(hit.collider.GetComponent<IDamageable>() != null)    // Check if the hit object is a damageable entity
                        {
                            // float normalizedDistance = hit.distance / weaponStats.range;

                            // if(normalizedDistance <= 1)
                            // {
                            //     hit.collider.GetComponent<IDamageable>().TakeDamage(Mathf.RoundToInt(Mathf.Lerp(weaponStats.weaponMaxDamage, weaponStats.weaponMinDamage, normalizedDistance)), hit.point, hit.normal, weaponStats.isMeleeWeapon);
                            // }
                            hit.collider.GetComponent<IDamageable>().TakeDamage(Mathf.RoundToInt(Mathf.Lerp(weaponStats.weaponMaxDamage, weaponStats.weaponMinDamage, hit.distance)), hit.point, hit.normal, weaponStats.isMeleeWeapon);
                        }

                        if(hit.collider.GetComponent<IBulletDecoratable>() != null)
                        {
                            hit.collider.GetComponent<IBulletDecoratable>().SpawnBulletHoles(hit, weaponStats);
                        }

                        if(hit.collider.GetComponent<IBreakable>() != null)
                        {                            
                            hit.collider.GetComponent<IBreakable>().BreakObject();
                        }

                        // if(hit.collider.GetComponent<Rigidbody>() != null)      // Check if the object can be moved
                        // {
                        //     hit.collider.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 2000 * weaponStats.forceRatio, hit.point);
                        // }

                        // Hit impact
                        /// 
                        /// The tag takes the form of the hit collider's name + "ImpactEffectTag" and searches for this same exact name from the ObjectPooler
                        /// CrateImpactEffectTag
                        ///
                        if(hit.collider.gameObject.layer != 7)
                        {
                            ObjectPooler.instance.SpawnFromPool(hit.collider.gameObject.tag, hit.point, Quaternion.LookRotation(hit.normal));
                        }
                    }
                }
                else        // weapons that use projectile
                {
                    if(Time.time - timeLastFired > 1 / weaponStats.fireRate)
                    {
                        timeLastFired = Time.time;

                        if (projectilePrefab != null)       // Shoot projectile in animation event
                        {
                            weaponAnimator.SetTrigger("fire");
                            armAnimator.SetTrigger("fire");

                            if(muzzleFlash != null) muzzleFlash.Play();     // Play muzzle flash of weapon - VFX

                            CameraShaker.instance.ShakeCamera(weaponStats.cameraShakeIntensity, weaponStats.cameraShakeDuration);    // Camera shake
                        }

                        if(dummyProjectileToDisable != null)
                        {
                            dummyProjectileToDisable.SetActive(false);

                            Invoke("ReEnableDisabledProjectile", 1f);
                        }
                    }
                    else if(timeLastFired > Time.time)
                    {
                        timeLastFired = Time.time;

                        if(muzzleFlash != null) muzzleFlash.Stop();
                    }
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(weaponClipOutClipName)) audioManager.PlayAudio(weaponClipOutClipName);
            }
        }
    }

    public void FireContinuous(Transform cameraTransform, LayerMask interactableLayers)
    {
        if(weaponStats.weaponCurrentAmmo > 0)
        {
            if(Time.time - timeLastFired > 1 / weaponStats.fireRate)
            {
                timeLastFired = Time.time;

                weaponAnimator.SetTrigger("fire");
                armAnimator.SetTrigger("fire");

                RaycastHit hit;
            
                bool isHitDetected = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, interactableLayers);

                if(muzzleFlash != null) muzzleFlash.Play();     // Play muzzle flash of weapon - VFX

                if(isHitDetected)
                {
                    // Debug.Log("Hit: " + hit.collider.gameObject.name);
                    
                    CameraShaker.instance.ShakeCamera(weaponStats.cameraShakeIntensity, weaponStats.cameraShakeDuration);    // Camera shake

                    if(hit.collider.GetComponent<IDamageable>() != null)    // Check if the hit object is a damageable entity
                    {
                        float normalizedDistance = hit.distance / weaponStats.range;
                        
                        if(normalizedDistance <= 1)
                        {
                            hit.collider.GetComponent<IDamageable>().TakeDamage(Mathf.RoundToInt(Mathf.Lerp(weaponStats.weaponMaxDamage, weaponStats.weaponMinDamage, normalizedDistance)), hit.point, hit.normal);
                        }
                    }

                    if(hit.collider.GetComponent<IBulletDecoratable>() != null)
                    {
                        // Debug.Log("Hit wall");
                        hit.collider.GetComponent<IBulletDecoratable>().SpawnBulletHoles(hit, weaponStats);
                    }

                    if(hit.collider.GetComponent<IBreakable>() != null)
                    {                        
                        hit.collider.GetComponent<IBreakable>().BreakObject();
                    }

                    if(hit.collider.GetComponent<Rigidbody>() != null)      // Check if the object can be moved
                    {
                        hit.collider.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 2000 * weaponStats.forceRatio, hit.point);
                    }

                    // Hit impact
                    /// 
                    /// The tag takes the form of the hit collider's name + "ImpactEffectTag" and searches for this same exact name from the ObjectPooler
                    /// CrateImpactEffectTag
                    ///
                    if(hit.collider.gameObject.layer != 7)
                    {
                        ObjectPooler.instance.SpawnFromPool(hit.collider.gameObject.tag, hit.point, Quaternion.LookRotation(hit.normal));
                    }
                }
            }
            else if(timeLastFired > Time.time)
            {
                timeLastFired = Time.time;

                if(muzzleFlash != null) muzzleFlash.Stop();
            }
        }
    }

    public void DisableWeapon()
    {
        // Debug.Log("Disabling weapon game object");
        gameObject.SetActive(false);
    }

    public void IncrementReloadNumber()
    {
        reloadNumber += 1;
    }

    public void CheckReloadNumber()
    {
        if(reloadNumber >= reloadTimes)
        {
            armAnimator.SetBool("hasCompletedReload", true);
            reloadNumber = 0;
        }
        else
        {
            armAnimator.SetBool("hasCompletedReload", false);
        }
    }

    public void ReloadWeapon()
    {
        // Reload animation
        // For shotgun only
        if(weaponStats.weaponMaxBullets > 0)
        {
            if(transform.gameObject.name == "Shotgun_Arms")
            {
                armAnimator.SetTrigger("reload");
                weaponAnimator.SetTrigger("reload");   
            }
            else
            {
                armAnimator.SetTrigger("reload");
                weaponAnimator.SetTrigger("reload");
            }
        }

        // Reload calculation
        // If the remaining bullets is more than enough to swap the entire magazine
        if(weaponStats.weaponMaxBullets > 0 && weaponStats.weaponMaxBullets >= (weaponStats.weaponMaxAmmo-weaponStats.weaponCurrentAmmo))
        {
            reloadTimes = weaponStats.weaponMaxAmmo-weaponStats.weaponCurrentAmmo;
            weaponStats.weaponCurrentAmmo += reloadTimes;
            currentAmmo = weaponStats.weaponCurrentAmmo;
            weaponStats.weaponMaxBullets -= reloadTimes;
            
            if(onCurrentAmmoChangeEvent != null) onCurrentAmmoChangeEvent.Raise(weaponStats.weaponCurrentAmmo);
            if(onMaxBulletsChangeEvent != null) onMaxBulletsChangeEvent.Raise(weaponStats.weaponMaxBullets);
        }
        // If the remaining bullets are less than the magazine ammo
        else if(weaponStats.weaponMaxBullets > 0 && weaponStats.weaponMaxBullets < (weaponStats.weaponMaxAmmo-weaponStats.weaponCurrentAmmo))
        {
            reloadTimes = weaponStats.weaponMaxBullets;
            weaponStats.weaponCurrentAmmo += reloadTimes;
            currentAmmo = weaponStats.weaponCurrentAmmo;
            weaponStats.weaponMaxBullets -= reloadTimes;

            if(onCurrentAmmoChangeEvent != null) onCurrentAmmoChangeEvent.Raise(weaponStats.weaponCurrentAmmo);
            if(onMaxBulletsChangeEvent != null) onMaxBulletsChangeEvent.Raise(weaponStats.weaponMaxBullets);
        }
        // If there are no remaining bullets
        else if(weaponStats.weaponMaxBullets <= 0)
        {
            // Debug.Log("Unable to reload. Insufficient bullets");
        }
    }

    private void DecrementAmmo(int decrement)
    {       
        currentAmmo -= decrement;

        weaponStats.weaponCurrentAmmo = currentAmmo;

        if(onCurrentAmmoChangeEvent != null) onCurrentAmmoChangeEvent.Raise(weaponStats.weaponCurrentAmmo);

        CheckIfAmmoOut();
    }

    private void SpawnProjectile()
    {
        Quaternion projectileRotation = Camera.main.transform.rotation;
        GameObject newProjectile = Instantiate(projectilePrefab, Camera.main.transform.position + Camera.main.transform.forward * 2, projectileRotation);
    }

    private void PlayWeaponAudio(string weaponAudioName)
    {
        if(!string.IsNullOrEmpty(weaponAudioName)) audioManager.PlayAudio(weaponAudioName);
    }

    private void ReEnableDisabledProjectile()
    {
        audioManager.PlayAudio(weaponReloadClipName);

        dummyProjectileToDisable.SetActive(true);
    }

    private void CheckIfAmmoOut()
    {
        if((currentAmmo > 0 || weaponStats.weaponCurrentAmmo > 0) || weaponStats.weaponMaxBullets > 0) return;

        currentAmmo = 0;

        weaponStats.weaponCurrentAmmo = currentAmmo;

        if(onCurrentAmmoChangeEvent != null) onCurrentAmmoChangeEvent.Raise(weaponStats.weaponCurrentAmmo);

        if(onCurrentAmmoZeroEvent != null) onCurrentAmmoZeroEvent.Raise(weaponStats.weaponCurrentAmmo);

        if(onMaxBulletsChangeEvent != null) onMaxBulletsChangeEvent.Raise(weaponStats.weaponMaxBullets);
    }

    private IEnumerator BaseballBatHit(Transform cameraTransform, LayerMask interactableLayers)
    {
        yield return new WaitForSeconds(0.1f);

        RaycastHit hit;
        bool isRaycastHitDetected = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, weaponStats.range, interactableLayers);
        
        if(isRaycastHitDetected)
        {
            if(muzzleFlash != null) muzzleFlash.Play();     // Play muzzle flash of weapon - VFX

            // Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            CameraShaker.instance.ShakeCamera(weaponStats.cameraShakeIntensity, weaponStats.cameraShakeDuration);    // Camera shake

            if(hit.collider.GetComponent<IDamageable>() != null)    // Check if the hit object is a damageable entity
            {
                float normalizedDistance = hit.distance / weaponStats.range;

                if(normalizedDistance <= 1)
                {
                    hit.collider.GetComponent<IDamageable>().TakeDamage(Mathf.RoundToInt(Mathf.Lerp(weaponStats.weaponMaxDamage, weaponStats.weaponMinDamage, normalizedDistance)), hit.point, hit.normal, weaponStats.isMeleeWeapon);
                }
            }

            if(hit.collider.GetComponent<IBreakable>() != null)     // Check if the object can break or spawn pieces
            {                    
                hit.collider.GetComponent<IBreakable>().BreakObject();
            }

            if(hit.collider.gameObject.layer == 12 && hit.collider.GetComponent<Rigidbody>() != null)      // Check if the object can be moved
            {
                hit.collider.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 2000 * weaponStats.forceRatio, hit.point);
            }

            // Hit impact
            /// 
            /// The tag takes the form of the hit collider's name + "ImpactEffectTag" and searches for this same exact name from the ObjectPooler
            /// CrateImpactEffectTag
            ///
            //if(!string.IsNullOrEmpty(impactEffectTag)) ObjectPooler.instance.SpawnFromPool(impactEffectTag, hit.point, Quaternion.LookRotation(hit.normal));
            if(hit.collider.gameObject.layer != 7)
            {
                ObjectPooler.instance.SpawnFromPool(hit.collider.gameObject.tag, hit.point, Quaternion.LookRotation(hit.normal));
            }

            // Play BaseballBatImpact audio
            // TODO: Can add multiple sound effects based on game object tags
            //       like metal, wood, plastic, wool etc.
            audioManager.PlayAudio(baseballBatImpactEffectClipName);
        }
    }
}
