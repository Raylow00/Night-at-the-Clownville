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
        onHorizontalMovementInputRequestEvent.Raise(arg_vector2Value);
    }

    private void ReleaseInteractiveButton()
    {
        onInteractiveButtonPressRequestEvent.Raise(false);
    }

    private void HoldInteractiveButton()
    {
        onInteractiveButtonHoldRequestEvent.Raise();
    }

    private void PressInteractiveButton()
    {
        onInteractiveButtonPressRequestEvent.Raise(true);
    }

    private void OnFlashlightPressed()
    {
        onFlashlightRequestEvent.Raise();
    }

    private void UseMap()
    {
        onUseMapRequestEvent.Raise();
    }

    private void EquipCamera()
    {
        onEquipCameraRequestEvent.Raise();
    }

    private void InspectWeapon()
    {
        onWeaponInspectRequestEvent.Raise();
    }

    private void ReloadWeapon()
    {
        onReloadRequestEvent.Raise();
    }

    private void EquipSkin()
    {
        onWeaponSkinEquipRequestEvent.Raise();
    }

    private void SwitchLastUsedWeapon()
    {
        onSwitchLastUsedWeaponRequestEvent.Raise();
    }

    private void ToggleWeapon(int arg_value)
    {
        onNumKeyRequestEvent.Raise(arg_value);
    }

    private void OnCrouchPressed()
    {
        onCrouchRequestEvent.Raise();
    }

    private void OnJumpPressed()
    {
        onJumpRequestEvent.Raise();
    }

    private void OnPauseButtonPressed()
    {
        onPauseGameRequestEvent.Raise();
    }
}
