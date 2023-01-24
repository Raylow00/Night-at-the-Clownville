using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Unittest_GameStateManager
{
    private GameObject testGameStateManagerGameObject;

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Unittest_GameStateManager_OnStart()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();

        //<-------------------------------- Test Execution ------------------------------>//
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_GAME_STARTED()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        testGameStateManager.ProcessGameState(0);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_ONGOING_Input_PAUSED_Output_PAUSED()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_PAUSED
        testGameStateManager.ProcessGameState(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_PAUSED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_PAUSED);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_ONGOING
        testGameStateManager.ProcessGameState(2);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_PAUSED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_PAUSED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_ONGOING_Input_QUIT_Output_QUIT()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_QUIT
        testGameStateManager.ProcessGameState(4);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_QUIT
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_QUIT);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_ONGOING_Input_ONGOING_Output_PAUSED()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_ONGOING
        testGameStateManager.ProcessGameState(2);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_PAUSED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_PAUSED);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_ONGOING_Input_SAVED_Output_SAVED()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_ONGOING
        testGameStateManager.ProcessGameState(3);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_SAVED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_SAVED);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_PAUSED_Input_ONGOING_Output_ONGOING()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_PAUSED
        testGameStateManager.ProcessGameState(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_PAUSED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_PAUSED);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_ONGOING
        testGameStateManager.ProcessGameState(2);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_PAUSED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_PAUSED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_PAUSED_Input_PAUSED_Output_ONGOING()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_PAUSED
        testGameStateManager.ProcessGameState(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_PAUSED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_PAUSED);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_PAUSED again
        testGameStateManager.ProcessGameState(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_PAUSED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_PAUSED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_PAUSED_Input_SAVED_Output_SAVED()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_ONGOING
        testGameStateManager.ProcessGameState(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_PAUSED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_PAUSED);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_SAVED
        testGameStateManager.ProcessGameState(3);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_SAVED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_PAUSED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_SAVED);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_PAUSED_Input_QUIT_Output_QUIT()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_PAUSED
        testGameStateManager.ProcessGameState(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_PAUSED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_PAUSED);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_QUIT
        testGameStateManager.ProcessGameState(4);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_PAUSED
        // Current game state => GAME_QUIT
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_PAUSED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_QUIT);
    }


    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_QUIT_Input_PAUSED_Output_QUIT()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_PAUSED
        testGameStateManager.ProcessGameState(4);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_PAUSED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_QUIT);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_PAUSED again
        testGameStateManager.ProcessGameState(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_PAUSED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_QUIT);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_QUIT_Input_ONGOING_Output_QUIT()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_QUIT
        testGameStateManager.ProcessGameState(4);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_QUIT
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_QUIT);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_ONGOING
        testGameStateManager.ProcessGameState(2);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_QUIT
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_QUIT);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_QUIT_Input_SAVED_Output_SAVED()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_QUIT
        testGameStateManager.ProcessGameState(4);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_QUIT
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_QUIT);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_SAVED
        testGameStateManager.ProcessGameState(3);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_QUIT
        // Current game state => GAME_SAVED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_QUIT);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_SAVED);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_SAVED_Input_PAUSED_Output_PAUSED()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_SAVED
        testGameStateManager.ProcessGameState(3);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_SAVED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_SAVED);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_PAUSED
        testGameStateManager.ProcessGameState(1);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_SAVED
        // Current game state => GAME_PAUSED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_SAVED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_PAUSED);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_SAVED_Input_ONGOING_Output_ONGOING()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_SAVED
        testGameStateManager.ProcessGameState(3);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_SAVED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_SAVED);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_ONGOING
        testGameStateManager.ProcessGameState(2);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_SAVED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_SAVED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);
    }

    [UnityTest]
    public IEnumerator Unittest_GameStateManager_ProcessGameState_CurrState_SAVED_Input_QUIT_Output_QUIT()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        //<-------------------------------- Test Setup ---------------------------------->//
        testGameStateManagerGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestResources/PlayMode/Utility/GameStateManager/Test Game State Manager"));
        GameStateManager testGameStateManager = testGameStateManagerGameObject.GetComponent<GameStateManager>();
        GameStateScriptableObject testGameStateScriptableObject = testGameStateManager.GetScriptableObject();
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // On start
        // Previous game state => GAME_STARTED
        // Current game state => GAME_ONGOING
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_STARTED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_ONGOING);

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_SAVED
        testGameStateManager.ProcessGameState(3);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_ONGOING
        // Current game state => GAME_SAVED
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_ONGOING);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_SAVED);
        yield return null;

        //<-------------------------------- Test Execution ------------------------------>//
        // Set game state to GAME_QUIT
        testGameStateManager.ProcessGameState(4);
        yield return null;

        //<-------------------------------- Test Expectation ---------------------------->//
        // Previous game state => GAME_SAVED
        // Current game state => GAME_QUIT
        Assert.IsTrue(testGameStateScriptableObject.previousGameState == (int)GameState.GAME_SAVED);
        Assert.IsTrue(testGameStateScriptableObject.currentGameState == (int)GameState.GAME_QUIT);
    }
}
