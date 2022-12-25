using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler instance;
    private bool objectsPooled;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }
    #endregion

    void Start()
    {
        objectsPooled = false;

        if (objectsPooled != true) ObjectPoolerInit();
    }

    public GameObject SpawnFromPool(string tag, Vector3 spawnPosition, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            // Debug.LogWarning("[ObjectPooler] The pool with tag " + tag + " doesn't exist.");
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = spawnPosition;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
        // Debug.Log("[ObjectPooler] Enqueueing back object");

        return objectToSpawn;
    }

    public void ObjectPoolerInit()
    {
        // Debug.Log("[ObjectPooler] Spawning particles and deactivating them first");

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i=0; i< pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public bool GetBoolObjectsPooled()
    {
        return objectsPooled;
    }
}
