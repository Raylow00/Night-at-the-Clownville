using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_EnemyHealth
{
    private GameObject testEnemyHealthGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_EnemyHealth_InitHealth()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnemyHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/Health/Test Enemy Health"));
        EnemyHealth testEnemyHealth = testEnemyHealthGameObject.GetComponent<EnemyHealth>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyHealth.GetCurrentHealth() == 100);
    }

    static int[] arg_value = new int[] { 1, 10, 100 };
    [UnityTest]
    public IEnumerator Unittest_EnemyHealth_AddHealth_NoChange([ValueSource(nameof(arg_value))] int arg_increment)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnemyHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/Health/Test Enemy Health"));
        EnemyHealth testEnemyHealth = testEnemyHealthGameObject.GetComponent<EnemyHealth>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyHealth.AddHealth(arg_increment);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyHealth.GetCurrentHealth() == 100);
    }

    [UnityTest]
    public IEnumerator Unittest_EnemyHealth_AddHealth([ValueSource(nameof(arg_value))] int arg_increment)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnemyHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/Health/Test Enemy Health"));
        EnemyHealth testEnemyHealth = testEnemyHealthGameObject.GetComponent<EnemyHealth>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyHealth.TakeDamage(50);
        testEnemyHealth.AddHealth(arg_increment);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyHealth.GetCurrentHealth() >= 51 && testEnemyHealth.GetCurrentHealth() <= 100);
    }

    static int[] arg_damage = new int[] { 1, 10 };
    [UnityTest]
    public IEnumerator Unittest_EnemyHealth_TakeDamage([ValueSource(nameof(arg_damage))] int arg_damage)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnemyHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/Health/Test Enemy Health"));
        EnemyHealth testEnemyHealth = testEnemyHealthGameObject.GetComponent<EnemyHealth>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyHealth.TakeDamage(arg_damage);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyHealth.GetCurrentHealth() >= 90 && testEnemyHealth.GetCurrentHealth() < 100);
        Assert.IsTrue(testEnemyHealth.GetHealthZero() == false);
    }

    [UnityTest]
    public IEnumerator Unittest_EnemyHealth_TakeDamage_ReachZero()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnemyHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/Health/Test Enemy Health"));
        EnemyHealth testEnemyHealth = testEnemyHealthGameObject.GetComponent<EnemyHealth>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyHealth.TakeDamage(200);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyHealth.GetCurrentHealth() == 0);
        Assert.IsTrue(testEnemyHealth.GetHealthZero() == true);
    }
}
