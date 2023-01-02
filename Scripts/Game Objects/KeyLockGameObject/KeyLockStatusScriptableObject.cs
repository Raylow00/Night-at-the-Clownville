using UnityEngine;

/// <summary>
///     Audio settings to be stored in memory for use in any part of the program
/// </summary>
[CreateAssetMenu(fileName = "KeyLockStatusScriptableObject", menuName = "GameObjects/KeyLockStatus")]
public class KeyLockStatusScriptableObject : ScriptableObject
{
    public bool isLockOpened;
    public bool isKeyObtained;
}
