using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossessionSway : MonoBehaviour
{
    [Header("Sway/Horizontal")]
    [Range(0f, 0.5f)]
    [SerializeField] private float swayHorizontalAmount = 0.02f;
    [Range(0f, 0.5f)]
    [SerializeField] private float maxSwayHorizontalAmount = 0.06f;
    [Range(0f, 10f)]
    [SerializeField] private float smoothHorizontalAmount = 6f;
    private Vector3 initialSwayPosition;

    [Header("Tilt/Rotational")]
    [SerializeField] private bool rotateX;
    [SerializeField] private bool rotateY;
    [SerializeField] private bool rotateZ;
    [Range(0f, 10f)]
    [SerializeField] private float tiltRotationalAmount = 4f;
    [Range(0f, 10f)]
    [SerializeField] private float maxTiltRotationalAmount = 5f;
    [Range(0f, 20f)]
    [SerializeField] private float tiltRotationalSmoothAmount = 12f;
    private Quaternion initialTiltRotation;

    [Header("Weapon Bob")]
    [Range(0f, 20f)]
    [SerializeField] private float bobbingSpeed = 15f;
    [Range(0f, 2f)]
    [SerializeField] private float bobbingAmount = 0.01f;
    private float timer;
    private float defaultPosY = 0f;
    private float mouseX;
    private float mouseY;
    private float movementX;
    private float movementY;

    void Start()
    {
        initialSwayPosition = transform.localPosition;
        initialTiltRotation = transform.localRotation;

        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        SwayHorizontal();
        SwayRotational();
    }

    public void ReceiveInput(Vector2 mouseInput, Vector2 horizontalInput)
    {
        mouseX = mouseInput.x;
        mouseY = mouseInput.y;

        movementX = horizontalInput.x;
        movementY = horizontalInput.y;
    }

    public void SwayHorizontal()
    {
        float swayX = - mouseX * swayHorizontalAmount;
        float swayY = - mouseY * swayHorizontalAmount;
        swayX = Mathf.Clamp(swayX, -maxSwayHorizontalAmount, maxSwayHorizontalAmount);
        swayY = Mathf.Clamp(swayY, -maxSwayHorizontalAmount, maxSwayHorizontalAmount);

        Vector3 finalSwayPosition = new Vector3(swayX, swayY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalSwayPosition + initialSwayPosition, Time.deltaTime * smoothHorizontalAmount);

        if(movementX != 0f || movementY != 0f)
        {
            timer += Time.deltaTime * bobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            timer = 0f;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * bobbingSpeed), transform.localPosition.z);
        }
    }

    public void SwayRotational()
    {
        float tiltY = - mouseX * tiltRotationalAmount;
        float tiltX = - mouseY * tiltRotationalAmount;
        tiltY = Mathf.Clamp(tiltY, -maxTiltRotationalAmount, maxTiltRotationalAmount);
        tiltX = Mathf.Clamp(tiltX, -maxTiltRotationalAmount, maxTiltRotationalAmount);

        Quaternion finalSwayRotation = Quaternion.Euler(new Vector3(
            rotateX ? -tiltX : 0,
            rotateY ? tiltY : 0, 
            rotateZ ? tiltY : 0
            ));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalSwayRotation * initialTiltRotation, Time.deltaTime * tiltRotationalSmoothAmount);
    }
}
