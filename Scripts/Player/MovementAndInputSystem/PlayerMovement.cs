using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Controller")]
    [SerializeField] private CharacterController characterController;

    [Header("Player Stats")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Player Energy System")]
    [SerializeField] private PlayerEnergySystem playerEnergySystem;

    [Header("Current active weapon")]
    [SerializeField] private PlayerWeaponHandler weaponHandler;

    [Header("Player Camera")]
    [SerializeField] private PlayerCameraHandler cameraHandler;

    [Header("Horizontal Movement")]
    [Range(10f, 18f)]
    [SerializeField] private float standingMovementSpeed = 11f;
    [Range(14f, 16f)]
    [SerializeField] private float standingBobbingSpeed = 15;
    [Range(0.1f, 0.3f)]
    [SerializeField] private float standingBobbingAmount = 0.12f;
    [Range(0f, 50f)]
    [SerializeField] private float increasedStandingMovementSpeed = 22f;
    [Range(15f, 40f)]
    [SerializeField] private float increasedStandingBobbingSpeed = 30f;
    [Range(0.1f, 0.15f)]
    [SerializeField] private float increasedStandingBobbingAmount = 0.12f;
    private Vector3 horizontalVelocity;
    private Vector2 horizontalInput;
    private float groundMovementSpeed;
    private bool toRechargeEnergy;

    [Header("Vertical Movement / Jump")]
    [Range(-50f, -10f)]
    [SerializeField] private float gravity = -30f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private float jumpHeight = 3.5f;
    private bool isGrounded;
    private bool hasPressedJump;
    private Vector3 verticalVelocity = Vector3.zero;
    private bool isLanding;

    [Header("Vertical Movement / Crouch")]
    [SerializeField] private float playerStandingPositionY = 2.34f;
    [SerializeField] private float playerCrouchPositionY = 0.8f;
    [Range(1f, 4.6f)]
    [SerializeField] private float crouchHeight = 1.8f;
    [Range(0.1f, 1f)]
    [SerializeField] private float crouchRadius = 0.6f;
    [Range(1f, 10f)]
    [SerializeField] private float crouchingMovementSpeed = 3.5f;
    [Range(1f, 14f)]
    [SerializeField] private float crouchingBobbingSpeed = 8f;
    [Range(0.01f, 0.1f)]
    [SerializeField] private float crouchingBobbingAmount = 0.06f;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string footstepsAudioMixerGroupParamName;
    [SerializeField] private float originalFootstepVolumeGain = 0f;
    [SerializeField] private float crouchingFootstepVolumeGain = -10f;
    private float originalHeight;
    private float originalRadius;
    private bool isCrouching;
    public bool IsCrouching => isCrouching;

    [Header("Head bobbing")]
    [SerializeField] private Transform bobbingCameraTransform;
    private Transform cameraTransform;
    private float bobbingSpeed = 15f;
    private float bobbingAmount = 0.12f;
    private float defaultPosY = 0f;
    private float timer;

    void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    void Start()
    {
        // Movement speed
        groundMovementSpeed = standingMovementSpeed;

        // Crouch
        originalHeight = characterController.height;
        originalRadius = characterController.radius;
        isCrouching = false;

        // Head bobbing
        defaultPosY = transform.localPosition.y;
        bobbingSpeed = standingBobbingSpeed;
        bobbingAmount = standingBobbingAmount;
    }

    void Update()
    {
        // Check for movement for head bobbing
        HandleBobbing();

        // Check for movement to control arm animations
        HandleArmAnimation();
    }

    void FixedUpdate()
    {
        // Check if grounded - ensure character stays grounded
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayer);
        if(isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
            isLanding = false;
        }

        // Horizontal movement
        //horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * groundMovementSpeed;
        horizontalVelocity = new Vector3(horizontalInput.x, 0f, horizontalInput.y);
        horizontalVelocity = cameraTransform.forward * horizontalVelocity.z + cameraTransform.right * horizontalVelocity.x;
        if(characterController.enabled) characterController.Move(horizontalVelocity * Time.deltaTime * groundMovementSpeed);

        // Energy - increased movement speed
        if(toRechargeEnergy)
        {
            playerStats.playerCurrentEnergy = playerEnergySystem.playerEnergy.IncreaseEnergy(playerEnergySystem.energyRechargeRate);

            if(playerEnergySystem.playerEnergy.currentEnergy >= playerEnergySystem.playerEnergy.maxEnergy)
            {
                toRechargeEnergy = false;
                playerStats.playerCurrentEnergy = playerEnergySystem.playerEnergy.maxEnergy;
            }
        }

        // Vertical movement
        verticalVelocity.y += gravity * Time.deltaTime;
        if(characterController.enabled) characterController.Move(verticalVelocity * Time.deltaTime);

        // Jump
        if(hasPressedJump)
        {
            if(isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                isLanding = true;

                characterController.stepOffset = 1.0f;
            }
            else{
                // Remove jittering when jumping over ledges
                characterController.stepOffset = 0f;
            }
        }

        if(isLanding)
        {
            if(verticalVelocity.y < 10f)
            {
                bool hasLanded = Physics.CheckSphere(transform.position, groundDistance, groundLayer);
                if(hasLanded)
                {
                    weaponHandler.GetActiveWeapon().gameObject.GetComponent<Animator>().SetTrigger("land");
                    isLanding = false;
                    hasPressedJump = false;

                    characterController.stepOffset = 1.0f;
                }
            }
        }
    }

    public void ReceiveHorizontalInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJumpPressed()
    {
        hasPressedJump = true;
    }

    public void OnCrouchPressed()
    {        
        if(isCrouching)
        {
            transform.position = new Vector3(transform.position.x, playerStandingPositionY, transform.position.z);
            characterController.radius = 2f;
            characterController.height = originalHeight;    //Standing hHeight
            groundMovementSpeed = standingMovementSpeed;      // Standing movement speed
            bobbingSpeed = standingBobbingSpeed;        // Standing bobbing speed
            bobbingAmount = standingBobbingAmount;      // Standing bobbing amount

            audioMixer.SetFloat(footstepsAudioMixerGroupParamName, originalFootstepVolumeGain);

            isCrouching = false;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, playerCrouchPositionY, transform.position.z);
            characterController.radius = 0.6f;
            characterController.height = crouchHeight;      // Crouching height
            groundMovementSpeed = crouchingMovementSpeed;     // Crouching movement speed
            bobbingSpeed = crouchingBobbingSpeed;       // Crouching bobbing speed
            bobbingAmount = crouchingBobbingAmount;     // Crouching bobbing amount;

            audioMixer.SetFloat(footstepsAudioMixerGroupParamName, crouchingFootstepVolumeGain);

            isCrouching = true;
        }
    }

    public void IncreaseMovementSpeed()
    {
        if(playerEnergySystem.playerEnergy.currentEnergy > 0)
        {
            // Debug.Log("Increased energy");
            groundMovementSpeed = increasedStandingMovementSpeed;
            bobbingSpeed = increasedStandingBobbingSpeed;
            bobbingAmount = increasedStandingBobbingAmount;

            toRechargeEnergy = false;

            if(horizontalInput.x != 0f || horizontalInput.y != 0f)
            {
                playerStats.playerCurrentEnergy = playerEnergySystem.playerEnergy.DecreaseEnergy(playerEnergySystem.energyDepletionRate);
            }
        }
        else
        {
            // Debug.Log("Restoring energy");
            RestoreMovementSpeed();
            return;
        }
    }

    public void RestoreMovementSpeed()
    {
        groundMovementSpeed = standingMovementSpeed;
        bobbingSpeed = standingBobbingSpeed;
        bobbingAmount = standingBobbingAmount;
        
        toRechargeEnergy = true;
    }

    public void StopCharacterControllerMovement()
    {
        characterController.Move(Vector3.zero);
    }

    private void HandleBobbing()
    {
        // If moving
        if(Mathf.Abs(horizontalVelocity.x) > 0.1f || Mathf.Abs(horizontalVelocity.z) > 0.1f)
        {
            timer += Time.deltaTime * bobbingSpeed;
            bobbingCameraTransform.localPosition = new Vector3(bobbingCameraTransform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, bobbingCameraTransform.localPosition.z);
        }
        // If idle
        else
        {
            timer = 0f;
            bobbingCameraTransform.localPosition = new Vector3(bobbingCameraTransform.localPosition.x, Mathf.Lerp(bobbingCameraTransform.localPosition.y, defaultPosY, Time.deltaTime * bobbingSpeed), bobbingCameraTransform.localPosition.z);
        }
    }

    private void HandleArmAnimation()
    {
        if((Mathf.Abs(horizontalVelocity.x) > 0.1f || Mathf.Abs(horizontalVelocity.z) > 0.1f) && groundMovementSpeed == standingMovementSpeed)
        {
            weaponHandler.GetActiveWeapon().gameObject.GetComponent<Animator>().SetFloat("horizontalSpeed", 0.65f, 0.2f, Time.deltaTime);
            cameraHandler.GetComponent<Animator>().SetFloat("horizontalSpeed", 0.5f, 0.2f, Time.deltaTime);
        }
        else if ((Mathf.Abs(horizontalVelocity.x) > 0.1f || Mathf.Abs(horizontalVelocity.z) > 0.1f) && groundMovementSpeed == increasedStandingMovementSpeed)
        {
            weaponHandler.GetActiveWeapon().gameObject.GetComponent<Animator>().SetFloat("horizontalSpeed", 0.8f, 0.2f, Time.deltaTime);
            cameraHandler.GetComponent<Animator>().SetFloat("horizontalSpeed", 1f, 0.2f, Time.deltaTime);
        }
        else
        {
            weaponHandler.GetActiveWeapon().gameObject.GetComponent<Animator>().SetFloat("horizontalSpeed", 0f, 0.2f, Time.deltaTime);
            cameraHandler.GetComponent<Animator>().SetFloat("horizontalSpeed", 0f, 0.2f, Time.deltaTime);
        }
    }
}
