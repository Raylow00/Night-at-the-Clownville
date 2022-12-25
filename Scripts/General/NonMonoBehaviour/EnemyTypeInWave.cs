using UnityEngine;

[System.Serializable]
public class EnemyTypeInWave
{
    public string enemyName;
    public GameObject enemyPrefab;
    public int noOfEnemiesInWave;
    public RandomSpawnPointGenerator randomSpawnPointGenerator;
}
