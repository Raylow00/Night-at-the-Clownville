using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_EnemyWave
{
    public GameObject testEnemyWaveSpawnerGameObject;

    [SetUp]
    public void SetUp()
    {
        testEnemyWaveSpawnerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/EnemyWave/Test Enemy Wave Spawner"));
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testEnemyWaveSpawnerGameObject);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_EnemyWave_SpawnEnemyWave()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        EnemyWaveSpawner testEnemyWaveSpawner = testEnemyWaveSpawnerGameObject.GetComponent<EnemyWaveSpawner>();

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyWaveSpawner.SpawnEnemyWave();
        Debug.Log("Number of enemies spawned: " + testEnemyWaveSpawner.GetEnemyWave().currentNumberOfEnemyInWave);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyWaveSpawner.GetEnemyWave().currentNumberOfEnemyInWave == 9);
        Assert.IsTrue(testEnemyWaveSpawner.GetIsEnemyWaveCleared() == false);
    }

    [UnityTest]
    public IEnumerator Unittest_EnemyWave_SpawnEnemyWave_DecrementEnemy()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        EnemyWaveSpawner testEnemyWaveSpawner = testEnemyWaveSpawnerGameObject.GetComponent<EnemyWaveSpawner>();

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyWaveSpawner.SpawnEnemyWave();
        Debug.Log("Number of enemies spawned: " + testEnemyWaveSpawner.GetEnemyWave().currentNumberOfEnemyInWave);
        yield return new WaitForSeconds(1f);

        testEnemyWaveSpawner.DecrementEnemyInWave();
        Debug.Log("Number of enemies spawned: " + testEnemyWaveSpawner.GetEnemyWave().currentNumberOfEnemyInWave);

        yield return new WaitForSeconds(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyWaveSpawner.GetEnemyWave().currentNumberOfEnemyInWave == 8);
        Assert.IsTrue(testEnemyWaveSpawner.GetIsEnemyWaveCleared() == false);
    }

    [UnityTest]
    public IEnumerator Unittest_EnemyWave_SpawnEnemyWave_DecrementEnemyUntilZero()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        EnemyWaveSpawner testEnemyWaveSpawner = testEnemyWaveSpawnerGameObject.GetComponent<EnemyWaveSpawner>();

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyWaveSpawner.SpawnEnemyWave();
        Debug.Log("Number of enemies spawned: " + testEnemyWaveSpawner.GetEnemyWave().currentNumberOfEnemyInWave);
        yield return new WaitForSeconds(1f);

        testEnemyWaveSpawner.DecrementEnemyInWave();
        testEnemyWaveSpawner.DecrementEnemyInWave();
        testEnemyWaveSpawner.DecrementEnemyInWave();
        testEnemyWaveSpawner.DecrementEnemyInWave();
        testEnemyWaveSpawner.DecrementEnemyInWave();
        testEnemyWaveSpawner.DecrementEnemyInWave();
        testEnemyWaveSpawner.DecrementEnemyInWave();
        testEnemyWaveSpawner.DecrementEnemyInWave();
        testEnemyWaveSpawner.DecrementEnemyInWave();
        Debug.Log("Number of enemies spawned: " + testEnemyWaveSpawner.GetEnemyWave().currentNumberOfEnemyInWave);

        yield return new WaitForSeconds(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyWaveSpawner.GetEnemyWave().currentNumberOfEnemyInWave == 0);
        Assert.IsTrue(testEnemyWaveSpawner.GetIsEnemyWaveCleared() == true);
    }
}
