using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TimerTest
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(1f)]
    [TestCase(5f)]
    [TestCase(36.3f)]
    public void Timer_SetStartingDuration(float duration)
    {
        var timer = new Timer(duration);

        Assert.IsTrue(timer.remainingSeconds == duration);
    }

    [Test]
    public void TickDown_TickingBelowZeroSeconds_StopsAtZero()
    {
        var timer = new Timer(1f);

        timer.TickDown(2f);

        Assert.IsTrue(timer.remainingSeconds == 0f);
    }

    [Test]
    public void TickDown_TimerEnds_EventIsRaised()
    {
        var timer = new Timer(1f);

        bool isEventRaised = false;

        timer.onTimerEnd += () => isEventRaised = true;

        timer.TickDown(1f);

        Assert.IsTrue(isEventRaised);
    }

    public void TickDown_TimerDoesNotEnd_EventIsNotRaised()
    {
        var timer = new Timer(1f);

        bool isEventRaised = false;

        timer.onTimerEnd += () => isEventRaised = true;

        timer.TickDown(0.5f);

        Assert.IsFalse(isEventRaised);
    }
}
