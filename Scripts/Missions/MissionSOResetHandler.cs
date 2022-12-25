using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSOResetHandler : MonoBehaviour
{
    [Header("Reset Mission 1")]
    [SerializeField] private bool resetMission1;
    [SerializeField] private MissionSO mission1;

    [Header("Reset Mission 2")]
    [SerializeField] private bool resetMission2;
    [SerializeField] private MissionSO mission2;

    [Header("Reset Mission 3")]
    [SerializeField] private bool resetMission3;
    [SerializeField] private MissionSO mission3;

    public void ResetMissions()
    {
        if (resetMission1)
        {
            ResetMission1();
        }

        if (resetMission2)
        {
            ResetMission2();
        }

        if (resetMission3)
        {
            ResetMission3();
        }
    }

    private void ResetMission1()
    {
        mission1.missionName = "M1_FindRCoin";
        mission1.hasBegun = false;
        mission1.keyObtained = false;
        mission1.lockOpened = false;
        mission1.hasSpawnedEnemyWave = false;
        mission1.isCompleted = false;
        mission1.hasFailed = false;
    }

    private void ResetMission2()
    {
        mission2.missionName = "M2_TurnOnPower";
        mission2.hasBegun = false;
        mission2.keyObtained = false;
        mission2.lockOpened = false;
        mission2.hasSpawnedEnemyWave = false;
        mission2.isCompleted = false;
        mission2.hasFailed = false;
    }

    private void ResetMission3()
    {
        mission3.missionName = "M3_JukeboxInsertRCoin";
        mission3.hasBegun = false;
        mission3.keyObtained = false;
        mission3.lockOpened = false;
        mission3.hasSpawnedEnemyWave = false;
        mission3.isCompleted = false;
        mission3.hasFailed = false;
    }

}
