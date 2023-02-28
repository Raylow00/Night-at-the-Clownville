using UnityEngine;

[CreateAssetMenu(menuName = "Player/Movement", fileName = "PlayerMovement")]
public class PlayerMovementScriptableObject : ScriptableObject
{
    public float currentHeight;
    public float currentRadius;
    public float currentSpeedFactor;
    public float currentBobbingSpeed;
    public float currentBobbingAmount;
}
