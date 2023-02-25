using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableLayers
{
    ENEMY_LAYER,
    BREAKABLE_LAYER
}

public class InteractableHandler : MonoBehaviour
{
    #region Serialized Fields
    [Header("Detectable")]
    [SerializeField] private float detectableDistance;
    [SerializeField] private LayerMask detectableLayers;

    [Header("Events")]
    [SerializeField] private VoidEvent onInteractiveButtonPressEvent;
    [SerializeField] private VoidEvent onInteractiveButtonReleaseEvent;
    [SerializeField] private VoidEvent onInteractiveButtonStartHoldEvent;
    [SerializeField] private FloatEvent onInteractiveButtonHoldEvent;
    [SerializeField] private VoidEvent onInteractableDetectEvent;
    [SerializeField] private VoidEvent onInteractableExitDetectEvent;
    #endregion

    #region Private Fields
    private RaycastHit raycastHit;
    private bool toStartHoldCountdown;
    private float holdTime;
    private GameObject interactableGameObject;
    private bool isInteractableDetected;

    private bool isPressRequested;
    #endregion

    void Update()
    {
        // Process interactable layer 
        bool isHitDetected = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, detectableDistance, detectableLayers);

        if (isHitDetected)
        {
            if (raycastHit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                isInteractableDetected = true;
                interactableGameObject = raycastHit.collider.gameObject;
                onInteractableDetectEvent.Raise();

                if (raycastHit.collider.gameObject.layer == (int)InteractableLayers.ENEMY_LAYER)
                {
                    Debug.Log("Enemy detected");
                }
                else if (raycastHit.collider.gameObject.layer == (int)InteractableLayers.BREAKABLE_LAYER)
                {
                    Debug.Log("Breakable detected");
                }
            }
        }
        else
        {
            isInteractableDetected = false;
            interactableGameObject = null;
            onInteractableExitDetectEvent.Raise();
        }

        // Process button hold time
        if (toStartHoldCountdown)
        {
            print("Starting hold time");
            holdTime += Time.deltaTime;

            if (holdTime > 0.2f)
            {
                print("Enabling hold slider");
                onInteractiveButtonStartHoldEvent.Raise();
                onInteractiveButtonHoldEvent.Raise(holdTime);
            }

            if (holdTime >= 2f)
            {
                print("Disabling hold slider");
                holdTime = 2f;
                onInteractiveButtonHoldEvent.Raise(holdTime);
                toStartHoldCountdown = false;
            }
        }
        else
        {
            holdTime = 0f;
            onInteractiveButtonHoldEvent.Raise(holdTime);
        }
    }

    /// <summary>
    ///     This function will listen to InputManager's press request
    ///     If pressed and interactable object is detected by crosshair 
    ///     from the Update function above,
    ///     - start counting down
    ///     - perform press action from interactable game object
    ///     - raise a presse event
    /// </summary>
    public void PressInteractiveButton()
    {
        if (isInteractableDetected == false)
        {
            return;
        }

        toStartHoldCountdown = true;
        interactableGameObject.GetComponent<IInteractable>().PressInteractiveButton();
        onInteractiveButtonPressEvent.Raise();
    }

    /// <summary>
    ///     This function will listen to InputManager's hold request
    ///     If held down and interactable object is detected by crosshair
    ///     from the Update function above,
    ///     - stop countdown
    ///     - perform hold action from interactable game object
    /// </summary>
    public void HoldInteractiveButton()
    {
        if (isInteractableDetected == false)
        {
            return;
        }

        toStartHoldCountdown = false;
        interactableGameObject.GetComponent<IInteractable>().HoldInteractiveButton();
    }

    /// <summary>
    ///     This function will listen to InputManager's release request
    ///     If released, and if the interactable game object is detected
    ///     by the crosshair in the Update function above,
    ///     - stop countdown
    ///     - perform release action on the interactable game object
    ///     If no interactable game object is detected,
    ///     - stop countdown
    ///     - raise an event
    /// </summary>
    public void ReleaseInteractiveButton()
    {
        if (isInteractableDetected == false)
        {
            toStartHoldCountdown = false;
            onInteractiveButtonReleaseEvent.Raise();
            return;
        }

        toStartHoldCountdown = false;
        interactableGameObject.GetComponent<IInteractable>().ReleaseInteractiveButton();
        onInteractiveButtonReleaseEvent.Raise();
    }
}
