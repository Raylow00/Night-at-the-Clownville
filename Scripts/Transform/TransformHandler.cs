using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHandler : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private TransformScriptableObject transformSO;
    #endregion

    #region Private Fields
    private Transform transform;
    #endregion

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    #region Public Methods
    /// <summary>
    ///     Returns transform's position
    /// </summary>
    /// <returns>
    ///     transform.position
    /// </returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    ///     Returns transform's rotation
    /// </summary>
    /// <returns>
    ///     transform.rotation
    /// </returns>
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    /// <summary>
    ///     Returns player local scale
    /// </summary>
    /// <returns>
    ///     playerTransform.localScale
    /// </returns>
    public Vector3 GetScale()
    {
        return transform.localScale;
    }

    /// <summary>
    ///     Sets the transform's position based on argument 
    /// </summary>
    /// <param name="arg_pos"></param>
    public void SetPosition(Vector3 arg_pos)
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }

        transform.position = arg_pos;

        // Save to scriptable object
        transformSO.positionX = transform.position.x;
        transformSO.positionY = transform.position.y;
        transformSO.positionZ = transform.position.z;
    }

    /// <summary>
    ///     Sets the transform's rotation in Quaternion instead of Vector3
    /// </summary>
    /// <param name="arg_rot"></param>
    public void SetRotation(Quaternion arg_rot)
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }

        transform.rotation = arg_rot;

        // Save to scriptable object
        transformSO.rotationX = transform.rotation.x;
        transformSO.rotationY = transform.rotation.y;
        transformSO.rotationZ = transform.rotation.z;
    }

    /// <summary>
    ///     Sets the transform's local scale instead of global scale
    /// </summary>
    /// <param name="arg_scale"></param>
    public void SetScale(Vector3 arg_scale)
    {
        if (transform == null)
        {
            transform = GetComponent<Transform>();
        }

        transform.localScale = arg_scale;

        // Save to scriptable object
        transformSO.scaleX = transform.localScale.x;
        transformSO.scaleY = transform.localScale.y;
        transformSO.scaleZ = transform.localScale.z;
    }
    #endregion
}
