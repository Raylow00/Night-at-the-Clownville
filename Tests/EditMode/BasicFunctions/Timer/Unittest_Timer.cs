using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class Unittest_Timer
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(1f, null, false)]
    public void Unittest_TimerTickDown_Ok(float arg_duration, VoidEvent arg_onTimerEndEvent, bool arg_isTimerCyclic)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        Timer testTimer = new Timer(arg_duration, arg_onTimerEndEvent, arg_isTimerCyclic);

        //<-------------------------------- Test Execution ------------------------------>//
        bool hasTimerEnded = testTimer.Tick(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(hasTimerEnded);

        float EXP_timerDuration = 0f;
        Assert.That(testTimer.GetTimerDuration, Is.EqualTo(EXP_timerDuration).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//
        hasTimerEnded = testTimer.Tick(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(hasTimerEnded);
        EXP_timerDuration = 0f;
        Assert.That(testTimer.GetTimerDuration, Is.EqualTo(EXP_timerDuration).Using(FloatEqualityComparer.Instance));
    }

    [Test]
    [TestCase(10f, null, false)]
    public void Unittest_TimerTickDown_LongerTimerDuration_Ok(float arg_duration, VoidEvent arg_onTimerEndEvent, bool arg_isTimerCyclic)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        Timer testTimer = new Timer(arg_duration, arg_onTimerEndEvent, arg_isTimerCyclic);

        //<-------------------------------- Test Execution ------------------------------>//
        bool hasTimerEnded = testTimer.Tick(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(hasTimerEnded);

        float EXP_timerDuration = 9f;
        Assert.That(testTimer.GetTimerDuration, Is.EqualTo(EXP_timerDuration).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//
        hasTimerEnded = testTimer.Tick(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(hasTimerEnded);

        EXP_timerDuration = 8f;
        Assert.That(testTimer.GetTimerDuration, Is.EqualTo(EXP_timerDuration).Using(FloatEqualityComparer.Instance));
    }

    [Test]
    [TestCase(1f, null, true)]
    public void Unittest_TimerTickDown_Cyclic_Ok(float arg_duration, VoidEvent arg_onTimerEndEvent, bool arg_isTimerCyclic)
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        Timer testTimer = new Timer(arg_duration, arg_onTimerEndEvent, arg_isTimerCyclic);

        //<-------------------------------- Test Execution ------------------------------>//
        bool hasTimerEnded = testTimer.Tick(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(hasTimerEnded);

        float EXP_timerDuration = 1f;
        Assert.That(testTimer.GetTimerDuration, Is.EqualTo(EXP_timerDuration).Using(FloatEqualityComparer.Instance));

        //<-------------------------------- Test Execution ------------------------------>//
        hasTimerEnded = testTimer.Tick(1f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(hasTimerEnded);

        EXP_timerDuration = 1f;
        Assert.That(testTimer.GetTimerDuration, Is.EqualTo(EXP_timerDuration).Using(FloatEqualityComparer.Instance));

    }
}
