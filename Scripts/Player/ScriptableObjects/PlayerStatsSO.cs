using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats", fileName = "PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Position")]
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;

    [Header("Last Scene Name")]
    public string lastSceneName;
    public bool lastSceneNameSaved = false;

    [Header("Health")]
    public int playerMaxHealth;
    public int playerCurrentHealth;

    [Header("Energy")]
    public float playerMaxEnergy;
    public float playerCurrentEnergy;

    [Header("Coins")]
    public int playerMaxCoins;
    public int playerCurrentCoins;
}
