using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [Header("Torchlight")]
    [SerializeField] private Light flashlight;
    [SerializeField] private float maxBatteryLevel = 100f;

    [Header("Depleting")]
    [Range(0.01f, 1f)]
    [SerializeField] private float depletionRate = 0.7f;

    [Header("Recharging")]
    [Range(0.01f, 2f)]
    [SerializeField] private float rechargeRate = 1.3f;

    [Header("Event")]
    [SerializeField] private FloatEvent onBatteryChangeEvent;
    
    [Header("SFX")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string flashlightClipName;

    [Header("Enemy Detection")]
    [SerializeField] private LayerMask interactableLayers;

    private float batteryLevel;
    private float batteryPercentage;
    private bool isDepleted;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;

        batteryLevel = maxBatteryLevel;
        batteryPercentage = CalculateBatteryPercentage(batteryLevel, maxBatteryLevel);
        flashlight.enabled = false;

        onBatteryChangeEvent.Raise(batteryPercentage);
    }

    void Update()
    {
        if(flashlight.enabled)
        {
            // Depletes battery
            batteryLevel -= Time.deltaTime * depletionRate;
            batteryPercentage = CalculateBatteryPercentage(batteryLevel, maxBatteryLevel);

            onBatteryChangeEvent.Raise(Mathf.Floor(batteryPercentage));
            // Debug.Log("Depleting: " + batteryPercentage);

            if(batteryLevel <= 0f)
            {
                DeactivateTorchlight();
                isDepleted = true;
                batteryLevel = 0f;
                batteryPercentage = CalculateBatteryPercentage(batteryLevel, maxBatteryLevel);
                onBatteryChangeEvent.Raise(batteryPercentage);
            }
        }

        if(!flashlight.enabled && batteryLevel < maxBatteryLevel)
        {
            // Recharges battery
            batteryLevel += Time.deltaTime * rechargeRate;
            batteryPercentage = CalculateBatteryPercentage(batteryLevel, maxBatteryLevel);

            onBatteryChangeEvent.Raise(Mathf.Floor(batteryPercentage));
            // Debug.Log("Recharging: " + batteryPercentage);

            if(batteryLevel > maxBatteryLevel)      // Only after the battery is done charging, isDepleted is set to false
            {
                isDepleted = false;                 // To avoid player from turning it on when it is charging
                batteryLevel = maxBatteryLevel;
            }
        } 
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.layer == 7)
        {
            if(flashlight.isActiveAndEnabled)
            {
                // Debug.Log("Enemy: " + col.gameObject.name + " detected");
                //col.gameObject.GetComponent<IAttackable>().RaiseTrigger(once=true);
            }
        }
    }

    public void OnTorchPressed()
    {
        if(!flashlight.enabled && !isDepleted)
        {
            ActivateTorchlight();
        }
        else if(flashlight.enabled)
        {
            DeactivateTorchlight();
        }
    }

    public void ActivateTorchlight()
    {
        // Debug.Log("Activating flashlight");

        flashlight.enabled = true;

        audioManager.PlayAudio(flashlightClipName);
    }

    public void DeactivateTorchlight()
    {
        // Debug.Log("Deactivating flashlight");

        flashlight.enabled = false;

        audioManager.PlayAudio(flashlightClipName);
    }

    private void DetectObjectInFront()
    {
        RaycastHit hit;
        bool isHitDetected = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, interactableLayers);
        if(isHitDetected)
        {
            // Debug.Log(hit.collider.gameObject.name);
        }
    }

    private float CalculateBatteryPercentage(float level, float maxLevel)
    {
        float percentage = level / maxLevel * 100f;

        return percentage;
    }
}
