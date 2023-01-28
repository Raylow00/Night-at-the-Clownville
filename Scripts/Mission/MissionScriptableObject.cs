using UnityEngine;

/// <summary>
///     Mission scriptable object that stores the current state of the mission
/// </summary>
[CreateAssetMenu(fileName = "MissionScriptableObject", menuName = "Missions/Mission")]
public class MissionScriptableObject : ScriptableObject
{
    public MissionState currMissionState = MissionState.MISSION_IDLE;
    public string missionName;
}
