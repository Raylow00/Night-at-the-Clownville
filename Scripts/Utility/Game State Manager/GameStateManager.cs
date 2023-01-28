using System.Collections;
using UnityEngine;

public enum GameState
{
    GAME_START,   // will not transition to GAME_ONGOING if no other state(PAUSE, SAVED and QUIT) is invoked
    GAME_PAUSE,    // will transition to GAME_ONGOING if current state is already GAME_PAUSED
    GAME_ONGOING,   // will transition to GAME_PAUSED if current state is already GAME_ONGOING
    GAME_SAVE,     // will transition to GAME_SAVED and remain if no other state(PAUSE and QUIT) is invoked
    GAME_QUIT       // will transition to GAME_QUIT if invoked
}

public class GameStateManager : MonoBehaviour
{

    #region Serialized Fields
    [SerializeField] private GameStateScriptableObject gameStateSO;
    [SerializeField] private VoidEvent onGameStartEvent;
    [SerializeField] private IntEvent onGamePauseEvent;
    [SerializeField] private VoidEvent onGameQuitEvent;
    [SerializeField] private VoidEvent onGameSaveRequestEvent;
    #endregion

    void Start()
    {
        SaveGameStates(GameState.GAME_START, GameState.GAME_START);
        ProcessGameState(GameState.GAME_ONGOING);
    }

    #region Public Methods
    /// <summary>
    ///     Sets the game state according to the enumeration above
    ///     and does the necessary processing for each state
    ///     - Game pause    - only if game is not paused and not quit 
    ///                     - sets and sends necessary data before pausing
    ///                     - if already paused and requested pause again, 
    ///                     - set game state to ongoing and resume game
    ///     - Game resume   - only if game is not already ongoing and not quit
    ///                     - if already ongoing and requested to resume,
    ///                     - set game state to pause
    ///     - Game save     - sends save game request and GameSaveManager to 
    ///                     - listen and perform the necessary save action
    ///     - Game quit     - sets and sends necessary data before quitting
    ///                     - auto save game before quitting (2 seconds for saving)
    ///     
    /// </summary>
    /// <param name="arg_state"></param>
    public void ProcessGameState(GameState arg_state)
    {
        // Cache current game state
        GameState currGameState = gameStateSO.currentGameState;
        
        switch (arg_state)
        {
            case GameState.GAME_START:
                // Send out game start event to 
                // reset player and missions
                onGameStartEvent.Raise();
                break;

            case GameState.GAME_PAUSE:
                // pause game if not PAUSED / QUIT
                if (currGameState != GameState.GAME_PAUSE &&
                    currGameState != GameState.GAME_QUIT)
                {
                    // Raise event first before STOPPING everything
                    onGamePauseEvent.Raise(1);
                    SaveGameStates(arg_state, currGameState);
                    Time.timeScale = 0f;
                }
                // if game state is already PAUSED, resume the game
                else if (currGameState == GameState.GAME_PAUSE &&
                         currGameState != GameState.GAME_QUIT)
                {
                    ProcessGameState(GameState.GAME_ONGOING);
                }
                else
                {
                    // Do nothing if already QUIT
                }
                
                break;

            case GameState.GAME_ONGOING:
                // Process game state to ONGOING if already started
                // prevGameState == STARTED
                // currGameState == ONGOING
                if (currGameState == GameState.GAME_START)
                {
                    SaveGameStates(arg_state, currGameState);
                }
                // resume the game if not ONGOING / QUIT
                else if (currGameState != GameState.GAME_ONGOING &&
                    currGameState != GameState.GAME_QUIT)
                {
                    // Raise event first before RESUMING everything
                    onGamePauseEvent.Raise(0);
                    SaveGameStates(arg_state, currGameState);
                    Time.timeScale = 1f;
                }
                // if game state is already ONGOING, pause the game
                else if (currGameState == GameState.GAME_ONGOING &&
                         currGameState != GameState.GAME_QUIT)
                {
                    ProcessGameState(GameState.GAME_PAUSE);
                }
                else
                {
                    // Do nothing if already QUIT
                }

                break;

            case GameState.GAME_SAVE:
                onGameSaveRequestEvent.Raise();
                SaveGameStates(arg_state, currGameState);

                break;

            case GameState.GAME_QUIT:
                // Raise event first before quitting application
                onGameQuitEvent.Raise();
                SaveGameStates(arg_state, currGameState);

                StartCoroutine(QuitGame());

                break;

            default:
                // Do nothing
                break;
        }
    }

    public GameStateScriptableObject GetScriptableObject()
    {
        return gameStateSO;
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Stores the current and previous game state into scriptable object
    /// </summary>
    /// <param name="arg_currState"></param>
    /// <param name="arg_prevState"></param>
    private void SaveGameStates(GameState arg_currState, GameState arg_prevState)
    {
        gameStateSO.currentGameState = arg_currState;
        gameStateSO.previousGameState = arg_prevState;
    }

    /// <summary>
    ///     Wait for 2 seconds before quitting game application
    /// </summary>
    /// <returns></returns>
    private IEnumerator QuitGame()
    {
        // Give 2 seconds to save game before quitting
        yield return new WaitForSeconds(2f);

        Application.Quit();
    }
    #endregion
}
