using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IBreakable, IInteractable
{
    [Header("Broken Object")]
    [SerializeField] private GameObject brokenVersion;

    public void PressInteractiveButton()
    {
        // Not used
    }

    public void HoldInteractiveButton()
    {
        BreakObject();
    }

    public void CancelHoldInteractiveButton()
    {
        // not used
    }

    public void BreakObject()
    {
        Instantiate(brokenVersion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
