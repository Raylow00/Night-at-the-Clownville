using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Health
{
    private GameObject testHealthGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_Health_InitHealth()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Health/Test Health"));
        Health testHealth = testHealthGameObject.GetComponent<Health>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testHealth.GetCurrentHealth() == 100);
    }

    static int[] arg_value = new int[] { 1, 10, 100 };
    [UnityTest]
    public IEnumerator Unittest_Health_AddHealth_NoChange([ValueSource(nameof(arg_value))] int arg_increment)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Health/Test Health"));
        Health testHealth = testHealthGameObject.GetComponent<Health>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testHealth.AddHealth(arg_increment);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testHealth.GetCurrentHealth() == 100);
    }

    [UnityTest]
    public IEnumerator Unittest_Health_AddHealth([ValueSource(nameof(arg_value))] int arg_increment)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Health/Test Health"));
        Health testHealth = testHealthGameObject.GetComponent<Health>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testHealth.TakeDamage(50);
        testHealth.AddHealth(arg_increment);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testHealth.GetCurrentHealth() >= 51 && testHealth.GetCurrentHealth() <= 100);
    }

    static int[] arg_damage = new int[] { 1, 10 };
    [UnityTest]
    public IEnumerator Unittest_Health_TakeDamage([ValueSource(nameof(arg_damage))] int arg_damage)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Health/Test Health"));
        Health testHealth = testHealthGameObject.GetComponent<Health>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testHealth.TakeDamage(arg_damage);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testHealth.GetCurrentHealth() >= 90 && testHealth.GetCurrentHealth() < 100);
        Assert.IsTrue(testHealth.GetHealthZero() == false);
    }

    [UnityTest]
    public IEnumerator Unittest_Health_TakeDamage_ReachZero()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testHealthGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Health/Test Health"));
        Health testHealth = testHealthGameObject.GetComponent<Health>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testHealth.TakeDamage(200);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testHealth.GetCurrentHealth() == 0);
        Assert.IsTrue(testHealth.GetHealthZero() == true);
    }
}
