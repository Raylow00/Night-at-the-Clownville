using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGetter : MonoBehaviour, IInteractable
{   
    [Header("Get Key Events")]
    [SerializeField] private ColliderEvent onKeyGetEvent;

    public void PressInteractiveButton()
    {
        onKeyGetEvent.Raise(GetComponent<BoxCollider>());
    }

    public void HoldInteractiveButton()
    {
        // Not used
    }

    public void CancelHoldInteractiveButton()
    {
        // not used
    }
}
