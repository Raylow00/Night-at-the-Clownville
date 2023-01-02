using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGameObjectManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private KeyLockStatusScriptableObject keyLockStatusSO;
    [SerializeField] private VoidEvent VoidEvent_OnLockOpenEvent;
    [SerializeField] private VoidEvent VoidEvent_OnLockCloseEvent;
    #endregion

    #region Private Fields
    private LockGameObject lockGameObject;
    #endregion

    #region Unity MonoBehaviour Methods
    void Start()
    {
        lockGameObject = new LockGameObject();
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
    ///     Get whether lock is opened
    /// </summary>
    /// <returns>
    ///     True if lock is opened
    ///     False otherwise
    /// </returns>
    public bool GetIsLockOpened()
    {
        return keyLockStatusSO.isLockOpened;
    }

    /// <summary>
    ///     Sets lock to open state
    /// </summary>
    public void OpenLock()
    {
        keyLockStatusSO.isLockOpened = lockGameObject.OpenLock();
        VoidEvent_OnLockOpenEvent.Raise();
    }

    /// <summary>
    ///     Sets lock to close state
    ///     and raise an event for this specific lock object
    /// </summary>
    public void CloseLock()
    {
        keyLockStatusSO.isLockOpened = lockGameObject.CloseLock();
        VoidEvent_OnLockCloseEvent.Raise();
    }
    #endregion
}
