using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHandler : MonoBehaviour
{
    private GameObject colliderGameObject;
    private bool isColliderHit;
    private HashSet<Collider> colliders = new HashSet<Collider>();
    public HashSet<Collider> GetColliders () { return colliders; }

    void Start(){}

    public void PressButton()
    {
        if(!isColliderHit) return;
        
        colliderGameObject.GetComponent<IInteractable>().PressInteractiveButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other); //hashset automatically handles duplicates

        foreach(Collider collider in colliders)
        {
            if(collider.gameObject.GetComponent<IInteractable>() != null)
            {
                isColliderHit = true;

                colliderGameObject = collider.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);

        if(other.gameObject.GetComponent<IInteractable>() != null)
        {
            isColliderHit = false;
            
            colliderGameObject = null;
        }
    }
}
