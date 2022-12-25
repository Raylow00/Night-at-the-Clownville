using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventTrigger : MonoBehaviour
{
    [SerializeField] private VoidEvent eventToTrigger;

    public void RaiseEvent()
    {
        eventToTrigger.Raise();
    }
}
