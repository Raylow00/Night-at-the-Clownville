using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGameObjectManager : MonoBehaviour
{
    [SerializeField] private KeyLockStatusScriptableObject keyLockStatusSO;
    [SerializeField] private VoidEvent VoidEvent_OnKeyObtainEvent;

    private KeyGameObject keyGameObject;

    void Start()
    {
        keyGameObject = new KeyGameObject();
    }

    public KeyLockStatusScriptableObject GetScriptableObject()
    {
        return keyLockStatusSO;
    }

    public bool GetIsKeyObtained()
    {
        return keyLockStatusSO.isKeyObtained;
    }

    public void ObtainKey()
    {
        keyLockStatusSO.isKeyObtained = keyGameObject.ObtainKey();

        VoidEvent_OnKeyObtainEvent.Raise();
    }
}
