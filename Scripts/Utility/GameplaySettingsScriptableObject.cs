using UnityEngine;

[CreateAssetMenu(menuName = "Utility/GameplaySettings", fileName = "GameplaySettings")]
public class GameplaySettingsScriptableObject : ScriptableObject
{
    public string default_ReloadKey;
    public string reloadKey;
    public string default_CrouchKey;
    public string crouchKey;
    public string default_FlashlightKey;
    public string flashlightKey;
    public string default_SwitchLastUsedWeaponKey;
    public string switchLastUsedWeaponKey;
    public string default_ViewMapKey;
    public string viewMapKey;
    public string default_EquipCameraKey;
    public string equipCameraKey;

    public float default_MouseSensitivityX;
    public float mouseSensitivityX;
    public float default_MouseSensitivityY;
    public float mouseSensitivityY;

    public float default_FieldOfView;
    public float fieldOfView;
}
