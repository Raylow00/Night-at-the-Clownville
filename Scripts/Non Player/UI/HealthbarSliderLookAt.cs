using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarSliderLookAt : MonoBehaviour
{
    [SerializeField] private Slider healthbarSlider;
    [SerializeField] private Image healthbarImage;
    [SerializeField] private Color zeroHealthColor;
    [SerializeField] private Color maxHealthColor;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
        SetHealthbar(100);
    }

    void FixedUpdate()
    {
        LookAtPlayer();
        if(healthbarSlider.value <= 0f) Destroy(gameObject);
    }

    private void LookAtPlayer()
    {
        transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward, playerCamera.transform.rotation * Vector3.up);
    }

    public void SetHealthbar(float currentHealth)
    {
        float currentHealthPercentage = CalculateHealthPercentage(currentHealth, 100f);
        healthbarSlider.value = currentHealthPercentage;
        healthbarImage.color = Color.Lerp(zeroHealthColor, maxHealthColor, currentHealthPercentage / 100);  // Divide by 100 again because Lerp takes a number between 0 to 1
    }

    private float CalculateHealthPercentage(float curHealth, float mHealth)
    {
        return ((curHealth / mHealth) * 100);
    }
}
