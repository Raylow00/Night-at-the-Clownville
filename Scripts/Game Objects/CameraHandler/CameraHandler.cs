using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    #region Serialized Fields
    [Header("Names")]
    [SerializeField] private string cameraEquipClipName;
    [SerializeField] private string cameraUnequipClipName;

    [Header("Camera game object")]
    [SerializeField] private GameObject cameraPrefab;

    [Header("Events")]
    [SerializeField] private StringEvent onCameraEquipEvent;
    [SerializeField] private StringEvent onCameraUnequipEvent;
    #endregion

    #region Private Fields
    private bool isCameraEquipped;
    #endregion

    #region Public Methods
    /// <summary>
    ///     Return the camera prefab
    /// </summary>
    /// <returns>
    ///     cameraPrefab
    /// </returns>
    public GameObject GetCameraPrefab()
    {
        return cameraPrefab;
    }

    /// <summary>
    ///     Get whether camera is equipped
    /// </summary>
    /// <returns>
    ///     isCameraEquipped
    /// </returns>
    public bool GetIsCameraEquipped()
    {
        return isCameraEquipped;
    }

    /// <summary>
    ///     Enable/disable camera game object based on input
    /// </summary>
    /// <param name="arg_enable"></param>
    public void EnableCameraGameObject(bool arg_enable)
    {
        cameraPrefab.SetActive(arg_enable);
        isCameraEquipped = arg_enable;

        if (arg_enable != false)
        {
            onCameraEquipEvent.Raise(cameraEquipClipName);
        }
        else
        {
            onCameraUnequipEvent.Raise(cameraUnequipClipName);
        }
    }
    #endregion
}
