using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// This class is responsible for updating the ScriptableObject relating to PlayerKeyBindings
/// The actual key bindings update is done in their respective classes like CinemachinePOVExtension, PlayerInputManager
///
public class KeyBindingsManager : MonoBehaviour
{
    public static KeyBindingsManager instance;

    [Header("Player Key Bindings")]
    [SerializeField] private PlayerKeyBindingsSO playerKeyBindingsSO;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    public void SetMouseSensitivity(float value)
    {
        playerKeyBindingsSO.mouseSensitivityX = (float)Mathf.RoundToInt(1.6f * value);
        playerKeyBindingsSO.mouseSensitivityY = (float)Mathf.RoundToInt(value);
    }

    public void SetFieldOfView(float value)
    {
        playerKeyBindingsSO.fieldOfView = (float)Mathf.RoundToInt(value);
    }

    public void SetFlashlightKey(string key)
    {
        playerKeyBindingsSO.flashlightKey = key.ToUpper();
    }

    public void SetCrouchKey(string key)
    {
        playerKeyBindingsSO.crouchKey = key.ToUpper();
    }

    public void SetLastWeaponKey(string key)
    {
        playerKeyBindingsSO.switchLastUsedWeaponKey = key.ToUpper();
    }

    public void SetWeaponSkinKey(string key)
    {
        playerKeyBindingsSO.switchWeaponSkinKey = key.ToUpper();
    }

    public void SetCameraEquipKey(string key)
    {
        playerKeyBindingsSO.equipCameraKey = key.ToUpper();
    }

    public void SetMapViewKey(string key)
    {
        playerKeyBindingsSO.viewMapKey = key.ToUpper();
    }
}
