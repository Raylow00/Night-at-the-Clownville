using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairColorModifier : MonoBehaviour
{
    [SerializeField] private VoidEvent onEnemyDetectedEvent;
    [SerializeField] private VoidEvent onEnemyExitDetectorEvent;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color modifiedColor;
    private Image crosshairImage;

    void Start()
    {
        crosshairImage = GetComponent<Image>();
    }

    public void ModifyCrosshairColor_Detected()
    {
        crosshairImage.color = modifiedColor;
    }

    public void ModifyCrosshairColor_Reset()
    {
        crosshairImage.color = originalColor;
    }
}
