using UnityEngine;

[CreateAssetMenu(menuName = "Crosshair/Crosshair", fileName = "Crosshair")]
public class CrosshairScriptableObject : ScriptableObject
{
    public float initScale;
    public float maxScale;
    public float scaleSpeed;
}
