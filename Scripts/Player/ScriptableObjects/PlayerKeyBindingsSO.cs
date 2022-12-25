using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerKeyBindings", menuName = "Player/KeyBindings")]
public class PlayerKeyBindingsSO : ScriptableObject
{
    [Range(1f, 30f)]
    public float mouseSensitivityX = 8f;
    [Range(1f, 30f)]
    public float mouseSensitivityY = 5f;
    public float fieldOfView = 60f;
    public string crouchKey;
    public string flashlightKey;
    public string switchLastUsedWeaponKey;
    public string switchWeaponSkinKey;
    public string viewMapKey;
    public string equipCameraKey;
}
