using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized Fields
    [Header("Scriptable Object")]
    [SerializeField] private PlayerMovementScriptableObject playerMovementSO;
    [SerializeField] private PlayerFootstepScriptableObject playerFootstepSO;

    [Header("Character Controller")]
    [SerializeField] private CharacterController characterController;

    [Header("Vertical Movement / Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float groundDistance;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private VoidEvent onCharacterJumpEvent;
    [SerializeField] private VoidEvent onCharacterLandEvent;

    [Header("Vertical Movement / Crouch")]
    [SerializeField] private float crouchHeight;
    [SerializeField] private float crouchingSpeedFactor;
    [SerializeField] private float crouchingCharacterControllerRadius;
    [SerializeField] private float standHeight;
    [SerializeField] private float standingSpeedFactor;
    [SerializeField] private float standingCharacterControllerRadius;
    [SerializeField] private VoidEvent onCharacterCrouchTrueEvent;
    [SerializeField] private VoidEvent onCharacterCrouchFalseEvent;

    [Header("Head bobbing")]
    [Tooltip("This is the camera assigned in the Virtual Camera under the Follow field")]
    [SerializeField] private Transform bobbingCameraTransform;
    [SerializeField] private float standingBobbingSpeed;
    [SerializeField] private float standingBobbingAmount;
    [SerializeField] private float crouchingBobbingSpeed;
    [SerializeField] private float crouchingBobbingAmount;

    [Header("Footstep")]
    [SerializeField] private string[] groundTypeTags;
    [SerializeField] private float footstepRate = 0.39f;
    [SerializeField] private float footstepCooldown = 0.5f;
    [SerializeField] private float increasedFootstepRate;
    [SerializeField] private float increasedFootstepCooldown;
    [SerializeField] private StringEvent onGroundTypeSendEvent;

    [Header("Player Energy")]
    [SerializeField] private TextViewer textViewer;
    [SerializeField] private float energyDepletionRate;
    [SerializeField] private float energyRechargeRate;

    [Header("Increased Horizontal Movement")]
    [SerializeField] private float increasedMovementSpeedFactor;
    [SerializeField] private float increasedMovementBobbingSpeed;
    [SerializeField] private float increasedMovementBobbingAmount;

    #endregion

    #region Private Fields
    private Transform cameraTransform;
    // Horizontal Movement variables
    private Vector2 horizontalInput;
    private Vector3 cachedHorizontalInput;
    private Vector3 horizontalVelocity;
    private float groundMovementHorizontalSpeedFactor;

    // Vertical Movement variables - Jump
    private bool jumpRequest;
    private bool isGrounded;        // on the ground
    private bool isLanding;         // in the air && velocity.y < 0
    private Vector3 verticalVelocity;

    // Vertical Movement variables - Crouch
    private Vector3 tempPosition;
    private bool isCrouching;

    // Head bobbing
    private float bobbingSpeed = 15f;
    private float bobbingAmount = 0.12f;
    private float defaultPosY = 0f;
    private float timer;

    // Footsteps
    private float currentFootstepRate;
    private float currentFootstepCooldown;
    private string currentGroundTypeTag;
    private bool hasIncreasedFootstepRate;

    // Energy
    private float currentEnergy;
    private float maxEnergy = 100;
    private bool isEnergyZero;
    private bool toRechargeEnergy;
    #endregion

    void Start()
    {
        InitMovement();
        InitEnergy();
    }

    void FixedUpdate()
    {
        ProcessHorizontalMovement();
        ProcessVerticalMovement();
        HandleBobbing();
        HandleFootsteps();

        CheckIfRechargeEnergy();
    }

    /// <summary>
    ///     Receive horizontal input for movement update
    /// </summary>
    /// <param name="arg_horizontalInput"></param>
    public void ReceiveHorizontalInput(Vector2 arg_horizontalInput)
    {
        horizontalInput = arg_horizontalInput;
    }

    /// <summary>
    ///     Receive spacebar input for jump request
    /// </summary>
    public void ReceiveJumpInput()
    {
        jumpRequest = true;
    }

    /// <summary>
    ///     Receive shift button input for increased movement request
    /// </summary>
    /// <param name="arg_isShiftButtonPressed"></param>
    public void ReceiveShiftButtonInput(bool arg_isShiftButtonPressed)
    {
        if (arg_isShiftButtonPressed && horizontalInput.x != 0 && horizontalInput.y != 0)
        {
            groundMovementHorizontalSpeedFactor = increasedMovementSpeedFactor;
            bobbingSpeed = increasedMovementBobbingSpeed;
            bobbingAmount = increasedMovementBobbingAmount;

            DecreaseEnergy(energyDepletionRate);
        }
        else
        {
            groundMovementHorizontalSpeedFactor = standingSpeedFactor;
            bobbingSpeed = standingBobbingSpeed;
            bobbingAmount = standingBobbingAmount;

            toRechargeEnergy = true;
        }

        if (currentEnergy > 0f)
        {
            if (!hasIncreasedFootstepRate)
            {
                currentFootstepRate = increasedFootstepRate;
                currentFootstepCooldown = increasedFootstepCooldown;
                hasIncreasedFootstepRate = true;
            }
        }
        else
        {
            RestoreFootstepRate();
        }
    }

    /// <summary>
    ///     Returns whether energy is zero
    /// </summary>
    /// <returns>
    ///     True if zero
    ///     False otherwise
    /// </returns>
    public bool GetEnergyZero()
    {
        return isEnergyZero;
    }

    /// <summary>
    ///     Add value to energy if not already maximum
    /// </summary>
    /// <param name="arg_increment"></param>
    public void IncreaseEnergy(float arg_increment)
    {
        if (currentEnergy >= maxEnergy)
        {
            return;
        }
        else
        {
            currentEnergy += arg_increment;
            if (currentEnergy >= maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
        }

        // Sets the current health on UI
        if (textViewer != null)
        {
            textViewer.SetText(currentEnergy);
        }
    }

    /// <summary>
    ///     Decrement value from energy if not already zero
    /// </summary>
    /// <param name="arg_damage"></param>
    public void DecreaseEnergy(float arg_damage)
    {
        if (isEnergyZero)
        {
            return;
        }
        else
        {
            currentEnergy -= arg_damage;
            if (currentEnergy <= 0f)
            {
                isEnergyZero = true;
                currentEnergy = 0;
            }

            if (isEnergyZero)
            {
                return;
            }
        }

        // Sets the current energy on UI
        if (textViewer != null)
        {
            textViewer.SetText(currentEnergy);
        }
    }

    /// <summary>
    ///     Set the below properties according to crouch request
    ///     - character controller height
    ///     - transform position
    ///     - speed factor
    ///     - bobbing speed
    ///     - bobbing amount
    /// </summary>
    //public void ProcessCrouch()
    //{
    //    // if already crouching,
    //    // set it to stand
    //    // otherwise, crouch
    //    float crouchedHeight = isCrouching? standHeight : crouchHeight;
    //    float crouchedRadius = isCrouching ? standingCharacterControllerRadius : crouchingCharacterControllerRadius;

    //    if (characterController.height != crouchedHeight)
    //    {
    //        AdjustController(crouchedHeight);

    //        Vector3 camPosition = cameraTransform.localPosition;
    //        camPosition.y = characterController.height;
    //        characterController.radius = crouchedRadius;

    //        cameraTransform.localPosition = camPosition;
    //    }

    //    if (isCrouching)
    //    {
    //        // Standing movement speed
    //        groundMovementHorizontalSpeedFactor = standingSpeedFactor;

    //        // Bobbing speed and amount
    //        bobbingSpeed = standingBobbingSpeed;
    //        bobbingAmount = standingBobbingAmount;

    //        isCrouching = false;
    //        onCharacterCrouchFalseEvent.Raise();
    //    }
    //    else
    //    {
    //        // Crouching movement speed
    //        groundMovementHorizontalSpeedFactor = crouchingSpeedFactor;

    //        // Bobbing speed and amount
    //        bobbingSpeed = crouchingBobbingSpeed;
    //        bobbingAmount = crouchingBobbingAmount;

    //        isCrouching = true;
    //        onCharacterCrouchTrueEvent.Raise();
    //    }

    //    playerMovementSO.currentHeight = characterController.height;
    //    playerMovementSO.currentRadius = characterController.radius;
    //    playerMovementSO.currentSpeedFactor = groundMovementHorizontalSpeedFactor;
    //    playerMovementSO.currentBobbingSpeed = bobbingSpeed;
    //    playerMovementSO.currentBobbingAmount = bobbingAmount;
    //}

    public void ProcessCrouch()
    {
        if (isCrouching)
        {
            //Standing Height
            tempPosition.y = standHeight;
            transform.position = tempPosition;
            characterController.height = standHeight;

            // Standing radius
            characterController.radius = standingCharacterControllerRadius;

            // Standing movement speed
            groundMovementHorizontalSpeedFactor = standingSpeedFactor;

            // Bobbing speed and amount
            bobbingSpeed = standingBobbingSpeed;
            bobbingAmount = standingBobbingAmount;

            isCrouching = false;
            onCharacterCrouchFalseEvent.Raise();
            Debug.Log("Standing up");
        }
        else
        {
            // Crouching height
            tempPosition.y = crouchHeight;
            transform.position = tempPosition;
            characterController.height = characterController.height * 2;
            characterController.height = crouchHeight;

            // Crouching radius
            characterController.radius = crouchingCharacterControllerRadius;

            // Crouching movement speed
            groundMovementHorizontalSpeedFactor = crouchingSpeedFactor;

            // Bobbing speed and amount
            bobbingSpeed = crouchingBobbingSpeed;
            bobbingAmount = crouchingBobbingAmount;

            isCrouching = true;
            onCharacterCrouchTrueEvent.Raise();
            Debug.Log("Crouching");
        }

        playerMovementSO.currentHeight = characterController.height;
        playerMovementSO.currentRadius = characterController.radius;
        playerMovementSO.currentSpeedFactor = groundMovementHorizontalSpeedFactor;
        playerMovementSO.currentBobbingSpeed = bobbingSpeed;
        playerMovementSO.currentBobbingAmount = bobbingAmount;
    }

    #region Private Methods
    /// <summary>
    ///     Initialize movement properties
    /// </summary>
    private void InitMovement()
    {
        cameraTransform                         = Camera.main.transform;
        cachedHorizontalInput                   = Vector3.zero;
        groundMovementHorizontalSpeedFactor     = standingSpeedFactor;
        tempPosition                            = transform.position;
        jumpRequest                             = false;
        isGrounded                              = true;
        isCrouching                             = false;
        isLanding                               = false;
        bobbingSpeed                            = standingBobbingSpeed;
        bobbingAmount                           = standingBobbingAmount;
        currentFootstepRate                     = footstepRate;
        currentFootstepCooldown                 = footstepCooldown;
    }

    /// <summary>
    ///     Initialize energy to max energy
    ///     and set its text on UI
    /// </summary>
    private void InitEnergy()
    {
        currentEnergy = maxEnergy;
        isEnergyZero = false;

        // Sets the current energy on UI
        if (textViewer != null)
        {
            textViewer.SetText(currentEnergy);
        }
    }

    /// <summary>
    ///     Process horizontal movement based on ground movement horizontal speed factor
    ///     This is only if character controller is enabled
    ///     Otherwise, character controller will not move
    /// </summary>
    private void ProcessHorizontalMovement()
    {
        // Horizontal movement
        cachedHorizontalInput.x = horizontalInput.x;
        cachedHorizontalInput.z = horizontalInput.y;
        horizontalVelocity = cachedHorizontalInput;
        horizontalVelocity = cameraTransform.forward * horizontalVelocity.z + cameraTransform.right * horizontalVelocity.x;
        if (characterController.enabled)
        {
            characterController.Move(horizontalVelocity * Time.deltaTime * groundMovementHorizontalSpeedFactor);
        }
        else
        {
            Debug.LogWarning("Character controller disabled. Movement not updated.");
        }
    }

    /// <summary>
    ///     Wrapper to process vertical movement - jump
    /// </summary>
    private void ProcessVerticalMovement()
    {
        ProcessJump();
    }

    /// <summary>
    ///     Process jump request and check whether character is gorunded
    ///     Process land action by checking if it is landing or has landed
    /// </summary>
    private void ProcessJump()
    {
        // Check if grounded - ensure character stays grounded
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayer);
        if (isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
            isLanding = false;
        }

        // Vertical movement
        verticalVelocity.y += gravity * Time.deltaTime;
        if (characterController.enabled)
        {
            characterController.Move(verticalVelocity * Time.deltaTime);
        }

        // Jump
        if (jumpRequest != false)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                isLanding = true;
                characterController.stepOffset = 1.0f;

                onCharacterJumpEvent.Raise();
            }
            else
            {
                // Remove jittering when jumping over ledges
                characterController.stepOffset = 0f;
            }
        }
        else
        {
            // Do nothing
        }

        if (isLanding)
        {
            if (verticalVelocity.y < 10f)
            {
                bool hasLanded = Physics.CheckSphere(transform.position, groundDistance, groundLayer);
                if (hasLanded)
                {
                    isLanding = false;
                    jumpRequest = false;        // no double jump in mid air
                    characterController.stepOffset = 1.0f;

                    onCharacterLandEvent.Raise();
                }
            }
        }
        else
        {
            // Do nothing
        }
    }

    /// <summary>
    ///     Handles the bobbing of the camera based on the movement
    /// </summary>
    private void HandleBobbing()
    {
        // If moving
        if (Mathf.Abs(horizontalVelocity.x) > 0.1f || Mathf.Abs(horizontalVelocity.z) > 0.1f)
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

    /// <summary>
    ///     Handles footsteps by checking the ground type and setting the footstep rate and cooldown
    ///     Also sends out the ground type tag for audio manager to process
    /// </summary>
    private void HandleFootsteps()
    {
        // Footsteps
        footstepCooldown -= Time.deltaTime;

        // Check the tag of the ground
        Collider[] hitGroundColliders = Physics.OverlapSphere(transform.position, groundDistance, groundLayer);
        foreach (var hitGroundCollider in hitGroundColliders)
        {
            foreach (string tag in groundTypeTags)
            {
                if (hitGroundCollider.gameObject.tag == tag)
                {
                    currentGroundTypeTag = tag;
                    // Send current ground type tag so AudioManager can play sound according to ground type
                    onGroundTypeSendEvent.Raise(currentGroundTypeTag);
                }
            }

            if (hitGroundCollider != null && (horizontalInput.x != 0f || horizontalInput.y != 0f) && footstepCooldown < 0f)
            {
                footstepCooldown = currentFootstepRate;
            }
        }
    }

    /// <summary>
    ///     Checks if toRechargeEnergy is set to true
    ///     If true, increase energy until max
    ///     Otherwise, do nothing
    /// </summary>
    private void CheckIfRechargeEnergy()
    {
        // Energy - increased movement speed
        if (toRechargeEnergy)
        {
            IncreaseEnergy(energyRechargeRate);

            if (currentEnergy >= maxEnergy)
            {
                toRechargeEnergy = false;
                currentEnergy = maxEnergy;
            }
        }
    }

    /// <summary>
    ///     Restore footstep rate if hasIncreasedFootstepRate is set to true
    ///     Otherwise, do nothing
    /// </summary>
    private void RestoreFootstepRate()
    {
        if (hasIncreasedFootstepRate)
        {
            currentFootstepRate = footstepRate;
            currentFootstepCooldown = footstepCooldown;
            hasIncreasedFootstepRate = false;
        }
    }

    //private void AdjustController(float height)
    //{
    //    float center = height / 2;

    //    characterController.height = Mathf.LerpUnclamped(characterController.height, height, 5f * Time.deltaTime);
    //    characterController.center = Vector3.LerpUnclamped(characterController.center, new Vector3(0, center, 0), 5f * Time.deltaTime);
    //}
    #endregion
}
