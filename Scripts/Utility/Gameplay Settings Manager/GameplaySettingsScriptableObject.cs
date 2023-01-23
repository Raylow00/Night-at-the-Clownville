using UnityEngine;

[CreateAssetMenu(menuName = "Utility/Gameplay/GameplaySettings", fileName = "GameplaySettings")]
public class GameplaySettingsScriptableObject : ScriptableObject
{
    public string default_ReloadKey
    {
        get => "r";
    }
    public string reloadKey;
    public string default_CrouchKey
    {
        get => "c";
    }
    public string crouchKey;
    public string default_FlashlightKey
    {
        get => "f";
    }
    public string flashlightKey;
    public string default_SwitchLastUsedWeaponKey
    {
        get => "q";
    }
    public string switchLastUsedWeaponKey;
    public string default_ViewMapKey
    {
        get => "m";
    }
    public string viewMapKey;
    public string default_EquipCameraKey
    {
        get => "g";
    }
    public string equipCameraKey;

    public float default_MouseSensitivityX
    {
        get => 12.8f;
    }
    public float mouseSensitivityX;
    public float default_MouseSensitivityY
    {
        get => 8f;
    }
    public float mouseSensitivityY;

    public float default_FieldOfView
    {
        get => 60f;
    }
    public float fieldOfView;
    public string saveFolder;
}
