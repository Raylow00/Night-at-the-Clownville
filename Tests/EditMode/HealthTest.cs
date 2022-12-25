using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTest
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(1, 10)]
    [TestCase(5, 10)]
    [TestCase(10, 20)]
    public void Health_SetStartingHealth(int curHealth, int mHealth)
    {
        var health = new Health(curHealth, mHealth, null, null);

        Assert.IsTrue(health.currentHealth == curHealth);
        Assert.IsTrue(health.maxHealth == mHealth);
        Assert.IsTrue(health.currentHealth <= health.maxHealth);
        Assert.IsTrue(health.currentHealth != 0f);
    }

    [Test]
    [TestCase(5, 1)]
    [TestCase(10, 5)]
    [TestCase(20, 10)]
    public void Health_TakeDamageAboveZeroHealth_ReturnsDecrementedHealth(int startingHealth, int damage)
    {
        var health = new Health(startingHealth, startingHealth, null, null);

        health.TakeDamage(damage, Vector3.zero, Vector3.zero);

        Assert.IsTrue(health.currentHealth == (startingHealth - damage));
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(5, 10)]
    [TestCase(10, 20)]
    public void Health_TakeDamageBelowZeroHealth_ReturnsZero(int startingHealth, int damage)
    {
        var health = new Health(startingHealth, startingHealth, null, null);

        health.TakeDamage(damage, Vector3.zero, Vector3.zero);

        Assert.IsTrue(health.currentHealth == 0f);
    }

    [Test]
    [TestCase(1, 10, 1)]
    [TestCase(5, 20, 10)]
    [TestCase(10, 50, 20)]
    public void Health_IncreaseHealthBelowMaxHealth_ReturnsIncrementedHealth(int startingHealth, int maxHealth, int inc)
    {
        var health = new Health(startingHealth, maxHealth, null, null);

        health.ReceiveHealth(inc);

        Assert.IsTrue(health.currentHealth == (startingHealth + inc));
    }

    [Test]
    [TestCase(1, 10, 20)]
    [TestCase(5, 20, 50)]
    [TestCase(10, 50, 100)]
    public void Health_IncreaseHealthAboveMaxHealth_ReturnsMaxHealth(int startingHealth, int maxHealth, int inc)
    {
        var health = new Health(startingHealth, maxHealth, null, null);

        health.ReceiveHealth(inc);

        Assert.IsTrue(health.currentHealth == maxHealth);
    }
}
