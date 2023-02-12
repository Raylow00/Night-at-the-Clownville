using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Coin
{
    private GameObject testCoinGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_Coin_InitCoin()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testCoinGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Coin/Test Coin"));
        Coin testCoin = testCoinGameObject.GetComponent<Coin>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testCoin.GetCurrentCoin() == 100);
    }

    static int[] arg_value = new int[] { 1, 10, 100 };
    [UnityTest]
    public IEnumerator Unittest_Health_AddHealth_NoChange([ValueSource(nameof(arg_value))] int arg_increment)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testCoinGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Coin/Test Coin"));
        Coin testCoin = testCoinGameObject.GetComponent<Coin>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testCoin.AddCoin(arg_increment);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testCoin.GetCurrentCoin() == 100);
    }

    [UnityTest]
    public IEnumerator Unittest_Health_AddHealth([ValueSource(nameof(arg_value))] int arg_increment)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testCoinGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Coin/Test Coin"));
        Coin testCoin = testCoinGameObject.GetComponent<Coin>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testCoin.UseCoin(50);
        testCoin.AddCoin(arg_increment);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testCoin.GetCurrentCoin() >= 51 && testCoin.GetCurrentCoin() <= 100);
    }

    static int[] arg_damage = new int[] { 1, 10 };
    [UnityTest]
    public IEnumerator Unittest_Health_TakeDamage([ValueSource(nameof(arg_damage))] int arg_damage)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testCoinGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Coin/Test Coin"));
        Coin testCoin = testCoinGameObject.GetComponent<Coin>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testCoin.UseCoin(arg_damage);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testCoin.GetCurrentCoin() >= 90 && testCoin.GetCurrentCoin() < 100);
        Assert.IsTrue(testCoin.GetCoinZero() == false);
    }

    [UnityTest]
    public IEnumerator Unittest_Health_TakeDamage_ReachZero()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        testCoinGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Coin/Test Coin"));
        Coin testCoin = testCoinGameObject.GetComponent<Coin>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testCoin.UseCoin(200);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testCoin.GetCurrentCoin() == 0);
        Assert.IsTrue(testCoin.GetCoinZero() == true);
    }
}
