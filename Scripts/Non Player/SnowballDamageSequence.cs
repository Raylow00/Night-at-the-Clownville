using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballDamageSequence : MonoBehaviour, IDamageable
{
    [Header("Snowball puff particles")]
    [SerializeField] private string snowballPuffParticlesTag;
    [Header("Audio manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string snowballPuffClipName;

    private ObjectPooler objectPooler;
    private GameObject snowballGO;
    private string waveSpawnerTag = "WaveSpawner";
    private WaveSpawner waveSpawner;

    void Start()
    {
        objectPooler = ObjectPooler.instance;
        snowballGO = transform.GetChild(0).gameObject;
        GameObject waveSpawnerGO = GameObject.FindGameObjectWithTag(waveSpawnerTag);
        if (waveSpawnerGO != null)
        {
            waveSpawner = GameObject.FindGameObjectWithTag(waveSpawnerTag).GetComponent<WaveSpawner>();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            // audio
            audioManager.PlayAudio(snowballPuffClipName);

            // particles
            objectPooler.SpawnFromPool(snowballPuffParticlesTag, transform.position, Quaternion.identity);
            snowballGO.SetActive(false);

            // wave spawner decrease enemy count in wave
            if (waveSpawner != null) waveSpawner.DecreaseEnemyCountInWave();

            Destroy(gameObject, 0.3f);
        }
    }

    public void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitNormalbool, bool isMeleeWeapon=false, bool toBroadcast=true)
    {
        // Debug.Log("Snowball taking damage");

        // audio
        audioManager.PlayAudio(snowballPuffClipName);

        // particles
        objectPooler.SpawnFromPool(snowballPuffParticlesTag, transform.position, Quaternion.identity);
        snowballGO.SetActive(false);

        // wave spawner decrease enemy count in wave
        if (waveSpawner != null) waveSpawner.DecreaseEnemyCountInWave();

        Destroy(gameObject, 0.3f);
    }
}
