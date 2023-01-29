using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Energy
{
    private GameObject testEnergyGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_Health_InitHealth()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnergyGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Energy/Test Energy"));
        Energy testEnergy = testEnergyGameObject.GetComponent<Energy>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnergy.GetCurrentEnergy() == 100);
    }

    static int[] arg_value = new int[] { 1, 10, 100 };
    [UnityTest]
    public IEnumerator Unittest_Health_AddHealth_NoChange([ValueSource(nameof(arg_value))] int arg_increment)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnergyGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Energy/Test Energy"));
        Energy testEnergy = testEnergyGameObject.GetComponent<Energy>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEnergy.IncreaseEnergy(arg_increment);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnergy.GetCurrentEnergy() == 100);
    }

    [UnityTest]
    public IEnumerator Unittest_Health_AddHealth([ValueSource(nameof(arg_value))] int arg_increment)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnergyGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Energy/Test Energy"));
        Energy testEnergy = testEnergyGameObject.GetComponent<Energy>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEnergy.DecreaseEnergy(50);
        testEnergy.IncreaseEnergy(arg_increment);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnergy.GetCurrentEnergy() >= 51 && testEnergy.GetCurrentEnergy() <= 100);
    }

    static int[] arg_damage = new int[] { 1, 10 };
    [UnityTest]
    public IEnumerator Unittest_Health_TakeDamage([ValueSource(nameof(arg_damage))] int arg_damage)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnergyGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Energy/Test Energy"));
        Energy testEnergy = testEnergyGameObject.GetComponent<Energy>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEnergy.DecreaseEnergy(arg_damage);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnergy.GetCurrentEnergy() >= 90 && testEnergy.GetCurrentEnergy() < 100);
        Assert.IsTrue(testEnergy.GetEnergyZero() == false);
    }

    [UnityTest]
    public IEnumerator Unittest_Health_TakeDamage_ReachZero()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testEnergyGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Energy/Test Energy"));
        Energy testEnergy = testEnergyGameObject.GetComponent<Energy>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testEnergy.DecreaseEnergy(200);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnergy.GetCurrentEnergy() == 0);
        Assert.IsTrue(testEnergy.GetEnergyZero() == true);
    }
}
