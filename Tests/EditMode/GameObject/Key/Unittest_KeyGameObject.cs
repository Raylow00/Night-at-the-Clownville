using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_KeyGameObject
{
    // A Test behaves as an ordinary method
    [Test]
    public void Unittest_KeyGameObject_InitKey()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//

        KeyGameObject testKey = new KeyGameObject();

        //<-------------------------------- Test Execution ------------------------------>//

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsFalse(testKey.GetIsKeyObtained());
    }

    [Test]
    public void Unittest_KeyGameObject_ObtainKey()
    {
        // Use the Assert class to test conditions
        //<-------------------------------- Test Setup ---------------------------------->//

        KeyGameObject testKey = new KeyGameObject();

        //<-------------------------------- Test Execution ------------------------------>//
        testKey.ObtainKey();

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testKey.GetIsKeyObtained());
    }
}
