using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public string waveName;
    public EnemyTypeInWave[] enemyTypes;
    [HideInInspector]
    public int currentNumberOfEnemyInWave;
}