using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEventTrigger : MonoBehaviour
{
    [SerializeField] private bool toPassOwnCollider;
    [SerializeField] private ColliderEvent onColliderEnterEvent;
    [SerializeField] private ColliderEvent onColliderExitEvent;

    void OnTriggerEnter(Collider col)
    {
        if(onColliderEnterEvent != null)
        {
            if(toPassOwnCollider)
            {
                // Debug.Log("[ColliderEventTrigger] Trigger entered by: " + GetComponent<Collider>().gameObject.name);
                onColliderEnterEvent.Raise(GetComponent<Collider>());
            }
            else
            {
                // Debug.Log("[ColliderEventTrigger] Trigger entered by: " + col.gameObject.name);
                onColliderEnterEvent.Raise(col);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(onColliderExitEvent != null)
        {
            if(toPassOwnCollider)
            {
                // Debug.Log("[ColliderEventTrigger] Trigger exited by: " + GetComponent<Collider>().gameObject.name);
                onColliderExitEvent.Raise(GetComponent<Collider>());
            }
            else
            {
                // Debug.Log("[ColliderEventTrigger] Trigger exited by: " + col.gameObject.name);
                onColliderExitEvent.Raise(col);
            }
        }
    }
}
