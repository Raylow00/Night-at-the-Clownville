using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy Wave")]
    public EnemyWave[] waves;

    [Header("Event to raise")]
    [SerializeField] private VoidEvent onWaveEliminatedEvent;
    [SerializeField] private VoidEvent onGameEndEvent;

    [Header("Fade in/out music")]
    [SerializeField] private AudioSource ambienceMusicAudioSource;
    [SerializeField] private AudioSource enemyWaveAudioSource;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private float fadeOutTargetVolume;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeInTargetVolume;

    [Header("Mission 3 Data")]
    [SerializeField] private MissionSO mission3Data;

    private int count;

    void OnEnable()
    {
        SpawnWave();
    }

    public void SpawnWave()
    {
        Invoke("FadeAmbienceOutEnemyWaveIn", 5f);

        foreach(EnemyWave wave in waves)
        {
            foreach(EnemyTypeInWave enemy in wave.enemies)
            {
                for(int i=0; i<enemy.noOfEnemiesInWave; i++)
                {
                    Vector3 spawnPoint = enemy.randomSpawnPointGenerator.SpawnRandomPointsInBox();
                    GameObject spawnedEnemy = Instantiate(enemy.enemyPrefab, spawnPoint, Quaternion.identity);
                    count += 1;
                }
            }
        }

        int enemyCount = GetEnemyCountInWave();
        // Debug.Log("[WaveSpawner] Enemy spawned: " + enemyCount);
    }

    public void DecreaseEnemyCountInWave()
    {
        count -= 1;

        if(count <= 0)
        {
            count = 0;
            onWaveEliminatedEvent.Raise();
            // Debug.Log("[WaveSpawner] Wave eliminated");

            if(mission3Data != null && mission3Data.isCompleted)
            {
                FadeAmbienceOutEnemyWaveIn();
                onGameEndEvent.Raise();
            }
        }

        int enemyCount = GetEnemyCountInWave();
        // Debug.Log("[WaveSpawner] Decreasing enemy count, enemy remaining: " + enemyCount);
        return;
    }

    public void FadeAmbienceOutEnemyWaveIn()
    {
        // Fade out background ambience master music
        StartCoroutine(FadeAudioSource.StartFade(ambienceMusicAudioSource, fadeOutDuration, fadeOutTargetVolume));
        // Fade in enemy wave music
        StartCoroutine(FadeAudioSource.StartFade(enemyWaveAudioSource, fadeInDuration, fadeInTargetVolume));
    }

    public void FadeAmbienceInEnemyWaveOut()
    {
        // Fade in background ambience master music
        StartCoroutine(FadeAudioSource.StartFade(ambienceMusicAudioSource, fadeInDuration, fadeInTargetVolume));
        // Fade out enemy wave music
        StartCoroutine(FadeAudioSource.StartFade(enemyWaveAudioSource, fadeOutDuration, fadeOutTargetVolume));
    }

    private int GetEnemyCountInWave()
    {
        return count;
    }
}
