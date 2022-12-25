using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverModeHandler : MonoBehaviour, IInteractable
{
    [Header("Shooter Mode Required Components")]
    private PlayerInputManager shooterInputManager;
    private GameObject shooter;
    private GameObject shooterCamera;
    private GameObject crosshairDetectorCanvas;
    
    [Header("Driver Mode Required Components")]
    [SerializeField] private GameObject driverTriggerArea;
    [SerializeField] private GameObject driver;
    [SerializeField] private GameObject driverCamera;
    private Vector3 driverLastPos;
    
    // 0 - Shooter mode
    // 1 - Driver mode
    public int currentMode;

    void Awake()
    {
        currentMode = 0;
    }

    void Start()
    {
        shooter = GameObject.FindWithTag("Player");
        shooterInputManager = shooter.GetComponent<PlayerInputManager>();
        shooterCamera = Camera.main.transform.gameObject;
        crosshairDetectorCanvas = GameObject.FindWithTag("Crosshair").transform.parent.gameObject;
    }

    void Update()
    {
        if(currentMode == 1) CheckIfRollOver();
    }

    public void PressInteractiveButton()
    {
        // Switch to driver mode
        if(currentMode != 1)
        {
            StartCoroutine(SwitchToDriverMode());
        }
        // Switch to shooter mode
        else if(currentMode != 0)
        {
            StartCoroutine(SwitchToShooterMode());
        }
    }

    public void HoldInteractiveButton()
    {
        // Not used
    }

    public void CancelHoldInteractiveButton()
    {
        // not used
    }
    
    IEnumerator SwitchToDriverMode()
    {
        // Debug.Log("Disabling shooter mode");

        // Disable
        shooterCamera.SetActive(false);

        crosshairDetectorCanvas.SetActive(false);

        shooterInputManager.toCheckForMovement = false;

        yield return new WaitForSeconds(0.1f);

        // Debug.Log("Enabling driver mode");

        // Enable
        driver.SetActive(true);

        driverCamera.SetActive(true);

        shooterInputManager.toCheckForVehicleMovement = true;

        currentMode = 1;
    }

    IEnumerator SwitchToShooterMode()
    {
        // Debug.Log("Disabling driver mode");

        // Disable
        driverCamera.SetActive(false);

        driver.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // Debug.Log("Enabling shooter mode");

        shooterCamera.SetActive(true);

        shooter.SetActive(true);

        shooter.GetComponent<CharacterController>().enabled = false;

        crosshairDetectorCanvas.SetActive(true);

        driverLastPos = driver.transform.position;

        shooterInputManager.toCheckForVehicleMovement = false;

        yield return new WaitForSeconds(0.1f);

        driverTriggerArea.transform.position = driverLastPos;

        shooter.transform.position = driverLastPos + new Vector3(driver.transform.localScale.x, 0f, driver.transform.localScale.z);
        
        yield return new WaitForSeconds(0.5f);

        shooter.GetComponent<CharacterController>().enabled = true;

        shooterInputManager.enabled = true;

        shooterInputManager.toCheckForMovement = true;

        currentMode = 0;
    }

    private void CheckIfRollOver()
    {
        if(Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 0.125f)
        {
            if(Mathf.Abs(Vector3.Dot(transform.right, Vector3.down)) > 0.825f)
            {
                // Debug.Log("Car is rolled over, switching back to shooter mode now");
                
                StartCoroutine(SwitchToShooterMode());

                driver.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
}
