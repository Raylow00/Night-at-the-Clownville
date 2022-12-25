using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This mission class keeps track of:
// whether the crate is triggered ==> Mission STARTS
// whether the coin is picked up ==> Mission is being EXECUTED
// and finally whether the coin is returned to the crate ==> Mission is COMPLETED

public class Mission_1_FindRCoin : Mission
{
    private bool coinPickedUp;

    [Header("MissionSO")]
    [SerializeField] private MissionSO missionData;

    [Header("Blue key game object enabler")]
    [SerializeField] private GameObjectEnabler keyEnabler;

    [Header("Mission Instruction")]
    [SerializeField] private string mission1Instruction;

    [Header("Events")]
    [SerializeField] private VoidEvent onCrateEnteredMission1InitiatedEvent;
    [SerializeField] private StringEvent onMission1InitiatedPublishMessageEvent;
    [SerializeField] private VoidEvent initiateCutSceneEvent;
    [SerializeField] private VoidEvent onMission1CompletedEvent;
    [SerializeField] private VoidEvent onMission1Completed_SpawnEnemyWaveEvent;

    void Awake()
    {
        this.enabled = true;
        
        if(missionData.isCompleted)
        {
            this.enabled = false;
        }
    }

    void Start()
    {
        if(missionData.hasBegun && !missionData.isCompleted)
        {
            base.Initiate();

            onCrateEnteredMission1InitiatedEvent.Raise();

            onMission1InitiatedPublishMessageEvent.Raise(mission1Instruction);

            missionData.hasBegun = true;

            // Debug.Log("[Mission_1_FindRCoin] Mission 1 has begun and continued");
        }
        else if(missionData.hasBegun && missionData.isCompleted)
        {
            base.Complete();
            
            onMission1CompletedEvent.Raise();

            missionData.isCompleted = true;
            
            // Debug.Log("[Mission_1_FindRCoin] Mission 1 complete");
        }

        if(missionData.keyObtained && keyEnabler != null)
        {
            keyEnabler.DisableGameObject();
        }
    }

    public void UpdateMission()
    {
        // Initiate mission and publish message
        if(missionData.hasBegun == false)
        {
            base.Initiate();

            onCrateEnteredMission1InitiatedEvent.Raise();

            onMission1InitiatedPublishMessageEvent.Raise(mission1Instruction);

            initiateCutSceneEvent.Raise();

            missionData.hasBegun = true;

            // Debug.Log("[Mission_1_FindRCoin] Mission 1 started");
        }
        // Publish message when mission has started but not completed
        else if(missionData.hasBegun == true && missionData.isCompleted == false && !coinPickedUp)
        {
            onMission1InitiatedPublishMessageEvent.Raise(mission1Instruction);
        }
        // Complete mission
        else if(missionData.hasBegun && coinPickedUp)
        {
            CompleteMission();
        }
    }

    public void MarkCoinPickedUp()
    {
        if(missionData.isCompleted == false)
        {
            coinPickedUp = true;

            // Debug.Log("[Mission_1_FindRCoin] R Coin picked up");
        }
    }

    public void MarkBlueKeyPickedUp()
    {
        if(missionData.keyObtained == false)
        {
            missionData.keyObtained = true;

            // Debug.Log("[Mission_1_FindRCoin] Blue key picked up");
        }
    }

    private void CompleteMission()
    {
        if(missionData.hasBegun && missionData.isCompleted == false)
        {
            base.Complete();

            // This event, when raised, will
            // - enable blue key
            // - enable r coin in chest
            // - close chest
            // - turn off lights
            onMission1CompletedEvent.Raise();

            if(!missionData.hasSpawnedEnemyWave)
            {
                onMission1Completed_SpawnEnemyWaveEvent.Raise();

                // Debug.Log("[Mission_1_FindRCoin] Enemy wave in Mission 1 spawned");

                missionData.hasSpawnedEnemyWave = true;
            }

            missionData.isCompleted = true;
            
            // Debug.Log("[Mission_1_FindRCoin] Mission 1 complete");
        }
    }
}
