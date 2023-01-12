using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    #region Serialized Fields
    //[SerializeField] private GameplaySettingsScriptableObject gameplaySettingsSO;
    #endregion

    #region Private Fields
    private Vector3 startingRotation;
    private float mouseX;
    private float mouseY;
    private float xClamp = 85f;     // Range (60f, 105f)
    #endregion

    public void SetFieldOfView(float arg_value)
    {
        CinemachineVirtualCamera vcam = GetComponent<CinemachineVirtualCamera>();
        //vcam.m_Lens.FieldOfView = playerKeyPreferences.fieldOfView;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = new Vector2(mouseX, mouseY);
                //startingRotation.x += deltaInput.x * playerKeyPreferences.mouseSensitivityX * Time.deltaTime;
                //startingRotation.y += deltaInput.y * playerKeyPreferences.mouseSensitivityY * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -xClamp, xClamp);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
