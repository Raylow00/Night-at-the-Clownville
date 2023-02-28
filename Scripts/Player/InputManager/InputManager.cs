using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Serialized Fields
    // Mouse input events
    [SerializeField] private Vector2Event onHorizontalMovementInputRequestEvent;
    [SerializeField] private FloatEvent onMouseInputXRequestEvent;
    [SerializeField] private FloatEvent onMouseInputYRequestEvent;
    // Keyboard input events
    [SerializeField] private IntEvent onNumKeyRequestEvent;
    [SerializeField] private VoidEvent onJumpRequestEvent;
    [SerializeField] private VoidEvent onCrouchRequestEvent;
    [SerializeField] private VoidEvent onSwitchLastUsedWeaponRequestEvent;
    [SerializeField] private VoidEvent onReloadRequestEvent;
    [SerializeField] private VoidEvent onWeaponInspectRequestEvent;
    [SerializeField] private VoidEvent onEquipCameraRequestEvent;
    [SerializeField] private VoidEvent onUseMapRequestEvent;
    [SerializeField] private VoidEvent onFlashlightRequestEvent;
    [SerializeField] private VoidEvent onPauseGameRequestEvent;

    [SerializeField] private BoolEvent onInteractiveButtonPressRequestEvent;
    [SerializeField] private VoidEvent onInteractiveButtonHoldRequestEvent;
    [SerializeField] private VoidEvent onInteractiveButtonCancelHoldRequestEvent;

    [SerializeField] private VoidEvent onWeaponSkinEquipRequestEvent;
    [SerializeField] private BoolEvent onShiftButtonPressEvent;

    #endregion

    #region Private Fields
    private PlayerController playerController;
    private PlayerController.GameplayControlsActions gameplayControlsActions;

    private Vector2 mouseInput;
    #endregion

    void Awake()
    {
        playerController = new PlayerController();
        gameplayControlsActions = playerController.GameplayControls;

        AssignActions();
    }

    void Update()
    {
        if (Input.GetButton("Shift"))
        {
            onShiftButtonPressEvent.Raise(true);
        }
        else if (Input.GetButtonUp("Shift"))
        {
            onShiftButtonPressEvent.Raise(false);
        }
    }

    void OnEnable()
    {
        playerController.Enable();
    }

    void OnDestroy()
    {
        playerController.Disable();
    }

    private void AssignActions()
    {
        gameplayControlsActions.PauseAction.performed += _ => OnPauseButtonPressed();
        gameplayControlsActions.HorizontalMovement.performed += context => MoveHorizontal(context.ReadValue<Vector2>());
        gameplayControlsActions.Jump.performed += _ => OnJumpPressed();
        gameplayControlsActions.MouseX.performed += context => mouseInput.x = context.ReadValue<float>();
        gameplayControlsActions.MouseY.performed += context => mouseInput.y = context.ReadValue<float>();
        gameplayControlsActions.Crouch.performed += _ => OnCrouchPressed();
        gameplayControlsActions.Weapon1.performed += _ => ToggleWeapon(0);
        gameplayControlsActions.Weapon2.performed += _ => ToggleWeapon(1);
        gameplayControlsActions.Weapon3.performed += _ => ToggleWeapon(2);
        gameplayControlsActions.Weapon4.performed += _ => ToggleWeapon(3);
        gameplayControlsActions.Weapon5.performed += _ => ToggleWeapon(4);
        gameplayControlsActions.SwitchEquipment.performed += _ => SwitchLastUsedWeapon();

        // Weapon skins
        gameplayControlsActions.ToggleWeaponSkin.performed += _ => EquipSkin();

        // Reload weapon
        gameplayControlsActions.Reload.performed += _ => ReloadWeapon();

        // Inspect weapon
        gameplayControlsActions.Inspect.performed += _ => InspectWeapon();

        // Camera
        gameplayControlsActions.Camera.performed += _ => EquipCamera();

        // Map view
        gameplayControlsActions.UseMap.performed += _ => UseMap();

        // Items Actions
        gameplayControlsActions.Flashlight.performed += _ => OnFlashlightPressed();

        // Interactive button - Press / Hold
        gameplayControlsActions.PressInteractive.started += _ => PressInteractiveButton();
        gameplayControlsActions.PressInteractive.performed += _ => HoldInteractiveButton();
        gameplayControlsActions.PressInteractive.canceled += _ => ReleaseInteractiveButton();
    }

    private void MoveHorizontal(Vector2 arg_vector2Value)
    {
        Debug.Log("Vector2 value: " + arg_vector2Value);
        onHorizontalMovementInputRequestEvent.Raise(arg_vector2Value);
    }

    private void ReleaseInteractiveButton()
    {
        Debug.Log("Release Interactive Button");
        onInteractiveButtonPressRequestEvent.Raise(false);
    }

    private void HoldInteractiveButton()
    {
        Debug.Log("Hold Interactive Button");
        onInteractiveButtonHoldRequestEvent.Raise();
    }

    private void PressInteractiveButton()
    {
        Debug.Log("Press Interactive Button");
        onInteractiveButtonPressRequestEvent.Raise(true);
    }

    private void OnFlashlightPressed()
    {
        Debug.Log("Flashlight requested");
        onFlashlightRequestEvent.Raise();
    }

    private void UseMap()
    {
        Debug.Log("Map requested");
        onUseMapRequestEvent.Raise();
    }

    private void EquipCamera()
    {
        Debug.Log("Camera requested");
        onEquipCameraRequestEvent.Raise();
    }

    private void InspectWeapon()
    {
        Debug.Log("Inspect weapon requested");
        onWeaponInspectRequestEvent.Raise();
    }

    private void ReloadWeapon()
    {
        Debug.Log("Reload weapon requested");
        onReloadRequestEvent.Raise();
    }

    private void EquipSkin()
    {
        Debug.Log("Equip skin requested");
        onWeaponSkinEquipRequestEvent.Raise();
    }

    private void SwitchLastUsedWeapon()
    {
        Debug.Log("Switch last used weapon requested");
        onSwitchLastUsedWeaponRequestEvent.Raise();
    }

    private void ToggleWeapon(int arg_value)
    {
        Debug.Log("Toggle weapon requested: " + arg_value);
        onNumKeyRequestEvent.Raise(arg_value);
    }

    private void OnCrouchPressed()
    {
        Debug.Log("Crouch requested");
        onCrouchRequestEvent.Raise();
    }

    private void OnJumpPressed()
    {
        Debug.Log("Jump requested");
        onJumpRequestEvent.Raise();
    }

    private void OnPauseButtonPressed()
    {
        Debug.Log("Pause button requested");
        onPauseGameRequestEvent.Raise();
    }
}
