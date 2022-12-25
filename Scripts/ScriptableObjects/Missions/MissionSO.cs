using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Missions/Mission")]
public class MissionSO : ScriptableObject
{
    public string missionName;
    public bool hasBegun;
    public bool keyObtained;
    public bool lockOpened;
    public bool hasSpawnedEnemyWave;
    public bool isCompleted;
    public bool hasFailed;
}