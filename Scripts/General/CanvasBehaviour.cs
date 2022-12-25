using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private bool toDisableCanvasOnStart;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Animator animator;
    
    [Header("Player Input Manager")]
    [SerializeField] private PlayerInputManager inputManager;

    [Header("Player Stats")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Event")]
    [SerializeField] private IntEvent onCoinChangeEvent;

    void Awake()
    {
        if(toDisableCanvasOnStart) DisableCanvas();
    }

    public void EnableCanvas(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            //animator.SetTrigger("canvasIn");
            canvas.gameObject.SetActive(true);

            // Enable cursor, then disable mouse button input and mouse activity
            inputManager.DisplayCursor(true);
            inputManager.toCheckForMouseButtonInput = false;
            inputManager.toCheckForMouseActivity = false;

            // Raise coin event
            onCoinChangeEvent.Raise(playerStats.playerCurrentCoins);
        }
    }

    public void DisableCanvas()
    {
        //animator.SetTrigger("canvasOut");
        canvas.gameObject.SetActive(false);

        // Enable mouse button input and mouse activity, then disable cursor
        inputManager.DisplayCursor(false);
        inputManager.toCheckForMouseButtonInput = true;
        inputManager.toCheckForMouseActivity = true;

        // Raise coin event
        onCoinChangeEvent.Raise(playerStats.playerCurrentCoins);
    }
}
