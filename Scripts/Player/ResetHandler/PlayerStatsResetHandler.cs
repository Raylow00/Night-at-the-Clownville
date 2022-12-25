using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsResetHandler : MonoBehaviour
{
    [Header("PlayerStatsSO")]
    [SerializeField] private bool resetPlayerStats;
    [SerializeField] private PlayerStatsSO playerStats;

    public void ResetPlayerStats()
    {
        if (resetPlayerStats)
        {
            ResetStats();
        }
    }

    private void ResetStats()
    {
        playerStats.playerPosX = 547.9f;
        playerStats.playerPosY = 2.34f;
        playerStats.playerPosZ = 1546f;

        playerStats.lastSceneName = "";
        playerStats.lastSceneNameSaved = false;

        playerStats.playerMaxHealth = 400;
        playerStats.playerCurrentHealth = 400;

        playerStats.playerMaxEnergy = 200f;
        playerStats.playerCurrentEnergy = 200f;

        playerStats.playerMaxCoins = 0;
        playerStats.playerCurrentCoins = 0;
    }
}
