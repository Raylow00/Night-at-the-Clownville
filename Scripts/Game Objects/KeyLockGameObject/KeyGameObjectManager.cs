using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGameObjectManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private KeyLockStatusScriptableObject keyLockStatusSO;
    [SerializeField] private VoidEvent VoidEvent_OnKeyObtainEvent;
    #endregion

    #region Private Fields
    private KeyGameObject keyGameObject;
    #endregion

    #region Unity MonoBehaviour Methods
    void Start()
    {
        keyGameObject = new KeyGameObject();
    }
    #endregion

    #region Public Methods
    /// <summary>
    ///     Gets the scriptable object so it can be tested that the data is stored back 
    /// </summary>
    /// <returns>
    ///     KeyLockStatusScriptableObject
    /// </returns>
    public KeyLockStatusScriptableObject GetScriptableObject()
    {
        return keyLockStatusSO;
    }

    /// <summary>
    ///     Get whether key is obtained
    /// </summary>
    /// <returns>
    ///     True if key is obtained
    ///     False otherwise
    /// </returns>
    public bool GetIsKeyObtained()
    {
        return keyLockStatusSO.isKeyObtained;
    }

    /// <summary>
    ///     Sets keyObtained to true
    ///     and raise an event for this specific key object
    /// </summary>
    public void ObtainKey()
    {
        keyLockStatusSO.isKeyObtained = keyGameObject.ObtainKey();

        VoidEvent_OnKeyObtainEvent.Raise();
    }
    #endregion
}
