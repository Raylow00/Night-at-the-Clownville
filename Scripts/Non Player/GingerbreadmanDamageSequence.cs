using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GingerbreadmanDamageSequence : MonoBehaviour, IDamageable
{
    [Header("Gingerbreadman puff particles")]
    [SerializeField] private string gingerbreadmanPuffParticlesTag;
    [Header("Audio manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string gingerbreadmanPuffClipName;
    private GameObject gingerbreadmanGO;
    private ObjectPooler objectPooler;
    private string waveSpawnerTag = "WaveSpawner";
    private WaveSpawner waveSpawner;

    void Start()
    {
        objectPooler = ObjectPooler.instance;
        gingerbreadmanGO = transform.GetChild(0).gameObject;
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
            audioManager.PlayAudio(gingerbreadmanPuffClipName);

            // particles
            objectPooler.SpawnFromPool(gingerbreadmanPuffParticlesTag, transform.position, Quaternion.identity);
            gingerbreadmanGO.SetActive(false);

            // wave spawner decrease enemy count in wave
            if(waveSpawner != null) waveSpawner.DecreaseEnemyCountInWave();

            Destroy(gameObject, 0.3f);
        }
    }

    public void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitNormalbool, bool isMeleeWeapon=false, bool toBroadcast=true)
    {
        // audio
        audioManager.PlayAudio(gingerbreadmanPuffClipName);

        // particles
        objectPooler.SpawnFromPool(gingerbreadmanPuffParticlesTag, transform.position, Quaternion.identity);
        gingerbreadmanGO.SetActive(false);

        // wave spawner decrease enemy count in wave
        if (waveSpawner != null) waveSpawner.DecreaseEnemyCountInWave();

        Destroy(gameObject, 0.3f);
    }
}
