using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplaySettingsManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private GameplaySettingsScriptableObject gameplaySettingsSO;
    [SerializeField] private StringEvent onReloadKeyRemapEvent;
    [SerializeField] private StringEvent onCrouchKeyRemapEvent;
    [SerializeField] private StringEvent onFlashlightKeyRemapEvent;
    [SerializeField] private StringEvent onSwitchLastUsedWeaponKeyRemapEvent;
    [SerializeField] private StringEvent onViewMapKeyRemapEvent;
    [SerializeField] private StringEvent onEquipCameraKeyRemapEvent;
    [SerializeField] private FloatEvent onMouseSensitivityChangeEvent;
    [SerializeField] private FloatEvent onFieldOfViewChangeEvent;
    #endregion

    #region Private Fields
    private PlayerController playerController;
    private PlayerController.GameplayControlsActions gameplayControlsActions;
    private InputAction reloadAction;
    private InputAction crouchAction;
    private InputAction flashlightAction;
    private InputAction switchLastUsedWeaponAction;
    private InputAction equipCameraAction;
    private InputAction viewMapAction;
    //private InputAction pauseAction;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private static GameplaySettingsManager instance;
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);

        playerController = new PlayerController();
        gameplayControlsActions = playerController.GameplayControls;
        reloadAction = gameplayControlsActions.Reload;
        crouchAction = gameplayControlsActions.Crouch;
        flashlightAction = gameplayControlsActions.Flashlight;
        switchLastUsedWeaponAction = gameplayControlsActions.SwitchEquipment;
        equipCameraAction = gameplayControlsActions.Camera;
        viewMapAction = gameplayControlsActions.UseMap;

        InitDefaultGameplaySettings();
        AssignActionsToKeys();
    }

    void OnEnable()
    {
        playerController.Enable();
    }

    void OnDestroy()
    {
        playerController.Disable();
    }

    #region Public Methods
    /// <summary>
    ///     Get the scriptable object GameplaySettingsScriptableObject
    /// </summary>
    /// <returns>
    ///     GameplaySettingsScriptableObject
    /// </returns>
    public GameplaySettingsScriptableObject GetScriptableObject()
    {
        return gameplaySettingsSO;
    }

    /// <summary>
    ///     Remaps the reload key by sending the old key + new key
    /// </summary>
    /// <param name="arg_newKey"></param>
    public void RemapReloadKey(string arg_newKey)
    {
        string keyData = gameplaySettingsSO.reloadKey + arg_newKey;
        OverrideKeyBindings(keyData);
        
        // Send out the old + new key
        onReloadKeyRemapEvent.Raise(keyData);
    }

    /// <summary>
    ///     Remaps the crouch key by sending the old key + new key
    /// </summary>
    /// <param name="arg_newKey"></param>
    public void RemapCrouchKey(string arg_newKey)
    {
        string keyData = gameplaySettingsSO.crouchKey + arg_newKey;
        OverrideKeyBindings(keyData);

        // Send out the old + new key
        onCrouchKeyRemapEvent.Raise(keyData);
    }

    /// <summary>
    ///     Remaps the flashlight key by sending the old key + new key
    /// </summary>
    /// <param name="arg_newKey"></param>
    public void RemapFlashlightKey(string arg_newKey)
    {
        string keyData = gameplaySettingsSO.flashlightKey + arg_newKey;
        OverrideKeyBindings(keyData);

        // Send out the old + new key
        onFlashlightKeyRemapEvent.Raise(keyData);
    }

    /// <summary>
    ///     Remaps the switch last used weapon key by sending the old key + new key
    /// </summary>
    /// <param name="arg_newKey"></param>
    public void RemapSwitchLastUsedWeaponKey(string arg_newKey)
    {
        string keyData = gameplaySettingsSO.switchLastUsedWeaponKey + arg_newKey;
        OverrideKeyBindings(keyData);
    }

    /// <summary>
    ///     Remaps the view map key by sending the old key + new key
    /// </summary>
    /// <param name="arg_newKey"></param>
    public void RemapViewMapKey(string arg_newKey)
    {
        string keyData = gameplaySettingsSO.viewMapKey + arg_newKey;
        OverrideKeyBindings(keyData);

        // Send out the old + new key
        onViewMapKeyRemapEvent.Raise(keyData);
    }

    /// <summary>
    ///     Remaps the equip camera key by sending the old key + new key
    /// </summary>
    /// <param name="arg_newKey"></param>
    public void RemapEquipCameraKey(string arg_newKey)
    {
        string keyData = gameplaySettingsSO.equipCameraKey + arg_newKey;
        OverrideKeyBindings(keyData);

        // Send out the old + new key
        onEquipCameraKeyRemapEvent.Raise(keyData);
    }

    /// <summary>
    ///     Sets the mouse sensitivity of X and Y axis using the input value from player
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetMouseSensitivity(float arg_value)
    {
        gameplaySettingsSO.mouseSensitivityX = 1.6f * arg_value;
        gameplaySettingsSO.mouseSensitivityY = arg_value;

        // Send out the new mouse sensitivity value
        onMouseSensitivityChangeEvent.Raise(arg_value);
    }

    /// <summary>
    ///     Sets the field of view of camera using the input value from player
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetFieldOfView(float arg_value)
    {
        gameplaySettingsSO.fieldOfView = arg_value;

        // Send out the new field of view
        onFieldOfViewChangeEvent.Raise(arg_value);
    }

    /// <summary>
    ///     Save gameplay settings by serializing into JSON format
    ///     TODO: To raise question whether to overwrite saved file
    /// </summary>
    /// <param name="arg_saveDir"></param>
    /// <returns></returns>
    public void SaveGameplaySettings(string arg_saveDir)
    {
        string saveDir = arg_saveDir + "/playerGameplaySettings";
        string[] tempSplit = arg_saveDir.Split('/');
        gameplaySettingsSO.saveFolder = tempSplit[tempSplit.Length - 2];
        string saveFilePath = saveDir + "/gameplaySettingsSO.txt";

        BinaryFormatter bf = new BinaryFormatter();

        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }

        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }

        // Serialize Video Settings data
        FileStream file = File.Create(saveFilePath);
        var json = JsonUtility.ToJson(gameplaySettingsSO);
        bf.Serialize(file, json);
        file.Close();
    }

    /// <summary>
    ///     Load gameplay settings into the existing scriptable object
    /// </summary>
    /// <param name="arg_savedSlotName"></param>
    /// <returns></returns>
    public bool LoadGameplaySettings(string arg_savedSlotName)
    {
        Debug.Log("Save folder name: " + gameplaySettingsSO.saveFolder);

        BinaryFormatter bf = new BinaryFormatter();

        string savedFile = Application.persistentDataPath + "/" + gameplaySettingsSO.saveFolder + "/" + arg_savedSlotName + "/playerGameplaySettings/gameplaySettingsSO.txt";
        if (File.Exists(savedFile))
        {
            FileStream file = File.Open(savedFile, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), gameplaySettingsSO);

            return true;
        }
        else
        {
            Debug.LogError("Error opening: " + savedFile + "\nNo such file exists!");
            return false;
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Initialize all keys to the default values in the GameplaySettingsScriptableObject
    /// </summary>
    private void InitDefaultGameplaySettings()
    {
        gameplaySettingsSO.reloadKey = gameplaySettingsSO.default_ReloadKey;
        gameplaySettingsSO.crouchKey = gameplaySettingsSO.default_CrouchKey;
        gameplaySettingsSO.flashlightKey = gameplaySettingsSO.default_FlashlightKey;
        gameplaySettingsSO.switchLastUsedWeaponKey = gameplaySettingsSO.default_SwitchLastUsedWeaponKey;
        gameplaySettingsSO.viewMapKey = gameplaySettingsSO.default_ViewMapKey;
        gameplaySettingsSO.equipCameraKey = gameplaySettingsSO.default_EquipCameraKey;

        gameplaySettingsSO.mouseSensitivityX = gameplaySettingsSO.default_MouseSensitivityX;
        gameplaySettingsSO.mouseSensitivityY = gameplaySettingsSO.default_MouseSensitivityY;

        gameplaySettingsSO.fieldOfView = gameplaySettingsSO.default_FieldOfView;

        // Reload key
        InputBinding reloadBinding = reloadAction.bindings[0];
        reloadBinding.overridePath = "<Keyboard>/#(" + gameplaySettingsSO.reloadKey + ")";
        reloadAction.ApplyBindingOverride(0, reloadBinding);

        // Crouch key
        InputBinding crouchBinding = crouchAction.bindings[0];
        crouchBinding.overridePath = "<Keyboard>/#(" + gameplaySettingsSO.crouchKey + ")";
        crouchAction.ApplyBindingOverride(0, crouchBinding);

        // Flashlight key
        InputBinding flashlightBinding = flashlightAction.bindings[0];
        flashlightBinding.overridePath = "<Keyboard>/#(" + gameplaySettingsSO.flashlightKey + ")";
        flashlightAction.ApplyBindingOverride(0, flashlightBinding);

        // Switch last used weapon key
        InputBinding lastUsedWeaponBinding = switchLastUsedWeaponAction.bindings[0];
        lastUsedWeaponBinding.overridePath = "<Keyboard>/#(" + gameplaySettingsSO.switchLastUsedWeaponKey + ")";
        switchLastUsedWeaponAction.ApplyBindingOverride(0, lastUsedWeaponBinding);

        // View map key
        InputBinding mapBinding = viewMapAction.bindings[0];
        mapBinding.overridePath = "<Keyboard>/#(" + gameplaySettingsSO.viewMapKey + ")";
        viewMapAction.ApplyBindingOverride(0, mapBinding);

        // Equip camera key
        InputBinding cameraBinding = equipCameraAction.bindings[0];
        cameraBinding.overridePath = "<Keyboard>/#(" + gameplaySettingsSO.equipCameraKey + ")";
        equipCameraAction.ApplyBindingOverride(0, cameraBinding);
    }

    /// <summary>
    ///     Assign each function to each action
    /// </summary>
    private void AssignActionsToKeys()
    {
        gameplayControlsActions.Reload.performed += _ => OnReloadPressed();
        gameplayControlsActions.Crouch.performed += _ => OnCrouchPressed();
        gameplayControlsActions.Flashlight.performed += _ => OnFlashlightPressed();
        gameplayControlsActions.SwitchEquipment.performed += _ => OnSwitchLastUsedWeaponPressed();
        gameplayControlsActions.Camera.performed += _ => OnEquipCameraPressed();
        gameplayControlsActions.UseMap.performed += _ => OnUseMapPressed();
    }

    /// <summary>
    ///     Handles the rebinding by setting the override path of each action binding
    ///     The commented out code refers to the interactive binding during runtime.
    ///     It is commented out and not used so that the input from players can be taken from the UI input field 
    ///     instead of the real-time input.
    /// </summary>
    /// <param name="arg_keyData"></param>
    private void OverrideKeyBindings(string arg_keyData)
    {
        // Input will be the original character followed by character to switch to
        // For example, "CF"
        // "C" - original key for crouching
        // "F" - key to switch to for crouching
        string originalKey = arg_keyData.Substring(0, 1);
        string newKey = arg_keyData.Substring(1, 1);

        switch (originalKey)
        {
            case "r":
                reloadAction.Disable();
                //rebindingOperation = reloadAction.PerformInteractiveRebinding()
                //                        .WithControlsExcluding("Mouse")
                //                        .OnMatchWaitForAnother(0.1f)
                //                        .OnComplete(operation => OnRebindComplete("Reload", reloadAction, rebindingOperation))
                //                        .Start();
                InputBinding reloadBinding = reloadAction.bindings[0];
                reloadBinding.overridePath = "<Keyboard>/#(" + newKey + ")";
                reloadAction.ApplyBindingOverride(0, reloadBinding);
                reloadAction.Enable();

                // Assign to gameplay settings SO after rebinding
                gameplaySettingsSO.reloadKey = newKey;

                // Send out event
                onReloadKeyRemapEvent.Raise(gameplaySettingsSO.reloadKey);

                break;
            case "c":
                crouchAction.Disable();
                //rebindingOperation = crouchAction.PerformInteractiveRebinding()
                //                        .WithControlsExcluding("Mouse")
                //                        .WithControlsExcluding("<Keyboard>/Enter")
                //                        .WithControlsExcluding("<Keyboard>/numpadEnter")
                //                        .OnMatchWaitForAnother(0.1f)
                //                        .OnComplete(operation => OnRebindComplete("Crouch", crouchAction, rebindingOperation))
                //                        .Start();
                InputBinding crouchBinding = crouchAction.bindings[0];
                crouchBinding.overridePath = "<Keyboard>/#(" + newKey + ")";
                crouchAction.ApplyBindingOverride(0, crouchBinding);
                crouchAction.Enable();

                // Assign to gameplay settings SO after rebinding
                gameplaySettingsSO.crouchKey = newKey;

                // Send out event
                onCrouchKeyRemapEvent.Raise(gameplaySettingsSO.crouchKey);

                break;
            case "f":
                flashlightAction.Disable();
                //rebindingOperation = flashlightAction.PerformInteractiveRebinding()
                //                        .WithControlsExcluding("Mouse")
                //                        .OnMatchWaitForAnother(0.1f)
                //                        .OnComplete(operation => OnRebindComplete("Flashlight", flashlightAction, rebindingOperation))
                //                        .Start();
                InputBinding flashlightBinding = flashlightAction.bindings[0];
                flashlightBinding.overridePath = "<Keyboard>/#(" + newKey + ")";
                flashlightAction.ApplyBindingOverride(0, flashlightBinding);
                flashlightAction.Enable();

                // Assign to gameplay settings SO after rebinding
                gameplaySettingsSO.flashlightKey = newKey;

                // Send out event
                onFlashlightKeyRemapEvent.Raise(gameplaySettingsSO.flashlightKey);

                break;
            case "q":
                switchLastUsedWeaponAction.Disable();
                //rebindingOperation = switchLastUsedWeaponAction.PerformInteractiveRebinding()
                //                        .WithControlsExcluding("Mouse")
                //                        .OnMatchWaitForAnother(0.1f)
                //                        .OnComplete(operation => OnRebindComplete("SwitchLastUsedWeapon", switchLastUsedWeaponAction, rebindingOperation))
                //                        .Start();
                InputBinding lastUsedWeaponBinding = switchLastUsedWeaponAction.bindings[0];
                lastUsedWeaponBinding.overridePath = "<Keyboard>/#(" + newKey + ")";
                switchLastUsedWeaponAction.ApplyBindingOverride(0, lastUsedWeaponBinding);
                switchLastUsedWeaponAction.Enable();

                // Assign to gameplay settings SO after rebinding
                gameplaySettingsSO.switchLastUsedWeaponKey = newKey;

                // Send out event
                onSwitchLastUsedWeaponKeyRemapEvent.Raise(gameplaySettingsSO.switchLastUsedWeaponKey);

                break;
            case "m":
                viewMapAction.Disable();
                //rebindingOperation = viewMapAction.PerformInteractiveRebinding()
                //                        .WithControlsExcluding("Mouse")
                //                        .OnMatchWaitForAnother(0.1f)
                //                        .OnComplete(operation => OnRebindComplete("ViewMap", viewMapAction, rebindingOperation))
                //                        .Start();
                InputBinding mapBinding = viewMapAction.bindings[0];
                mapBinding.overridePath = "<Keyboard>/#(" + newKey + ")";
                viewMapAction.ApplyBindingOverride(0, mapBinding);
                viewMapAction.Enable();

                // Assign to gameplay settings SO after rebinding
                gameplaySettingsSO.viewMapKey = newKey;

                // Send out event
                onViewMapKeyRemapEvent.Raise(gameplaySettingsSO.viewMapKey);

                break;
            case "g":
                equipCameraAction.Disable();
                //rebindingOperation = equipCameraAction.PerformInteractiveRebinding()
                //                        .WithControlsExcluding("Mouse")
                //                        .OnMatchWaitForAnother(0.1f)
                //                        .OnComplete(operation => OnRebindComplete("EquipCamera", equipCameraAction, rebindingOperation))
                //                        .Start();
                InputBinding cameraBinding = equipCameraAction.bindings[0];
                cameraBinding.overridePath = "<Keyboard>/#(" + newKey + ")";
                equipCameraAction.ApplyBindingOverride(0, cameraBinding);
                equipCameraAction.Enable();

                // Assign to gameplay settings SO after rebinding
                gameplaySettingsSO.equipCameraKey = newKey;

                // Send out event
                onEquipCameraKeyRemapEvent.Raise(gameplaySettingsSO.equipCameraKey);

                break;
            default:
                break;
        }
    }

    /// <summary>
    ///     Action to perform when reload is pressed
    /// </summary>
    private void OnReloadPressed()
    {
        Debug.Log("Reload key pressed!");
    }

    /// <summary>
    ///     Action to perform when use map is pressed
    /// </summary>
    private void OnUseMapPressed()
    {
        Debug.Log("Map key pressed!");
    }

    /// <summary>
    ///     Action to perform when equip camera is pressed
    /// </summary>
    private void OnEquipCameraPressed()
    {
        Debug.Log("Camera key pressed!");
    }

    /// <summary>
    ///     Action to perform when switch last used weapon is pressed
    /// </summary>
    private void OnSwitchLastUsedWeaponPressed()
    {
        Debug.Log("Switch last used weapon key pressed!");
    }

    /// <summary>
    ///     Action to perform when crouch is pressed
    /// </summary>
    private void OnCrouchPressed()
    {
        Debug.Log("Crouch key pressed!");
    }

    /// <summary>
    ///     Action to perform when flashlight is pressed
    /// </summary>
    private void OnFlashlightPressed()
    {
        Debug.Log("Flashlight key pressed!");
    }

    /// <summary>
    ///     Unused callback function for dynamic rebinding of keys 
    ///     Referenced in function OverrideKeyBindings()
    /// </summary>
    /// <param name="keyAction"></param>
    /// <param name="inputAction"></param>
    /// <param name="operation"></param>
    private void OnRebindComplete(string keyAction, InputAction inputAction, InputActionRebindingExtensions.RebindingOperation operation)
    {
        operation.Dispose();
        inputAction.Enable();;
    }
    #endregion
}
