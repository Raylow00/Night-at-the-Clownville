using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class Unittest_ObjectPooler
{
    private GameObject testObjectPoolerGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_ObjectPooler_InitPool()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        //<-------------------------------- Test Setup ---------------------------------->//
        testObjectPoolerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/ObjectPooler/Test_Object_Pooler"));
        ObjectPooler testObjectPooler = testObjectPoolerGameObject.GetComponent<ObjectPooler>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        int EXP_objectPoolCount = 1;
        Assert.IsTrue(testObjectPooler.poolDictionary.Count == EXP_objectPoolCount);

        //<-------------------------------- Test TearDown ------------------------------->//
        Object.DestroyImmediate(testObjectPoolerGameObject);
    }

    [UnityTest]
    public IEnumerator Unittest_ObjectPooler_SpawnFromPool()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        //<-------------------------------- Test Setup ---------------------------------->//
        testObjectPoolerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/ObjectPooler/Test_Object_Pooler"));
        ObjectPooler testObjectPooler = testObjectPoolerGameObject.GetComponent<ObjectPooler>();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//

        int EXP_objectPoolCount = 1;
        Assert.IsTrue(testObjectPooler.poolDictionary.Count == EXP_objectPoolCount);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        Vector3 spawnPosition = Vector3.zero;
        Quaternion spawnRotation = Quaternion.identity;
        GameObject OUT_objectSpawned;
        OUT_objectSpawned = testObjectPooler.SpawmFromPool("TestObjectPool", spawnPosition, spawnRotation);

        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        EXP_objectPoolCount = 1;
        Assert.IsTrue(testObjectPooler.poolDictionary.Count == EXP_objectPoolCount);
        Assert.IsTrue(OUT_objectSpawned != null);
        yield return null;

        //<-------------------------------- Test TearDown ------------------------------->//
        Object.DestroyImmediate(testObjectPoolerGameObject);
    }
}
