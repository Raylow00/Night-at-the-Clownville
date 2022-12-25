using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [System.Serializable]
    public class AxleInfo 
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }

    [Header("Vehicle Properties")]
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxSteeringAngle;

    private Vector2 horizontalInput;

    void FixedUpdate()
    {
        float motor = maxMotorTorque * horizontalInput.y;
        float steering = maxSteeringAngle * horizontalInput.x;

        foreach (AxleInfo axleInfo in axleInfos) {

            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

    public void ReceiveHorizontalInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
}
