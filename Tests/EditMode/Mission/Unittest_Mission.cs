using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_Mission
{
    private string testMissionName = "testMission";

    // A Test behaves as an ordinary method
    [Test]
    public void Unittest_Mission_Idle_SetMissionIdle()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_IDLE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_IDLE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_IDLE);
    }

    [Test]
    public void Unittest_Mission_Idle_SetMissionInit()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_IDLE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_INIT);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_INIT);
    }

    [Test]
    public void Unittest_Mission_Idle_SetMissionOngoing()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_IDLE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_IDLE);
    }

    [Test]
    public void Unittest_Mission_Idle_SetMissionComplete()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_IDLE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_IDLE);
    }

    [Test]
    public void Unittest_Mission_Idle_SetMissionFailed()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_IDLE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_FAILED);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_IDLE);
    }

    [Test]
    public void Unittest_Mission_Idle_SetMissionReset()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_IDLE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_RESET);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_IDLE);
    }

    [Test]
    public void Unittest_Mission_Init_SetMissionIdle()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_INIT);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_IDLE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_INIT);
    }

    [Test]
    public void Unittest_Mission_Init_SetMissionInit()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_INIT);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_INIT);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_INIT);
    }

    [Test]
    public void Unittest_Mission_Init_SetMissionOngoing()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_INIT);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_ONGOING);
    }

    [Test]
    public void Unittest_Mission_Init_SetMissionComplete()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_INIT);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_INIT);
    }

    [Test]
    public void Unittest_Mission_Init_SetMissionFailed()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_INIT);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_FAILED);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_INIT);
    }

    [Test]
    public void Unittest_Mission_Init_SetMissionReset()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_INIT);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_RESET);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_INIT);
    }

    [Test]
    public void Unittest_Mission_Ongoing_SetMissionIdle()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_IDLE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_ONGOING);
    }

    [Test]
    public void Unittest_Mission_Ongoing_SetMissionInit()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_INIT);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_ONGOING);
    }

    [Test]
    public void Unittest_Mission_Ongoing_SetMissionOngoing()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_ONGOING);
    }

    [Test]
    public void Unittest_Mission_Ongoing_SetMissionComplete()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_COMPLETE);
    }

    [Test]
    public void Unittest_Mission_Ongoing_SetMissionFailed()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_FAILED);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_FAILED);
    }

    [Test]
    public void Unittest_Mission_Ongoing_SetMissionReset()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_RESET);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_RESET);
    }

    [Test]
    public void Unittest_Mission_Complete_SetMissionIdle()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_IDLE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_IDLE);
    }

    [Test]
    public void Unittest_Mission_Complete_SetMissionInit()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_INIT);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_COMPLETE);
    }

    [Test]
    public void Unittest_Mission_Complete_SetMissionOngoing()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_COMPLETE);
    }

    [Test]
    public void Unittest_Mission_Complete_SetMissionComplete()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_COMPLETE);
    }

    [Test]
    public void Unittest_Mission_Complete_SetMissionFailed()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_FAILED);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_COMPLETE);
    }

    [Test]
    public void Unittest_Mission_Complete_SetMissionReset()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_RESET);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_COMPLETE);
    }

    [Test]
    public void Unittest_Mission_Failed_SetMissionIdle()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_FAILED);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_IDLE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_IDLE);
    }

    [Test]
    public void Unittest_Mission_Failed_SetMissionInit()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_FAILED);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_INIT);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_FAILED);
    }

    [Test]
    public void Unittest_Mission_Failed_SetMissionOngoing()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_FAILED);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_FAILED);
    }

    [Test]
    public void Unittest_Mission_Failed_SetMissionComplete()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_FAILED);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_FAILED);
    }

    [Test]
    public void Unittest_Mission_Failed_SetMissionFailed()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_FAILED);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_FAILED);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_FAILED);
    }

    [Test]
    public void Unittest_Mission_Failed_SetMissionReset()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_FAILED);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_RESET);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_FAILED);
    }

    [Test]
    public void Unittest_Mission_Reset_SetMissionIdle()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_RESET);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_IDLE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_RESET);
    }

    [Test]
    public void Unittest_Mission_Reset_SetMissionInit()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_RESET);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_INIT);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_INIT);
    }

    [Test]
    public void Unittest_Mission_Reset_SetMissionOngoing()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_RESET);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_ONGOING);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_RESET);
    }

    [Test]
    public void Unittest_Mission_Reset_SetMissionComplete()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_RESET);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_COMPLETE);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_RESET);
    }

    [Test]
    public void Unittest_Mission_Reset_SetMissionFailed()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_RESET);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_FAILED);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_RESET);
    }

    [Test]
    public void Unittest_Mission_Reset_SetMissionReset()
    {
        //<-------------------------------- Test Setup ---------------------------------->//
        Mission testMission = new Mission(testMissionName, MissionState.MISSION_RESET);

        //<-------------------------------- Test Execution ------------------------------>//
        testMission.SetMissionState(MissionState.MISSION_RESET);

        //<-------------------------------- Test Expectation ---------------------------->//
        MissionState currMissionState = testMission.GetMissionState();
        Assert.IsTrue(currMissionState == MissionState.MISSION_RESET);
    }
}
