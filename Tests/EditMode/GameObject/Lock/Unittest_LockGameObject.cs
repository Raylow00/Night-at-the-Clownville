using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_LockGameObject
{
    // A Test behaves as an ordinary method
    [Test]
    public void Unittest_LockGameObject_InitLock()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        LockGameObject testLock = new LockGameObject();

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(testLock.GetIsLockOpened());

    }

    [Test]
    public void Unittest_LockGameObject_OpenLock()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        LockGameObject testLock = new LockGameObject();

        //<-------------------------------- Test Execution ------------------------------>//
        testLock.OpenLock();

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testLock.GetIsLockOpened());

    }

    [Test]
    public void Unittest_LockGameObject_CloseLock()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//
        LockGameObject testLock = new LockGameObject();

        //<-------------------------------- Test Execution ------------------------------>//
        testLock.OpenLock();
        testLock.CloseLock();

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(testLock.GetIsLockOpened());

    }
}
