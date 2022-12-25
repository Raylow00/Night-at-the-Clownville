using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerEnergyTest
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(1f, 5f)]
    [TestCase(5f, 10f)]
    [TestCase(10f, 15f)]
    public void PlayerEnergy_SetStartingEnergy(float startingEnergy, float startingMaxEnergy)
    {
        var playerEnergy = new PlayerEnergy(startingEnergy, startingMaxEnergy, null, null);

        Assert.IsTrue(playerEnergy.currentEnergy == startingEnergy);
        Assert.IsTrue(playerEnergy.maxEnergy == startingMaxEnergy);
        Assert.IsTrue(playerEnergy.currentEnergy <= playerEnergy.maxEnergy);
    }

    [Test]
    [TestCase(1f, 10f)]
    [TestCase(10f, 20f)]
    [TestCase(50f, 100f)]
    public void IncreaseEnergy_ValidAddition_ReturnsIncrementedEnergy(float inc, float max)
    {
        var playerEnergy = new PlayerEnergy(0f, max, null, null);

        var currentEnergy = playerEnergy.IncreaseEnergy(inc);

        Assert.IsTrue(currentEnergy == inc);
        Assert.IsTrue(currentEnergy < playerEnergy.maxEnergy);
    }

    [Test]
    [TestCase(10f, 1f)]
    [TestCase(20f, 10f)]
    [TestCase(100f, 50f)]
    public void IncreaseEnergy_InvalidAddition_ReturnsMaxEnergy(float inc, float max)
    {
        var playerEnergy = new PlayerEnergy(0f, max, null, null);

        var currentEnergy = playerEnergy.IncreaseEnergy(inc);

        Assert.IsTrue(currentEnergy == playerEnergy.maxEnergy);
    }

    [Test]
    [TestCase(5f, 10f, 2f)]
    [TestCase(10f, 20f, 5f)]
    [TestCase(50f, 100f, 20f)]
    public void DecreaseEnergy_ValidSubtraction_ReturnsDecrementedEnergy(float startingFloat, float max, float dec)
    {
        var playerEnergy = new PlayerEnergy(startingFloat, max, null, null);

        var currentEnergy = playerEnergy.DecreaseEnergy(dec);

        Assert.IsTrue(currentEnergy == (startingFloat - dec));
        Assert.IsTrue(currentEnergy > 0f);
    }

    [Test]
    [TestCase(5f, 10f, 5f)]
    [TestCase(10f, 20f, 10f)]
    [TestCase(50f, 100f, 50f)]
    public void DecreaseEnergy_InvalidSubtraction_ReturnsZero(float startingFloat, float max, float dec)
    {
        var playerEnergy = new PlayerEnergy(startingFloat, max, null, null);

        var currentEnergy = playerEnergy.DecreaseEnergy(dec);

        Assert.IsTrue(currentEnergy == (startingFloat - dec));
        Assert.IsTrue(currentEnergy == 0f);
    }
}
