using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollDeath : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] private bool toDieOnStartForTest;

    [Header("Ragdoll Animator")]
    [SerializeField] private Animator animator;

    [Header("Ragdoll Colliders")]
    private Collider[] ragdollColliders;

    [Header("Ragdoll Rigidbodies")]
    private Rigidbody[] ragdollRigidbodies;

    [SerializeField] private float explosionForce;

    void Start()
    {
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();

        ToggleRagdoll(false);

        Vector3 tempHit = new Vector3(0f, 0f, 0f);

        if(toDieOnStartForTest) Die(tempHit);
    }

    public void Die(Vector3 hitNormal)
    {
        // Debug.Log("Ragdoll death working...");

        ToggleRagdoll(true);

        foreach(Rigidbody rb in ragdollRigidbodies)
        {
            rb.AddExplosionForce(explosionForce, transform.position + hitNormal, 5f, 0f, ForceMode.Impulse);
        }
    }

    public void ToggleRagdoll(bool state)
    {
        // If state == false (Turn ragdoll off) --> animator should be on, and vice versa
        animator.enabled = !state;

        // If state == false (Turn ragdoll off) --> rigidbodies should not be dynamic but kinematic instead
        foreach(Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !state;
        }

        // If state == false (Turn ragdoll off) --> colliders should be on
        // To be modified for taking in different attacking colliders
        /* foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = state;
        } */
    }
}
