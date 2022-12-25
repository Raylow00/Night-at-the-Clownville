using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    [Header("Player Camera")]
    [SerializeField] private bool toDisableCursorOnStart;
    [SerializeField] private Transform playerCameraTransform;
    [Range(60f, 105f)]
    [SerializeField] private float xClamp = 85f;
    private float xRotation = 0f;

    [Header("Sensitivity")]
    [Range(10f, 30f)]
    [SerializeField] private float sensitivityX = 25f;
    [Range(0.01f, 1f)]
    [SerializeField] private float sensitivityY = 0.15f;
    private float mouseX;
    private float mouseY;

    void Start()
    {
        if(toDisableCursorOnStart)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCameraTransform.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }
}
