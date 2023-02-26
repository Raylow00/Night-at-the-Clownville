using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized Fields
    [Header("Scriptable Object")]
    [SerializeField] private PlayerMovementScriptableObject playerMovementSO;

    [Header("Character Controller")]
    [SerializeField] private CharacterController characterController;

    [Header("Vertical Movement / Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float groundDistance;
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
    #endregion

    void Start()
    {
        InitMovement();
    }

    void FixedUpdate()
    {
        ProcessHorizontalMovement();
        ProcessVerticalMovement();
        HandleBobbing();
    }

    public void ReceiveHorizontalInput(Vector2 arg_horizontalInput)
    {
        horizontalInput = arg_horizontalInput;
    }

    public void ReceiveJumpInput()
    {
        jumpRequest = true;
    }

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
        }
        else
        {
            // Crouching height
            tempPosition.y = crouchHeight;
            transform.position = tempPosition;
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
        }

        playerMovementSO.currentHeight = characterController.height;
        playerMovementSO.currentRadius = characterController.radius;
        playerMovementSO.currentSpeedFactor = groundMovementHorizontalSpeedFactor;
        playerMovementSO.currentBobbingSpeed = bobbingSpeed;
        playerMovementSO.currentBobbingAmount = bobbingAmount;
    }

    #region Private Methods
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
    }

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

    private void ProcessVerticalMovement()
    {
        ProcessJump();
    }

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
    #endregion
}
