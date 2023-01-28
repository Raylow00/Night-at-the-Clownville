using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_MissionManager
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_MissionManager_IDLE_ProcessMissionState_INIT()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testMissionManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Manager"));
        MissionManager testMissionManager = testMissionManagerGameObject.GetComponent<MissionManager>();
        MissionScriptableObject testMissionScriptableObject = testMissionManager.GetMissionScriptableObject();

        GameObject testVoidEventListenerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Init Listener"));
        VoidEventListener testVoidEventListener = testVoidEventListenerGameObject.GetComponent<VoidEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testMissionManager.ProcessMissionState(MissionState.MISSION_INIT);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testMissionScriptableObject.currMissionState == MissionState.MISSION_INIT);
        Assert.IsTrue(testVoidEventListener.GetAcknowledgement() == true);

    }

    [UnityTest]
    public IEnumerator Unittest_MissionManager_IDLE_ProcessMissionState_ONGOING()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testMissionManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Manager"));
        MissionManager testMissionManager = testMissionManagerGameObject.GetComponent<MissionManager>();
        MissionScriptableObject testMissionScriptableObject = testMissionManager.GetMissionScriptableObject();

        GameObject testVoidEventListenerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Ongoing Listener"));
        VoidEventListener testVoidEventListener = testVoidEventListenerGameObject.GetComponent<VoidEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testMissionManager.ProcessMissionState(MissionState.MISSION_ONGOING);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testMissionScriptableObject.currMissionState == MissionState.MISSION_IDLE);
        Assert.IsTrue(testVoidEventListener.GetAcknowledgement() == false);
    }

    [UnityTest]
    public IEnumerator Unittest_MissionManager_IDLE_ProcessMissionState_COMPLETE()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testMissionManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Manager"));
        MissionManager testMissionManager = testMissionManagerGameObject.GetComponent<MissionManager>();
        MissionScriptableObject testMissionScriptableObject = testMissionManager.GetMissionScriptableObject();

        GameObject testVoidEventListenerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Complete Listener"));
        VoidEventListener testVoidEventListener = testVoidEventListenerGameObject.GetComponent<VoidEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testMissionManager.ProcessMissionState(MissionState.MISSION_COMPLETE);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testMissionScriptableObject.currMissionState == MissionState.MISSION_IDLE);
        Assert.IsTrue(testVoidEventListener.GetAcknowledgement() == false);

    }

    [UnityTest]
    public IEnumerator Unittest_MissionManager_IDLE_ProcessMissionState_FAILED()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testMissionManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Manager"));
        MissionManager testMissionManager = testMissionManagerGameObject.GetComponent<MissionManager>();
        MissionScriptableObject testMissionScriptableObject = testMissionManager.GetMissionScriptableObject();

        GameObject testVoidEventListenerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Fail Listener"));
        VoidEventListener testVoidEventListener = testVoidEventListenerGameObject.GetComponent<VoidEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testMissionManager.ProcessMissionState(MissionState.MISSION_FAILED);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testMissionScriptableObject.currMissionState == MissionState.MISSION_IDLE);
        Assert.IsTrue(testVoidEventListener.GetAcknowledgement() == false);

    }

    [UnityTest]
    public IEnumerator Unittest_MissionManager_IDLE_ProcessMissionState_RESET()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        GameObject testMissionManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Manager"));
        MissionManager testMissionManager = testMissionManagerGameObject.GetComponent<MissionManager>();
        MissionScriptableObject testMissionScriptableObject = testMissionManager.GetMissionScriptableObject();

        GameObject testVoidEventListenerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Mission/Test Mission Reset Listener"));
        VoidEventListener testVoidEventListener = testVoidEventListenerGameObject.GetComponent<VoidEventListener>();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testMissionManager.ProcessMissionState(MissionState.MISSION_RESET);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testMissionScriptableObject.currMissionState == MissionState.MISSION_IDLE);
        Assert.IsTrue(testVoidEventListener.GetAcknowledgement() == false);

    }
}
