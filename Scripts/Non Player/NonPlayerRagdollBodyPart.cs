using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerRagdollBodyPart : MonoBehaviour, IDamageable
{
    [Header("Ragdoll Physics")]
    [SerializeField] private bool toToggleRagdoll;
    [SerializeField] private float toggleRagdollOnForThisTime;

    [Header("Properties of body part")]
    [SerializeField] private bool isCriticalHit;
    [SerializeField] private int criticalHitMultiplier = 2;

    [Header("Particles")]
    [SerializeField] private string damageParticlesPoolTag;
    [SerializeField] private string deadParticlesPoolTag;
    private ObjectPooler objectPooler;

    [Header("SFX")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string hitImpactClipName;

    private Rigidbody rb;
    private Collider col;
    private Animator anim;
    private NonPlayerHealthSystem nonPlayerHealthSystem;
    private RagdollDeath ragdollDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        anim = GetComponentInParent<Animator>();
        nonPlayerHealthSystem = GetComponentInParent<NonPlayerHealthSystem>();
        ragdollDeath = GetComponentInParent<RagdollDeath>();
    }

    public void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitNormal, bool isMeleeWeapon=false, bool toBroadcast=true)
    {
        int damageLevel = damage;

        if(isCriticalHit)
        {
            damageLevel = damage * criticalHitMultiplier;
        }
        
        nonPlayerHealthSystem.TakeDamage(damageLevel, hitPoint, hitNormal, isMeleeWeapon);
    }
}
