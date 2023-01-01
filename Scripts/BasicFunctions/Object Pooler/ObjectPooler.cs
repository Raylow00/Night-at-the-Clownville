using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<ObjectPool> objectPools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPooler instance;

    void Awake()
    {
        if (instance == null)
        {
            // Use this instance if there is none
            instance = this;
        }
        else if (instance != this)
        {
            // Destroy if there is more than one instance 
            Destroy(this);
        }

        // Keep this in persistent memory when changing scenes
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        InitObjectPool();
    }

    #region Public Methods
    public GameObject SpawmFromPool(string arg_tag, Vector3 arg_spawnPosition, Quaternion arg_spawnRotation)
    {
        // If the tag is not found in the pool dictionary
        // return null
        if (poolDictionary.ContainsKey(arg_tag) == false)
        {
            return null;
        }

        // Set object to spawn to active 
        // Set to required position and rotation
        GameObject objectToSpawn = poolDictionary[arg_tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = arg_spawnPosition;
        objectToSpawn.transform.rotation = arg_spawnRotation;

        return objectToSpawn;
    }
    #endregion

    #region Private Methods
    private void InitObjectPool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (ObjectPool pool in objectPools)
        {
            Queue<GameObject> objectPoolQueueObject = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);

                // Set gameobject to inactive first
                obj.SetActive(false);
                objectPoolQueueObject.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPoolQueueObject);
        }
    }
    #endregion
}
