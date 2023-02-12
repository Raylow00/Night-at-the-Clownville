using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [Header("Enemy Wave")]
    public EnemyWave enemyWave;

    [Header("Center point of wave")]
    [SerializeField] private Transform centerPoint;

    [Header("Size of box")]
    [SerializeField] private Vector3 size;

    [Header("Events")]
    [SerializeField] private VoidEvent onEnemyWaveSpawnEvent;
    [SerializeField] private VoidEvent onEnemyWaveClearEvent;

    private int enemyCount = 0;
    private bool isEnemyWaveCleared = false;

    void OnDrawGizmosSelected()
    {
        // Draw a semi-transparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(centerPoint.position, size);
    }

    #region Public Methods
    public bool GetIsEnemyWaveCleared()
    {
        return isEnemyWaveCleared;
    }

    public EnemyWave GetEnemyWave()
    {
        return enemyWave;
    }

    public void SpawnEnemyWave()
    {
        foreach (EnemyTypeInWave enemyType in enemyWave.enemyTypes)
        {
            for (int i = 0; i < enemyType.numberOfEnemyInWaveToSpawn; i++)
            {
                Vector3 spawnPoint = SpawnRandomPointsInBox();
                GameObject spawnedEnemy = Instantiate(enemyType.enemyPrefab, spawnPoint, Quaternion.identity);
                enemyCount += 1;
                enemyWave.currentNumberOfEnemyInWave = enemyCount;
            }
        }
        
        onEnemyWaveSpawnEvent.Raise();
    }

    public int DecrementEnemyInWave()
    {
        enemyCount -= 1;
        enemyWave.currentNumberOfEnemyInWave = enemyCount;

        if (enemyWave.currentNumberOfEnemyInWave <= 0)
        {
            enemyCount = 0;
            enemyWave.currentNumberOfEnemyInWave = enemyCount;

            isEnemyWaveCleared = true;

            onEnemyWaveClearEvent.Raise();
            return 0;
        }

        return enemyCount;
    }
    #endregion

    #region Private Methods
    private Vector3 SpawnRandomPointsInBox()
    {
        return centerPoint.position + new Vector3(
            (Random.value - 0.5f) * size.x,
            (Random.value - 0.5f) * size.y,
            (Random.value - 0.5f) * size.z
        );
    }
    #endregion
}
