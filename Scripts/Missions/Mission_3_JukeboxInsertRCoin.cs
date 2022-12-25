using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_3_JukeboxInsertRCoin : Mission, IInteractable
{
    [Header("Mission 1 Data")]
    [SerializeField] private MissionSO mission1Data;
    [Header("Mission 2 Data")]
    [SerializeField] private MissionSO mission2Data;

    [Header("Mission 3 Data")]
    [SerializeField] private MissionSO mission3Data;
    private bool coinInserted;

    [Header("Hintable component")]
    [SerializeField] private Hintable hintable;

    [Header("Red key game object enabler")]
    [SerializeField] private GameObjectEnabler keyEnabler;

    [Header("Lock Animator")]
    [SerializeField] private Animator lockAnimator;

    [Header("Show coin Hand")]
    [SerializeField] private PlayerWeaponHandler weaponHandler;
    [SerializeField] private GameObject showCoinHandGO;

    [Header("Mission Instruction")]
    [SerializeField] private string mission3Instruction;

    [Header("Event")]
    [SerializeField] private VoidEvent onMission3InitiatedEvent;
    [SerializeField] private StringEvent onMission3InitiatedPublishMessageEvent;
    [SerializeField] private VoidEvent onJukeboxInsertedRCoinEvent;
    [SerializeField] private VoidEvent onMission3CompletedEvent;
    [SerializeField] private VoidEvent onMission3Completed_SpawnEnemyWaveEvent;
    [SerializeField] private StringEvent onMission1IncompleteEvent;

    void Start()
    {
        if(mission3Data.hasBegun && !mission3Data.isCompleted)
        {
            base.Initiate();
            onMission3InitiatedEvent.Raise();
            onMission3InitiatedPublishMessageEvent.Raise(mission3Instruction);
            mission3Data.hasBegun = true;

            // Debug.Log("[Mission_3_JukeboxInsertRCoin] Mission 3 has begun and continued");
        }
        else if(mission3Data.hasBegun && mission3Data.isCompleted)
        {
            base.Complete();
            onMission3CompletedEvent.Raise();
            mission3Data.isCompleted = true;

            // Debug.Log("[Mission_3_JukeboxInsertRCoin] Mission 3 complete");
        }

        if(mission3Data.keyObtained && keyEnabler != null)
        {
            keyEnabler.DisableGameObject();
        }
    }

    public void UpdateMission()
    {
        // Initiate mission and publish message
        if(!mission3Data.hasBegun && mission1Data.isCompleted && mission2Data.isCompleted)
        {
            base.Initiate();
            onMission3InitiatedEvent.Raise();
            onMission3InitiatedPublishMessageEvent.Raise(mission3Instruction);
            mission3Data.hasBegun = true;
            coinInserted = false;

            // Debug.Log("[Mission_3_JukeboxInsertRCoin] Mission 3 started");
        }
        // Publish message when mission has started but not completed
        else if(mission3Data.hasBegun == true && mission3Data.isCompleted == false)
        {
            onMission3InitiatedPublishMessageEvent.Raise(mission3Instruction);
        }
        // Complete mission
        else if(!mission1Data.isCompleted)
        {
            // Debug.Log("[Mission_3_JukeboxInsertRCoin] Mission 1 incomplete. R Coin not picked up yet.");
            onMission1IncompleteEvent.Raise("You do not have the R coin.");
        }
    }

    public void PressInteractiveButton()
    {
        StartCoroutine(ShowCoinHand());
    }

    public void HoldInteractiveButton()
    {
        if(!mission3Data.hasBegun)
        {
            UpdateMission();
        }
        
        if(mission3Data.hasBegun && !mission3Data.isCompleted)
        {
            // Debug.Log("[Mission_3_JukeboxInsertRCoin] Inserting R Coin into jukebox");
            coinInserted = true;
            MarkJukeboxInsertedRCoin();
            Destroy(hintable);   
        }
        else return;
    }

    public void CancelHoldInteractiveButton()
    {
        showCoinHandGO.SetActive(false);
        weaponHandler.SwitchLastUsedWeapon();
    }

    private IEnumerator ShowCoinHand()
    {
        weaponHandler.UnloadWeapon();

        yield return new WaitForSeconds(1.5f);

        weaponHandler.CurrentWeaponGO.SetActive(false);

        showCoinHandGO.SetActive(true);
    }

    public void MarkRedKeyPickedUp()
    {
        if(mission3Data.keyObtained == false)
        {
            mission3Data.keyObtained = true;

            // Debug.Log("[Mission_3_JukeboxInsertRCoin] Red key picked up");
        }
    }

    public void UnlockThenDisableLock_CompleteMission()
    {
        if(mission3Data.keyObtained)
        {
            mission3Data.lockOpened = true;
            lockAnimator.SetTrigger("unlock_then_disable");

            CompleteMission();
        }
    }

    private void MarkJukeboxInsertedRCoin()
    {
        // Debug.Log("[Mission_3_JukeboxInsertRCoin] Removing hint");
        //int defaultLayer = LayerMask.NameToLayer("Default");
        //gameObject.layer = defaultLayer;
        showCoinHandGO.SetActive(false);
        weaponHandler.SwitchLastUsedWeapon();
        onJukeboxInsertedRCoinEvent.Raise();
    }

    private void CompleteMission()
    {
        base.Complete();

        onMission3CompletedEvent.Raise();

        if(!mission3Data.hasSpawnedEnemyWave)
        {
            onMission3Completed_SpawnEnemyWaveEvent.Raise();

            // Debug.Log("[Mission_3_JukeboxInsertRCoin] Enemy wave in Mission 3 spawned");

            mission3Data.hasSpawnedEnemyWave = true;
        }
        
        mission3Data.isCompleted = true;
        
        // Debug.Log("[Mission_3_JukeboxInsertRCoin] Mission 3 complete");
        
    }
}
