using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergySystem : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Energy Properties")]
    [Range(0f, 1f)] public float energyDepletionRate = 0.2f;
    [Range(0f, 1f)] public float energyRechargeRate = 0.2f;
    

    // [Header("Energy Type")]
    // [SerializeField] private PlayerEnergy.PlayerEnergyType[] energyTypes;
    
    [Header("Events")]
    [SerializeField] private FloatEvent onEnergyChangeEvent;
    [SerializeField] private VoidEvent onEnergyZeroEvent;
    // [SerializeField] private StringEvent onEnergyNotIncreasableEvent;

    public PlayerEnergy playerEnergy;

    void Awake()
    {
        if(playerStats.playerCurrentEnergy != playerStats.playerMaxEnergy)
        {
            playerStats.playerCurrentEnergy = playerStats.playerMaxEnergy;
        }

        playerEnergy = new PlayerEnergy(playerStats.playerCurrentEnergy, playerStats.playerMaxEnergy, onEnergyChangeEvent, onEnergyZeroEvent);
    }

    void Start()
    {
        if(onEnergyChangeEvent != null) onEnergyChangeEvent.Raise(playerEnergy.currentEnergy);
    }

    // public bool CheckIfPurchasable(string id)
    // {
    //     bool purchasable = false;

    //     if(playerEnergy.currentEnergy < playerEnergy.maxEnergy) purchasable = true;
    //     else
    //     {
    //         onEnergyNotIncreasableEvent.Raise("Your current energy is max!");
    //     }


    //     return purchasable;
    // }

    // public void UnlockItem(string id)
    // {
    //     foreach(PlayerEnergy.PlayerEnergyType energyType in energyTypes)
    //     {
    //         if(id == energyType.energyID)
    //         {
    //             playerStats.playerCurrentEnergy = playerEnergy.IncreaseEnergy(energyType.energyAmount);
    //         }
    //     }
    // }
}
