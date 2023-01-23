using System.IO;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private string saveFolderName;
    [SerializeField] private StringEvent onGameSaveRequestEvent;
    #endregion

    #region Public Fields
    public static GameSaveManager instance;
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    #region Public Methods
    /// <summary>
    ///     Sends out the save game request to each settings manager 
    ///     and player data manager
    /// </summary>
    /// <param name="arg_saveSlotName"></param>
    /// <returns></returns>
    public bool SaveGame(string arg_saveSlotName)
    {
        // If no save directory was created, create one
        if (!HasSavedFile(saveFolderName, arg_saveSlotName))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + saveFolderName + "/" + arg_saveSlotName);
            Debug.Log("Created save directory at " + Application.persistentDataPath + "/" + saveFolderName + "/" + arg_saveSlotName);
        }
        // Overwrite if there is already a slot with the same name
        // TODO: To handle overwriting (raise question)
        else
        {
            Debug.LogWarning("WARNING: Slot exists, you are about to overwrite it!");
        }

        // Send out the save request with the save directory
        onGameSaveRequestEvent.Raise(Application.persistentDataPath + "/" + saveFolderName + "/" + arg_saveSlotName);

        return true;
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Checks whether there is an existing folder for the slot to save
    /// </summary>
    /// <param name="arg_saveFolderName"></param>
    /// <param name="arg_saveSlotName"></param>
    /// <returns></returns>
    private bool HasSavedFile(string arg_saveFolderName, string arg_saveSlotName)
    {
        return Directory.Exists(Application.persistentDataPath + "/" + arg_saveFolderName + "/" + arg_saveSlotName);
    }
    #endregion
}
