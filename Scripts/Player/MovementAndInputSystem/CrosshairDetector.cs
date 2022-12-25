using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairDetector : MonoBehaviour
{
    [Header("Layer Mask")]
    [SerializeField] private LayerMask detectableLayers;

    [Header("Interactable Object Detection")]
    [SerializeField] private float detectableDistance = 5f;
    private GameObject interactableGameObject;
    private bool isInteractableColliderHit;
    private bool toStartHoldCountdown;
    private float holdTime = 0f;

    [Header("Events")]
    [SerializeField] private StringEvent onInteractableDetectedEvent;
    [SerializeField] private VoidEvent onInteractableExitDetectorEvent;
    [SerializeField] private FloatEvent onHoldInteractiveButtonHeldEvent;
    [SerializeField] private VoidEvent onHoldInteractiveButtonStartedEvent;
    [SerializeField] private VoidEvent onHoldInteractiveButtonCancelledEvent;
    

    void Start()
    {
        holdTime = 0f;
    }

    void Update()
    {
        RaycastHit hit;

        bool isHitDetected = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, detectableDistance, detectableLayers);

        if(isHitDetected)
        {
            // Interactable layer
            if(hit.collider.gameObject.GetComponent<IHintable>() != null)
            {
                isInteractableColliderHit = true;

                interactableGameObject = hit.collider.gameObject;

                onInteractableDetectedEvent.Raise(hit.collider.gameObject.GetComponent<IHintable>().GetHint());
            }
        }
        else
        {
            // Interactable layer
            isInteractableColliderHit = false;

            interactableGameObject = null;

            toStartHoldCountdown = false;

            onInteractableExitDetectorEvent.Raise();
        }

        if(toStartHoldCountdown)
        {
            print("Starting hold time");
            holdTime += Time.deltaTime;

            if(holdTime > 0.2f)
            {
                print("Enabling hold slider");
                onHoldInteractiveButtonStartedEvent.Raise();
                onHoldInteractiveButtonHeldEvent.Raise(holdTime);
            }
            
            if(holdTime >= 2f)
            {
                print("Disabling hold slider");
                holdTime = 2f;
                onHoldInteractiveButtonHeldEvent.Raise(holdTime);
                toStartHoldCountdown = false;
            }
        }
        else
        {
            holdTime = 0f;
            onHoldInteractiveButtonHeldEvent.Raise(holdTime);
        }
    }

    public void PressInteractiveButton()
    {
        if(!isInteractableColliderHit) return;

        print("Pressed once...");

        toStartHoldCountdown = true;
        interactableGameObject.GetComponent<IInteractable>().PressInteractiveButton();
    }

    public void HoldInteractiveButton()
    {
        if(!isInteractableColliderHit) return;

        print("Holding...");

        toStartHoldCountdown = false;
        interactableGameObject.GetComponent<IInteractable>().HoldInteractiveButton();
    }

    public void CancelHoldInteractiveButton()
    {
        if(!isInteractableColliderHit) return;
        
        print("Cancelling...");
        toStartHoldCountdown = false;
        onHoldInteractiveButtonCancelledEvent.Raise();
        interactableGameObject.GetComponent<IInteractable>().CancelHoldInteractiveButton();
    }
}
