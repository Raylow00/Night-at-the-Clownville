using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGameObjectManager : MonoBehaviour
{
    [SerializeField] private KeyLockStatusScriptableObject keyLockStatusSO;
    [SerializeField] private VoidEvent VoidEvent_OnLockOpenEvent;
    [SerializeField] private VoidEvent VoidEvent_OnLockCloseEvent;

    private LockGameObject lockGameObject;

    void Start()
    {
        lockGameObject = new LockGameObject();
    }

    public KeyLockStatusScriptableObject GetScriptableObject()
    {
        return keyLockStatusSO;
    }

    public bool GetIsLockOpened()
    {
        return keyLockStatusSO.isLockOpened;
    }

    public void OpenLock()
    {
        keyLockStatusSO.isLockOpened = lockGameObject.OpenLock();
        VoidEvent_OnLockOpenEvent.Raise();
    }

    public void CloseLock()
    {
        keyLockStatusSO.isLockOpened = lockGameObject.CloseLock();
        VoidEvent_OnLockCloseEvent.Raise();
    }
}
