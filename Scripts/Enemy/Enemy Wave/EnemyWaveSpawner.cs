using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    #region Serialized Fields
    [Header("Enemy Wave")]
    public EnemyWave enemyWave;

    [Header("Center point of wave")]
    [SerializeField] private Transform centerPoint;

    [Header("Size of box")]
    [SerializeField] private Vector3 size;

    [Header("Events")]
    [SerializeField] private VoidEvent onEnemyWaveSpawnEvent;
    [SerializeField] private VoidEvent onEnemyWaveClearEvent;
    #endregion

    #region Private Fields
    private int enemyCount = 0;
    private bool isEnemyWaveCleared = false;
    #endregion

    void OnDrawGizmosSelected()
    {
        // Draw a semi-transparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(centerPoint.position, size);
    }

    #region Public Methods
    /// <summary>
    ///     Get whether enemy wave is cleared
    /// </summary>
    /// <returns>
    ///     isEnemyWaveCleared
    /// </returns>
    public bool GetIsEnemyWaveCleared()
    {
        return isEnemyWaveCleared;
    }

    /// <summary>
    ///     Returns the enemy wave to get the current number of enemies
    /// </summary>
    /// <returns>
    ///     enemyWave
    /// </returns>
    public EnemyWave GetEnemyWave()
    {
        return enemyWave;
    }

    /// <summary>
    ///     Spawns an enemy wave that can include multiple enemy types
    ///     and keeps track of the number of enemies in the scene
    /// </summary>
    public void SpawnEnemyWave()
    {
        foreach (EnemyTypeInWave enemyType in enemyWave.enemyTypes)
        {
            for (int i = 0; i < enemyType.numberOfEnemyTypeToSpawn; i++)
            {
                Vector3 spawnPoint = SpawnRandomPointsInBox();
                GameObject spawnedEnemy = Instantiate(enemyType.enemyPrefab, spawnPoint, Quaternion.identity);
                enemyCount += 1;
                enemyWave.currentNumberOfEnemyInWave = enemyCount;
            }
        }
        
        onEnemyWaveSpawnEvent.Raise();
    }

    /// <summary>
    ///     Decrement the number of enemy in this wave
    /// </summary>
    /// <returns>
    ///     enemyCount
    /// </returns>
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
    /// <summary>
    ///     Returns a Vector3 point randomly in a box given by a transform
    /// </summary>
    /// <returns>
    ///     Vector3 spawnPoint
    /// </returns>
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
