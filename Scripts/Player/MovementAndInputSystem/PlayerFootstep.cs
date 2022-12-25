using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    [Header("Ground Type")]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private string[] groundTypeTags;
    private string currentGroundType;

    [Header("Footsteps Properties")]
    [SerializeField] private PlayerMovement playerMovement;
    [Range(0.3f, 0.5f)]
    [SerializeField] private float walkingFootstepRate = 0.39f;
    [Range(0.1f, 1f)]
    [SerializeField] private float walkingFootstepCooldown = 0.5f;
    [Range(0.2f, 0.3f)]
    [SerializeField] private float runningFootstepRate = 0.27f;
    [Range(0.1f, 1f)]
    [SerializeField] private float runningFootstepCooldown = 0.5f;

    [Header("Player Energy")]
    [SerializeField] private PlayerEnergySystem playerEnergySystem;

    [Header("SFX")]
    [SerializeField] private AudioManager audioManager;

    private Vector2 horizontalInput;

    private float footstepRate;
    private float footstepCooldown;
    private bool hasIncreasedFootstepRate;

    void Awake()
    {
        footstepRate = walkingFootstepRate;
        footstepCooldown = walkingFootstepCooldown;
    }

    void Update()
    {
        // Footsteps
        footstepCooldown -= Time.deltaTime;

         // Check the tag of the ground
        Collider[] hitGroundColliders = Physics.OverlapSphere(transform.position, groundDistance, groundLayer);
        foreach(var hitGroundCollider in hitGroundColliders)
        {
            if(hitGroundCollider != null && (horizontalInput.x != 0f || horizontalInput.y != 0f) && footstepCooldown < 0f)
            {
                audioManager.PlayAudio(currentGroundType);
                
                footstepCooldown = footstepRate;
            }
            
            foreach(string tag in groundTypeTags)
            {
                if(hitGroundCollider.gameObject.tag == tag)
                {
                    currentGroundType = tag;
                }
            }
        }
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void IncreaseFootstepRate()
    {
        if(playerEnergySystem.playerEnergy.currentEnergy > 0f)
        {
            if(!hasIncreasedFootstepRate)
            {
                footstepRate = runningFootstepRate;
                footstepCooldown = runningFootstepCooldown;
                hasIncreasedFootstepRate = true;
            }
        }
        else
        {
            RestoreFootstepRate();
        }
    }

    public void RestoreFootstepRate()
    {
        if(hasIncreasedFootstepRate)
        {
            footstepRate = walkingFootstepRate;
            footstepCooldown = walkingFootstepCooldown;
            hasIncreasedFootstepRate = false;
        }
    }
}
