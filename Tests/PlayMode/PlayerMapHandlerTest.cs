using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMapHandlerTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerMapHandler_UseMap_MapIsActivated()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerMapHandler playerMapHandler = playerGO.GetComponent<PlayerMapHandler>();

        playerMapHandler.UseMap();

        yield return new WaitForSeconds(2f);

        Assert.IsTrue(playerMapHandler.IsMapActivated);

        yield return new WaitForSeconds(2f);

        Object.Destroy(playerMapHandler.gameObject);

        // Debug.Log("Object destroyed");
    }

    [UnityTest]
    public IEnumerator PlayerMapHandler_MapActivatedFlipMap_MapIsFlipped()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerMapHandler playerMapHandler = playerGO.GetComponent<PlayerMapHandler>();

        playerMapHandler.UseMap();

        yield return new WaitForSeconds(2f);

        playerMapHandler.FlipMap();

        yield return new WaitForSeconds(2f);

        Assert.IsTrue(playerMapHandler.IsMapFlipped);

        yield return new WaitForSeconds(2f);

        Object.Destroy(playerMapHandler.gameObject);

        // Debug.Log("Object destroyed");
    }

    [UnityTest]
    public IEnumerator PlayerMapHandler_MapNotActivatedFlipMap_MapIsNotFlipped()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerMapHandler playerMapHandler = playerGO.GetComponent<PlayerMapHandler>();

        playerMapHandler.FlipMap();

        yield return new WaitForSeconds(2f);

        Assert.IsFalse(playerMapHandler.IsMapActivated);
        Assert.IsTrue(playerMapHandler.IsMapFlipped);

        yield return new WaitForSeconds(2f);

        Object.Destroy(playerMapHandler.gameObject);

        // Debug.Log("Object destroyed");
    }

    [UnityTest]
    public IEnumerator PlayerMapHandler_MapActivatedExitMap_MapExited()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerMapHandler playerMapHandler = playerGO.GetComponent<PlayerMapHandler>();

        playerMapHandler.UseMap();

        yield return new WaitForSeconds(2f);

        Assert.IsTrue(playerMapHandler.IsMapActivated);

        yield return new WaitForSeconds(2f);

        playerMapHandler.ExitMap();

        yield return new WaitForSeconds(2f);

        Assert.IsFalse(playerMapHandler.IsMapActivated);

        yield return new WaitForSeconds(2f);

        Object.Destroy(playerMapHandler.gameObject);

        // Debug.Log("Object destroyed");
    }

    [UnityTest]
    public IEnumerator PlayerMapHandler_MapNotActivatedExitMap_MapNotExited()
    {
        GameObject playerGO = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));

        PlayerMapHandler playerMapHandler = playerGO.GetComponent<PlayerMapHandler>();

        Assert.IsFalse(playerMapHandler.IsMapActivated);
        
        yield return new WaitForSeconds(2f);

        playerMapHandler.ExitMap();

        yield return new WaitForSeconds(2f);

        Assert.IsFalse(playerMapHandler.IsMapActivated);

        yield return new WaitForSeconds(2f);

        Object.Destroy(playerMapHandler.gameObject);

        // Debug.Log("Object destroyed");
    }
}