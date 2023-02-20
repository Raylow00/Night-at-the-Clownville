using UnityEngine;

public class FlashlightHandler : MonoBehaviour
{
    #region Serialized Fields
    [Header("Flashlight")]
    [SerializeField] private Light flashlight;

    [Header("Properties")]
    [SerializeField] private bool isAutoRecharge;
    [SerializeField] private float rechargeRate;
    [SerializeField] private float depleteRate;

    [Header("Events")]
    [SerializeField] private VoidEvent onFlashlightToggleEvent;
    [SerializeField] private FloatEvent onFlashlightBatteryChangeEvent;
    #endregion

    #region Private Fields
    private float currentBatteryLevel;
    private float maxBatteryLevel;
    private bool isFlashlightOn;
    private bool isRecharging;
    private bool isRecharged;
    private bool isDepleting;
    private bool isDepleted;
    #endregion

    void Start()
    {
        maxBatteryLevel = 100;
        currentBatteryLevel = maxBatteryLevel;
        isFlashlightOn = false;
        isRecharged = true;
        isRecharging = false;
        isDepleted = false;
        isDepleting = false;
        flashlight.enabled = false;
    }

    void Update()
    {
        if (flashlight.enabled)
        {
            float depleteValue = Time.deltaTime * depleteRate;
            DepleteFlashlight(depleteValue);
        }
        else
        {
            if (isAutoRecharge)
            {
                float rechargeValue = Time.deltaTime * rechargeRate;
                RechargeFlashlight(rechargeValue);
            }
        }
    }

    #region Public Methods
    /// <summary>
    ///     Get whether flashlight is currently recharging
    /// </summary>
    /// <returns>
    ///     isRecharging
    /// </returns>
    public bool GetIsFlashlightRecharging()
    {
        return isRecharging;
    }

    /// <summary>
    ///     Get whether flashlight is currently depleting
    /// </summary>
    /// <returns>
    ///     isDepleting
    /// </returns>
    public bool GetIsFlashlightDepleting()
    {
        return isDepleting;
    }

    /// <summary>
    ///     Get the current battery level
    /// </summary>
    /// <returns>
    ///     currentBatteryLevel
    /// </returns>
    public float GetCurrentBatteryLevel()
    {
        return currentBatteryLevel;
    }

    /// <summary>
    ///     Get whether flashlight is currently on
    /// </summary>
    /// <returns>
    ///     isFlashlightOn
    /// </returns>
    public bool GetIsFlashlightOn()
    {
        return isFlashlightOn;
    }

    /// <summary>
    ///     Get whether flashlight is fully recharged
    /// </summary>
    /// <returns>
    ///     isRecharged
    /// </returns>
    public bool GetIsFlashlightRecharged()
    {
        return isRecharged;
    }

    /// <summary>
    ///     Get whether flashlight is fully depleted
    /// </summary>
    /// <returns>
    ///     isDepleted
    /// </returns>
    public bool GetIsFlashlightDepleted()
    {
        return isDepleted;
    }

    /// <summary>
    ///     Toggle flashlight on or off
    /// </summary>
    /// <param name="arg_onOff"></param>
    public void ToggleFlashlight(bool arg_onOff)
    {
        if (arg_onOff != false)
        {
            flashlight.enabled = true;
            isFlashlightOn = true;
        }
        else
        {
            flashlight.enabled = false;
            isFlashlightOn = false;
        }

        onFlashlightToggleEvent.Raise();
    }

    /// <summary>
    ///     Recharges battery of flashlight
    /// </summary>
    /// <param name="arg_rechargeValue"></param>
    public void RechargeFlashlight(float arg_rechargeValue)
    {
        isRecharging = true;
        isDepleting = false;

        if (isRecharged)
        {
            return;
        }
        else
        {
            currentBatteryLevel += arg_rechargeValue;

            if (currentBatteryLevel >= maxBatteryLevel)
            {
                isRecharged = true;
                isDepleted = false;
                currentBatteryLevel = maxBatteryLevel;
            }

            Debug.Log("Recharging, current level: " + currentBatteryLevel);
        }

        onFlashlightBatteryChangeEvent.Raise(currentBatteryLevel);
    }

    /// <summary>
    ///     Depletes the battery of flashlight
    /// </summary>
    /// <param name="arg_depleteValue"></param>
    public void DepleteFlashlight(float arg_depleteValue)
    {
        isDepleting = true;
        isRecharging = false;

        if (isDepleted)
        {
            return;
        }
        else
        {
            currentBatteryLevel -= arg_depleteValue;

            if (currentBatteryLevel <= 0)
            {
                isDepleted = true;
                isRecharged = false;
                currentBatteryLevel = 0f;
            }

            Debug.Log("Depleting, current level: " + currentBatteryLevel);
        }

        onFlashlightBatteryChangeEvent.Raise(currentBatteryLevel);
    }
    #endregion
}
