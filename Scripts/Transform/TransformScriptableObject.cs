using UnityEngine;

[CreateAssetMenu(menuName = "Transform/Transform", fileName = "TransformScriptableObject")]
public class TransformScriptableObject : ScriptableObject
{
    public float positionX;
    public float positionY;
    public float positionZ;

    public float rotationX;
    public float rotationY;
    public float rotationZ;

    public float scaleX;
    public float scaleY;
    public float scaleZ;
}
