using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerHealthSystem : MonoBehaviour, IDamageable
{
    // TODO: To redesign the healthbar UI, either place it at the top of the HUD
    //       or stick with the current approach of having it above game object
    [Header("Healthbar UI")]
    [SerializeField] private HealthbarSliderLookAt healthbarSliderUI;

    [Header("Particles")]
    [SerializeField] private string damageParticlesPoolTag;
    [SerializeField] private string deadParticlesPoolTag;
    private ObjectPooler objectPooler;

    [Header("SFX")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string hitImpactClipName;

    [Header("Death")]
    [Tooltip("Fill in only one method of death")]
    [SerializeField] private RagdollDeath ragdollDeath;

    private Health nonPlayerHealth;
    
    void Awake()
    {
        nonPlayerHealth = new Health(100, 100, null, null);
    }

    void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    public void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitNormal, bool isMeleeWeapon=false, bool toBroadcast=true)
    {
        nonPlayerHealth.TakeDamage(damage, hitPoint, hitNormal, false);

        objectPooler.SpawnFromPool(damageParticlesPoolTag, hitPoint, Quaternion.LookRotation(hitNormal));
        
        if(isMeleeWeapon) audioManager.PlayAudio(hitImpactClipName);

        CheckIfDead(hitNormal);

        healthbarSliderUI.SetHealthbar((float)nonPlayerHealth.currentHealth);
    }

    public void CheckIfDead(Vector3 hitPos)
    {
        if(nonPlayerHealth.currentHealth <= 0f)
        {
            if(ragdollDeath != null)
            {
                ragdollDeath.Die(hitPos);
            }
            else
            {
                objectPooler.SpawnFromPool(deadParticlesPoolTag, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
