using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

///
/// Game data is saved under the file path:
/// C:\Users\Raych\AppData\LocalLow\Station R Games\Night at the Clownville
///
public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance;

    [Header("Folder Names")]
    [Tooltip("Folder containing all game files ('/game_data')")]
    [SerializeField] private string gameSaveFolder;
    [Tooltip("Folder containing all player data under game save folder ('/player_data')")]
    [SerializeField] private string playerDataFolder;
    [Tooltip("Folder containing all weapon data under game save folder ('/weapon_data')")]
    [SerializeField] private string weaponDataFolder;

    [Header("Player data to save")]
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private string playerStatsFileName;
    [SerializeField] private PlayerKeyBindingsSO playerKeyBindings;
    [Tooltip("Filename ('/player_keyBindings.txt')")]
    [SerializeField] private string playerKeyBindingsFileName;
    [SerializeField] private PlayerAudioSettingsSO playerAudioSettings;
    [SerializeField] private string playerAudioSettingsFileName;
    [SerializeField] private PlayerVideoSettingsSO playerVideoSettings;
    [SerializeField] private string playerVideoSettingsFileName;

    [Header("Weapon data")]
    [SerializeField] private WeaponStats[] weaponStats;

    [Header("Event")]
    [SerializeField] private VoidEvent onGameSavedEvent;
    [SerializeField] private VoidEvent onGameLoadedEvent;

    private string saveSlotName;
    private string loadSlotName;
    private int i;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);

        i = 0;
    }

    public void QuitGame()
    {
        // Debug.Log("Quitting application");

        AutoSaveGame();

        Application.Quit();
    }

    public void AutoSaveGame()
    {
        string temp = "autosave_" + i;

        i += 1;
        
        SaveGame(temp);
    }

    public void SaveGame(string folderName)
    {
        saveSlotName = folderName;

        // Checks if a folder for game save exists
        if(!HasSavedFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName);
        }

        SavePlayerData();

        SaveKeyBindings();

        SaveAudioSettings();

        SaveVideoSettings();

        SaveWeaponData();

        onGameSavedEvent.Raise();

        // Debug.Log("Game saved");
    }

    public List<string> ListSavedSlots()
    {
        List<string> slots = new List<string>();

        string[] dirs = Directory.GetDirectories(Application.persistentDataPath + gameSaveFolder);

        foreach(string dir in dirs)
        {
            slots.Add(dir.Replace(Application.persistentDataPath+gameSaveFolder+"\\", ""));
        }

        return slots;
    }

    public void LoadGame(string folderName)
    {
        loadSlotName = folderName;

        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder);
        }

        LoadPlayerData();

        LoadKeyBindings();

        LoadAudioSettings();

        LoadVideoSettings();

        LoadWeaponData();

        onGameLoadedEvent.Raise();

        // Debug.Log("Game loaded");
    }

    public void SaveKeyBindings()
    {
        // Checks if a folder for game save exists
        if(!HasSavedFile())     
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName);               
        }

        // Check if a folder specifically for player exists
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder))    
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder);
        }

        BinaryFormatter bf = new BinaryFormatter();

        // Serialize PlayerKeyBindings
        FileStream file = File.Create(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder + playerKeyBindingsFileName);
        var json = JsonUtility.ToJson(playerKeyBindings);
        bf.Serialize(file, json);
        file.Close();

        // Debug.Log("Key bindings saved");
    }

    public void LoadKeyBindings()
    {
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder);
        }

        BinaryFormatter bf = new BinaryFormatter();

        // Load PlayerKeyBindings
        if(File.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder + playerKeyBindingsFileName))
        {
            FileStream file = File.Open(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder + playerKeyBindingsFileName, FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerKeyBindings);

            // Debug.Log("Key bindings loaded");
        }
        else
        {
            // Debug.LogWarning("No such file exists!");
            return;
        }      
    }

    private void SaveAudioSettings()
    {
        // Check if a folder specifically for player exists
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder))    
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder);
        }

        BinaryFormatter bf = new BinaryFormatter();

        // Serialize PlayerKeyBindings
        FileStream file = File.Create(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder + playerAudioSettingsFileName);
        var json = JsonUtility.ToJson(playerAudioSettings);
        bf.Serialize(file, json);
        file.Close();

        // Debug.Log("Player audio settings saved");
    }

    private void LoadAudioSettings()
    {
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder);
        }

        BinaryFormatter bf = new BinaryFormatter();

        // Load PlayerKeyBindings
        if(File.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder + playerAudioSettingsFileName))
        {
            FileStream file = File.Open(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder + playerAudioSettingsFileName, FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerAudioSettings);

            // Debug.Log("Player audio settings loaded");
        }
        else
        {
            // Debug.LogWarning("No such file exists!");
            return;
        }
    }

    private void SaveVideoSettings()
    {
        // Check if a folder specifically for player exists
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder))    
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder);
        }

        BinaryFormatter bf = new BinaryFormatter();

        // Serialize PlayerKeyBindings
        FileStream file = File.Create(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder + playerVideoSettingsFileName);
        var json = JsonUtility.ToJson(playerVideoSettings);
        bf.Serialize(file, json);
        file.Close();

        // Debug.Log("Player video settings saved");
    }

    private void LoadVideoSettings()
    {
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder);
        }

        BinaryFormatter bf = new BinaryFormatter();

        // Load PlayerKeyBindings
        if(File.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder + playerVideoSettingsFileName))
        {
            FileStream file = File.Open(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder + playerVideoSettingsFileName, FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerVideoSettings);

            // Debug.Log("Player video settings loaded");
        }
        else
        {
            // Debug.LogWarning("No such file exists!");
            return;
        }
    }

    private void SavePlayerData()
    {
        // Check if a folder specifically for player exists
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder);
        }

        // Serialize PlayerStats
        BinaryFormatter bf = new BinaryFormatter();
        FileStream playerFile = File.Create(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + playerDataFolder + playerStatsFileName);
        var json = JsonUtility.ToJson(playerStats);
        bf.Serialize(playerFile, json);
        playerFile.Close();

        // Debug.Log("Player stats saved");
    }

    private void LoadPlayerData()
    {
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder);
        }

        BinaryFormatter bf = new BinaryFormatter();

        // Load PlayerStats
        if(File.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder + playerStatsFileName))
        {
            FileStream file = File.Open(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + playerDataFolder + playerStatsFileName, FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerStats);

            // Debug.Log("Player stats loaded");
        }
        else
        {
            // Debug.LogWarning("No such file exists!");
            return;
        }
    }

    private void SaveWeaponData()
    {
        // Check if a folder specifically for weapon exists
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + weaponDataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + weaponDataFolder);
        }

        // Serialize WeaponStats
        foreach(WeaponStats weaponStat in weaponStats)
        {
            BinaryFormatter weaponBinaryFormatter = new BinaryFormatter();
            FileStream weaponFile = File.Create(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName + weaponDataFolder + "/" + weaponStat.name + ".txt");
            var weaponJson = JsonUtility.ToJson(weaponStat);
            weaponBinaryFormatter.Serialize(weaponFile, weaponJson);
            weaponFile.Close();
        }

        // Debug.Log("Weapon data saved");
    }

    private void LoadWeaponData()
    {
        // Check if a folder specifically for weapon exists
        if(!Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + weaponDataFolder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + weaponDataFolder);
        }

        BinaryFormatter bf = new BinaryFormatter();

        // Load PlayerStats
        foreach(WeaponStats weaponStat in weaponStats)
        {
            if(File.Exists(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + weaponDataFolder + "/" + weaponStat.name + ".txt"))
            {
                FileStream file = File.Open(Application.persistentDataPath + gameSaveFolder + "/" + loadSlotName + weaponDataFolder + "/" + weaponStat.name + ".txt", FileMode.Open);

                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), weaponStat);

                // Debug.Log("Weapon stat " + weaponStat.name + " loaded");
            }
            else
            {
                // Debug.LogWarning("No such file exists!");
                return;
            }
        }
    }

    private bool HasSavedFile()
    {
        return Directory.Exists(Application.persistentDataPath + gameSaveFolder + "/" + saveSlotName);
    }
}
