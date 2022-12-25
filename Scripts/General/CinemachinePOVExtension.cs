using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [Header("Input Manager")]
    private Vector3 startingRotation;

    [Header("Player Camera")]
    [Range(60f, 105f)]
    [SerializeField] private float xClamp = 85f;

    [Header("Mouse Sensitivity")]
    [SerializeField] private PlayerKeyBindingsSO playerKeyPreferences;
    [SerializeField] private FloatEvent onSensitivityChangeEvent;

    [Header("Field of View")]
    [SerializeField] private FloatEvent onFOVChangeEvent;

    private float mouseX;
    private float mouseY;

    void Start()
    {
        ModifySensitivity(playerKeyPreferences.mouseSensitivityY);
        ModifyFOV(playerKeyPreferences.fieldOfView);
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * playerKeyPreferences.mouseSensitivityX;
        mouseY = mouseInput.y * playerKeyPreferences.mouseSensitivityY;
    }

    public void ModifySensitivity(float value)
    {
        playerKeyPreferences.mouseSensitivityX = (float)Mathf.RoundToInt(1.6f * value);
        playerKeyPreferences.mouseSensitivityY = (float)Mathf.RoundToInt(value);
    }

    public void ModifyFOV(float value)
    {
        playerKeyPreferences.fieldOfView = (float)Mathf.RoundToInt(value);

        CinemachineVirtualCamera vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.m_Lens.FieldOfView = playerKeyPreferences.fieldOfView;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = new Vector2(mouseX, mouseY);
                startingRotation.x += deltaInput.x * playerKeyPreferences.mouseSensitivityX * Time.deltaTime;
                startingRotation.y += deltaInput.y * playerKeyPreferences.mouseSensitivityY * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -xClamp, xClamp);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
