using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.AI;
using System;
using Object = UnityEngine.Object;

public class Unittest_EnemyNavigation
{
    GameObject testGround;
    NavMeshData testNavMeshData;
    NavMeshDataInstance testNavMeshDataInstance;
    GameObject testEnemyNavigationGameObject;
    NavMeshAgent testNavMeshAgent;
    GameObject testPlayerGameObject;
    EnemyNavigation testEnemyNavigation;

    [SetUp]
    public void SetUp()
    {
        testGround = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/Navigation/Test Ground"));
        testNavMeshData = Resources.Load<NavMeshData>("TestResources/PlayMode/Enemy/Navigation/TestNavMesh");
        testNavMeshDataInstance = NavMesh.AddNavMeshData(testNavMeshData);
        testEnemyNavigationGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/Navigation/Test Enemy Navigation"));
        testNavMeshAgent = testEnemyNavigationGameObject.GetComponent<NavMeshAgent>();
        testPlayerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Enemy/Navigation/Test Player Transform"));
        testEnemyNavigation = testEnemyNavigationGameObject.GetComponent<EnemyNavigation>();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_EnemyNavigation_Init()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyNavigation.GetChaseTargetTransform().position = new Vector3(20f, 0f, 20f);
        yield return new WaitForSeconds(21f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyNavigation.GetNavMode() == NavigationMode.NAV_IDLE);
    }

    [UnityTest]
    public IEnumerator Unittest_EnemyNavigation_Idle_TargetEnterChaseRange()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//

        //<-------------------------------- Test Execution ------------------------------>//
        yield return new WaitForSeconds(5f);
        testEnemyNavigation.GetChaseTargetTransform().position = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(5f);
        Debug.Log("[TEST] target transform position: " + testEnemyNavigation.GetChaseTargetTransform().position);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyNavigation.GetNavMode() == NavigationMode.NAV_CHASE);
    }

    [UnityTest]
    public IEnumerator Unittest_EnemyNavigation_Patrol_TargetEnterChaseRange()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//

        //<-------------------------------- Test Execution ------------------------------>//
        yield return new WaitForSeconds(5f);
        testEnemyNavigation.GetChaseTargetTransform().position = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(5f);
        Debug.Log("[TEST] target transform position: " + testEnemyNavigation.GetChaseTargetTransform().position);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyNavigation.GetNavMode() == NavigationMode.NAV_CHASE);
    }

    [UnityTest]
    public IEnumerator Unittest_EnemyNavigation_Chase_TargetExitChaseRange()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyNavigation.GetChaseTargetTransform().position = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(5f);
        testEnemyNavigation.GetChaseTargetTransform().position = new Vector3(20f, 1f, 20f);
        yield return new WaitForSeconds(15f);
        Debug.Log("[TEST5] target transform position: " + testEnemyNavigation.GetChaseTargetTransform().position);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyNavigation.GetNavMode() == NavigationMode.NAV_PATROL);
    }

    [UnityTest]
    public IEnumerator Unittest_EnemyNavigation_Patrol_Idle_Patrol()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//

        //<-------------------------------- Test Execution ------------------------------>//
        testEnemyNavigation.GetChaseTargetTransform().position = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(5f);
        testEnemyNavigation.GetChaseTargetTransform().position = new Vector3(20f, 1f, 20f);
        yield return new WaitForSeconds(15f);
        Debug.Log("[TEST5] target transform position: " + testEnemyNavigation.GetChaseTargetTransform().position);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyNavigation.GetNavMode() == NavigationMode.NAV_PATROL);

        //<-------------------------------- Test Execution ------------------------------>//
        yield return new WaitForSeconds(21f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyNavigation.GetNavMode() == NavigationMode.NAV_IDLE);

        //<-------------------------------- Test Execution ------------------------------>//
        yield return new WaitForSeconds(21f);

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testEnemyNavigation.GetNavMode() == NavigationMode.NAV_PATROL);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testGround);
        Object.Destroy(testEnemyNavigationGameObject);
        Object.Destroy(testNavMeshAgent);
        Object.Destroy(testPlayerGameObject);
        Object.Destroy(testEnemyNavigation);
    }
}
