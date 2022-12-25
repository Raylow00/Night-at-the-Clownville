using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Player Controller")]
    private PlayerController playerController;
    private PlayerController.GameplayControlsActions gameplayControlsActions;

    [Header("Mouse Look")]
    [SerializeField] public bool toDisableCursorOnStart;
    [SerializeField] public bool toCheckForMouseActivity;
    [SerializeField] public bool toCheckForMouseButtonInput;
    [SerializeField] private CinemachinePOVExtension cinemachinePOVExtension;
    private Vector2 mouseInput;
    
    [Header("Crosshair Scaler")]
    [SerializeField] private CrosshairScaler crosshairScaler;
    [SerializeField] private CrosshairDetector crosshairDetector;

    [Header("Movement")]
    [SerializeField] public bool toCheckForMovement;
    [SerializeField] private PlayerMovement playerMovement;
    private Vector2 horizontalInput;

    [Header("Vehicle Movement")]
    [SerializeField] public bool toCheckForVehicleMovement;
    [SerializeField] private VehicleController vehicleController;

    [Header("Footsteps")]
    [SerializeField] private PlayerFootstep playerFootstep;

    [Header("Energy System")]
    [SerializeField] private PlayerEnergySystem playerEnergy;

    [Header("Weapon Handler")]
    [SerializeField] private PlayerWeaponHandler weaponHandler;

    [Header("Weapon/Camera Sway")]
    [SerializeField] private PlayerPossessionSway weaponSway;
    [SerializeField] private PlayerPossessionSway cameraSway;

    [Header("Camera Handler")]
    [SerializeField] private PlayerCameraHandler cameraHandler;

    [Header("Map Handler")]
    [SerializeField] private PlayerMapHandler mapHandler;

    [Header("Torchlight")]
    [SerializeField] private PlayerFlashlight playerFlashlight;

    [Header("Player Stats")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Menu Manager")]
    [SerializeField] private MainMenuManager menuManager;

    [Header("Key Rebindings")]
    [SerializeField] private PlayerKeyBindingsSO playerKeyBindingsSO;
    private InputAction crouchAction;
    private InputAction flashlightAction;
    private InputAction swapLastUsedAction;
    private InputAction swapWeaponSkinAction;
    private InputAction equipCameraAction;
    private InputAction viewMapAction;
    private InputAction pauseAction;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    void Awake()
    {
        playerController = new PlayerController();
        gameplayControlsActions = playerController.GameplayControls;
        crouchAction = gameplayControlsActions.Crouch;
        flashlightAction = gameplayControlsActions.Flashlight;
        swapLastUsedAction = gameplayControlsActions.SwitchEquipment;
        swapWeaponSkinAction = gameplayControlsActions.ToggleWeaponSkin;
        equipCameraAction = gameplayControlsActions.Camera;
        viewMapAction = gameplayControlsActions.UseMap;

        gameplayControlsActions.PauseAction.performed += _ => menuManager.OnPauseButtonPressed();

        SetBindings();

        // WASD movemenet and Camera
        gameplayControlsActions.HorizontalMovement.performed += context => horizontalInput = context.ReadValue<Vector2>();
        gameplayControlsActions.Jump.performed += _ => OnJumpPressed();
        gameplayControlsActions.MouseX.performed += context => mouseInput.x = context.ReadValue<float>();
        gameplayControlsActions.MouseY.performed += context => mouseInput.y = context.ReadValue<float>();
        gameplayControlsActions.Crouch.performed += _ => OnCrouchPressed();

        // Weapons Actions
        if(weaponHandler != null)
        {
            gameplayControlsActions.Weapon1.performed += _ => weaponHandler.ToggleWeapon(0);
            // gameplayControlsActions.Weapon2.performed += _ => weaponHandler.ToggleWeapon(1);
            // gameplayControlsActions.Weapon3.performed += _ => weaponHandler.ToggleWeapon(2);
            // gameplayControlsActions.Weapon4.performed += _ => weaponHandler.ToggleWeapon(3);
            // gameplayControlsActions.Weapon5.performed += _ => weaponHandler.ToggleWeapon(4);
            gameplayControlsActions.SwitchEquipment.performed += _ => weaponHandler.SwitchLastUsedWeapon();

            // Weapon skins
            gameplayControlsActions.ToggleWeaponSkin.performed += _ => weaponHandler.EquipSkin();

            // Reload weapon
            gameplayControlsActions.Reload.performed += _ => weaponHandler.ReloadWeapon();

            // Inspect weapon
            gameplayControlsActions.Inspect.performed += _ => weaponHandler.InspectWeapon();
        }
        
        // Camera
        // if(cameraHandler != null) gameplayControlsActions.Camera.performed += _ => cameraHandler.EquipCamera();

        // Map view
        // if(mapHandler != null) gameplayControlsActions.UseMap.performed += _ => mapHandler.UseMap();
        
        // Items Actions
        if(playerFlashlight != null) gameplayControlsActions.Flashlight.performed += _ => playerFlashlight.OnTorchPressed();

        // Interactive button - Press / Hold
        if(crosshairDetector != null) gameplayControlsActions.PressInteractive.started += _ => crosshairDetector.PressInteractiveButton();
        if(crosshairDetector != null) gameplayControlsActions.PressInteractive.performed += _ => crosshairDetector.HoldInteractiveButton();
        if(crosshairDetector != null) gameplayControlsActions.PressInteractive.canceled += _ => crosshairDetector.CancelHoldInteractiveButton();
    }

    void Start()
    {
        if(toDisableCursorOnStart) DisplayCursor(false);
    }

    // DEMO ONLY
    public void EnableOtherWeapons()
    {
        gameplayControlsActions.Weapon2.performed += _ => weaponHandler.ToggleWeapon(1);
        gameplayControlsActions.Weapon3.performed += _ => weaponHandler.ToggleWeapon(2);
        gameplayControlsActions.Weapon4.performed += _ => weaponHandler.ToggleWeapon(3);
        gameplayControlsActions.Weapon5.performed += _ => weaponHandler.ToggleWeapon(4);
    }

    public void EnableGamePlayControlsActions()
    {
        // Debug.Log("Enabling actions");
        gameplayControlsActions.Enable();

        // Debug.Log("Check for mouse inputs");
        toCheckForMouseActivity = true;
    }

    public void DisableGamePlayControlsActions()
    {
        // Debug.Log("Stop checking for mouse inputs");
        toCheckForMouseActivity = false;

        // Debug.Log("Stopping movement");
        playerMovement.StopCharacterControllerMovement();

        // Debug.Log("Disabling actions");
        gameplayControlsActions.Disable();
    }

    public void DisplayCursor(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }

    public void SetBindings()
    {
        // Crouch key
        InputBinding crouchBinding = crouchAction.bindings[0];
        crouchBinding.overridePath = "<Keyboard>/#(" + playerKeyBindingsSO.crouchKey + ")";
        crouchAction.ApplyBindingOverride(0, crouchBinding);

        // Flashlight key
        InputBinding flashlightBinding = flashlightAction.bindings[0];
        flashlightBinding.overridePath = "<Keyboard>/#(" + playerKeyBindingsSO.flashlightKey + ")";
        flashlightAction.ApplyBindingOverride(0, flashlightBinding);

        // Switch last used weapon key
        InputBinding lastUsedWeaponBinding = swapLastUsedAction.bindings[0];
        lastUsedWeaponBinding.overridePath = "<Keyboard>/#(" + playerKeyBindingsSO.switchLastUsedWeaponKey + ")";
        swapLastUsedAction.ApplyBindingOverride(0, lastUsedWeaponBinding);

        // Switch weapon skin key
        InputBinding weaponSkinBinding = swapWeaponSkinAction.bindings[0];
        weaponSkinBinding.overridePath = "<Keyboard>/#(" + playerKeyBindingsSO.switchWeaponSkinKey + ")";
        swapWeaponSkinAction.ApplyBindingOverride(0, weaponSkinBinding);

        // Equip camera key
        InputBinding cameraBinding = equipCameraAction.bindings[0];
        cameraBinding.overridePath = "<Keyboard>/#(" + playerKeyBindingsSO.equipCameraKey + ")";
        equipCameraAction.ApplyBindingOverride(0, cameraBinding);

        // View map key
        InputBinding mapBinding = viewMapAction.bindings[0];
        mapBinding.overridePath = "<Keyboard>/#(" + playerKeyBindingsSO.viewMapKey + ")";
        viewMapAction.ApplyBindingOverride(0, mapBinding);
    }

    public void RebindKey(string keyAction)
    {
        switch (keyAction)
        {
            case "Crouch":
                crouchAction.Disable();
                rebindingOperation = crouchAction.PerformInteractiveRebinding()
                                        .WithControlsExcluding("Mouse")
                                        .OnMatchWaitForAnother(0.1f)
                                        .OnComplete(operation => OnRebindComplete("Crouch", crouchAction, rebindingOperation))
                                        .Start();
                break;

            case "Flashlight":
                flashlightAction.Disable();
                rebindingOperation = flashlightAction.PerformInteractiveRebinding()
                                        .WithControlsExcluding("Mouse")
                                        .OnMatchWaitForAnother(0.1f)
                                        .OnComplete(operation => OnRebindComplete("Flashlight", flashlightAction, rebindingOperation))
                                        .Start();
                break;
        }

        // Debug.Log("Rebinding " + keyAction + "key");
    }

    void OnRebindComplete(string keyAction, InputAction inputAction, InputActionRebindingExtensions.RebindingOperation operation)
    {
        operation.Dispose();

        inputAction.Enable();
        
        // Debug.Log("Key rebound to: " + inputAction.GetBindingDisplayString());
    }

    void OnEnable()
    {
        playerController.Enable();
    }

    void OnDestroy()
    {
        playerController.Disable();
    }

    void Update()
    {
        // WASD Movement and Mouse
        if(playerMovement != null) 
        {
            if(toCheckForMovement)
            {
                playerMovement.ReceiveHorizontalInput(horizontalInput);
            }
        }
            
        if(vehicleController != null)
        {
            if(toCheckForVehicleMovement)
            {
                vehicleController.ReceiveHorizontalInput(horizontalInput);
            }
        }
            
        if(cinemachinePOVExtension != null)
        {
            if(toCheckForMouseActivity)
            {
                cinemachinePOVExtension.ReceiveInput(mouseInput);
            }
        }
            
        // Footsteps
        if(playerFootstep != null)
        {
            if(toCheckForMovement)
            {
                playerFootstep.ReceiveInput(horizontalInput);
            }
        }

        // Crosshair
        if(crosshairScaler != null)
        {
            if(toCheckForMovement)
            {
                crosshairScaler.ReceiveHorizontalInput(horizontalInput);
            }
        }

        // Weapon sway
        if(weaponSway != null) weaponSway.ReceiveInput(mouseInput, horizontalInput);
        
        // Camera sway
        if(cameraSway != null) cameraSway.ReceiveInput(mouseInput, horizontalInput);

        // Weapon Attack / Camera Screenshot
        if(toCheckForMouseButtonInput) CheckForMouseButtonInput();

        // Check for increased energy
        if(playerEnergy != null) CheckForIncreasedEnergy();
    }

    void CheckForMouseButtonInput()
    {
        if(Input.GetButtonDown("Fire1"))     // Pressing the 'Fire' key once
        {
            if(weaponHandler.CurrentWeaponGO.activeSelf)
            {
                weaponHandler.Attack("manual");
            }
            else if(!weaponHandler.CurrentWeaponGO.activeSelf && cameraHandler.isActiveAndEnabled)
            {
                cameraHandler.TakeScreenshot(Screen.width, Screen.height);
            }
        }
        
        if(Input.GetButton("Fire1"))    // Holding down the 'Fire' key
        {
            if(weaponHandler.CurrentWeaponGO.activeSelf) weaponHandler.Attack("automatic");
        }
    }

    void CheckForIncreasedEnergy()
    {
        if(Input.GetButton("Shift"))
        {
            playerMovement.IncreaseMovementSpeed();
            playerFootstep.IncreaseFootstepRate();
        }
        else if(Input.GetButtonUp("Shift"))
        {
            playerMovement.RestoreMovementSpeed();
            playerFootstep.RestoreFootstepRate();
        }
    }

    void OnJumpPressed()
    {
        if(playerMovement != null && !playerMovement.IsCrouching) playerMovement.OnJumpPressed();
    }

    void OnCrouchPressed()
    {
        if(playerMovement != null) playerMovement.OnCrouchPressed();
    }

    public void EnablePistolWeaponToggle()
    {
        gameplayControlsActions.Weapon2.performed += _ => weaponHandler.ToggleWeapon(1);
    }
}
