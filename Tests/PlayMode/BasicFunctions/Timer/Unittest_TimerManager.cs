using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

public class Unittest_TimerManager
{
    private GameObject testTimerManagerGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_TimerManager_NonCyclicTimerTick()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        testTimerManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/Timer/Test_Timer_Manager_Non_Cyclic"));
        TimerManager testTimerManager = testTimerManagerGameObject.GetComponent<TimerManager>();

        yield return new WaitForSeconds(10f);

        float EXP_timerDuration = 0f;
        Assert.That(testTimerManager.GetTimerDuration, Is.EqualTo(EXP_timerDuration).Using(FloatEqualityComparer.Instance));
    }

    [UnityTest]
    public IEnumerator Unittest_TimerManager_CyclicTimerTick()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        testTimerManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/BasicFunctions/Timer/Test_Timer_Manager_Cyclic"));
        TimerManager testTimerManager = testTimerManagerGameObject.GetComponent<TimerManager>();

        yield return new WaitForSeconds(10f);

        float EXP_timerDuration = 10f;
        Assert.That(testTimerManager.GetTimerDuration, Is.EqualTo(EXP_timerDuration).Using(FloatEqualityComparer.Instance));
    }
}
