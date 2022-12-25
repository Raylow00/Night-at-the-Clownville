using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_2_TurnOnPower : Mission
{
    private bool powerTurnedOn;

    [Header("MissionSO")]
    [SerializeField] private MissionSO missionData;
    [Header("Mission 1 Data")]
    [SerializeField] private MissionSO mission1Data;

    [Header("Orange key game object enabler")]
    [SerializeField] private GameObjectEnabler keyEnabler;

    [Header("Lights Manager")]
    [SerializeField] private LightsManager allLightsManager;
    [SerializeField] private LightsManager suicideShootingArenaLightsManager;

    [Header("Fade in/out music")]
    [SerializeField] private AudioSource ambienceMusicAudioSource;
    [SerializeField] private AudioSource enemyWaveAudioSource;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeInTargetVolume;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private float fadeOutTargetVolume;

    [Header("Mission Instruction")]
    [SerializeField] private string mission2Instruction;

    [Header("Events")]
    [SerializeField] private StringEvent onMission2InitiatedPublishMessageEvent;
    [SerializeField] private VoidEvent onMission2CompletedEvent;
    [SerializeField] private VoidEvent onMission2Completed_SpawnEnemyWaveEvent;

    void Start()
    {
        // Update mission 2 data
        if(missionData.hasBegun && !missionData.isCompleted)
        {
            base.Initiate();
            
            missionData.hasBegun = true;

            onMission2InitiatedPublishMessageEvent.Raise(mission2Instruction);

            if(allLightsManager) allLightsManager.TurnOffLights();

            // Debug.Log("[Mission_2_TurnOnPower] Mission 2 has begun and continued");
        }
        else if(missionData.hasBegun && missionData.isCompleted)
        {
            if(allLightsManager) allLightsManager.TurnOffLights();

            if(suicideShootingArenaLightsManager) suicideShootingArenaLightsManager.TurnOnLights();

            base.Complete();
            
            onMission2CompletedEvent.Raise();
            
            missionData.isCompleted = true;
            
            // Debug.Log("[Mission_2_TurnOnPower] Mission 2 complete");
        }

        if(missionData.keyObtained && keyEnabler != null)
        {
            keyEnabler.DisableGameObject();
        }
    }

    public void UpdateMission()
    {
        // Initiate mission and publish message
        if(!missionData.hasBegun && mission1Data.isCompleted)
        {
            base.Initiate();

            missionData.hasBegun = true;

            onMission2InitiatedPublishMessageEvent.Raise(mission2Instruction);

            // Debug.Log("[Mission_2_TurnOnPower] Mission 2 started");
        }
        // Publish message when mission has started but not completed
        else if(missionData.hasBegun == true && missionData.isCompleted == false && !powerTurnedOn)
        {
            onMission2InitiatedPublishMessageEvent.Raise(mission2Instruction);
        }
        // Complete mission
        else if(missionData.hasBegun && powerTurnedOn)
        {
            // Debug.Log("[Mission_2_TurnOnPower] Completing mission");

            CompleteMission();
        } 
    }

    public void MarkPowerTurnedOn()
    {
        if(!missionData.isCompleted)
        {
            powerTurnedOn = true;

            // Debug.Log("[Mission_2_TurnOnPower] Power is turned on");
        }
    }

    public void MarkOrangeKeyPickedUp()
    {
        if(missionData.keyObtained == false)
        {
            missionData.keyObtained = true;

            // Debug.Log("[Mission_2_TurnOnPower] Orange key picked up");
        }
    }

    private void CompleteMission()
    {
        if(missionData.hasBegun && missionData.isCompleted == false)
        {
            base.Complete();

            onMission2CompletedEvent.Raise();

            if(!missionData.hasSpawnedEnemyWave)
            {
                onMission2Completed_SpawnEnemyWaveEvent.Raise();

                // Debug.Log("[Mission_2_TurnOnPower] Enemy wave in Mission 2 spawned");

                missionData.hasSpawnedEnemyWave = true;
            }

            missionData.isCompleted = true;
            
            // Debug.Log("[Mission_2_TurnOnPower] Mission 2 complete");
        }
    }
}
