using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTime;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update(){

        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;

            if(shakeTime <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cineBMCP = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cineBMCP.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time = 0f)
    {
        CinemachineBasicMultiChannelPerlin cineBMCP = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cineBMCP.m_AmplitudeGain = intensity;
        shakeTime = time;
    }
}
