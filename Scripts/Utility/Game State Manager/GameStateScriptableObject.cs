using UnityEngine;

/// <summary>
///     Game state settings to be stored in memory for use in any part of the program
/// </summary>
[CreateAssetMenu(fileName = "GameStateScriptableObject", menuName = "Utility/Game State/Game State")]
public class GameStateScriptableObject : ScriptableObject
{
    public int previousGameState;
    public int currentGameState;
}
