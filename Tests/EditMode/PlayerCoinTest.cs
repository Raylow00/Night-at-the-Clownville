using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerCoinTest
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    public void PlayerCoin_SetStartingCoins(int startingCoins)
    {
        var playerCoin = new PlayerCoin(startingCoins, null, null);

        Assert.IsTrue(playerCoin.currentCoins == startingCoins);
    }

    [Test]
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(50)]
    public void CollectCoins_ValidIncrement_ReturnsIncrementedCoins(int inc)
    {
        var playerCoin = new PlayerCoin(0, null, null);

        var currentCoins = playerCoin.CollectCoins(inc);

        Assert.IsTrue(currentCoins == inc);
    }

    [Test]
    public void UseCoins_ValidDecrement_ReturnsDecrementedCoins()
    {
        var playerCoin = new PlayerCoin(5, null, null);

        var currentCoins = playerCoin.UseCoins(1);

        Assert.IsTrue(currentCoins == 4);
    }

    [Test]
    public void UseCoins_InvalidDecrement_CurrentCoinsRemainSame()
    {
        var playerCoin = new PlayerCoin(5, null, null);

        var currentCoins = playerCoin.UseCoins(6);

        Assert.IsTrue(currentCoins == 5);
    }
}
