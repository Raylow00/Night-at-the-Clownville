using UnityEngine;

/// <summary>
///     Health scriptable object 
/// </summary>
[CreateAssetMenu(fileName = "HealthScriptableObject", menuName = "Health")]
public class HealthScriptableObject : ScriptableObject
{
    public float currHealth;
    public float maxHealth;
    public bool isHealthZero;
}
