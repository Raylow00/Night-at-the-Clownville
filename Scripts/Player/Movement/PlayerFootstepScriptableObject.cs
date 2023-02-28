using UnityEngine;

[CreateAssetMenu(menuName = "Player/Footstep", fileName = "PlayerFootstep")]
public class PlayerFootstepScriptableObject : ScriptableObject
{
    public float walkingFootstepRate;
    public float walkingFootstepCooldown;

    public float runningFootstepRate;
    public float runningFootstepCooldown;

    // TODO: To move this enum to the player movement class
    public enum CurrentGroundType
    {
        WOOD_GROUND,
        CONCRETE_GROUND,
        METAL_GROUND,
        WATER_GROUND,
        MUD_GROUND,
        SNOW_GROUND
    };
}
