using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoSettingsManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private VideoSettingsScriptableObject videoSettingsSO;
    [SerializeField] private StringEvent onResolutionPopulateEvent;
    [SerializeField] private IntEvent onResolutionChangeEvent;
    [SerializeField] private BoolEvent onFullScreenToggleEvent;
    [SerializeField] private IntEvent onFullScreenModeChangeEvent;
    [SerializeField] private IntEvent onVideoQualityIndexChangeEvent;
    #endregion

    #region Private Fields
    private static VideoSettingsManager instance;
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

        InitDefaultVideoSettings();
    }

    #region Public Methods
    /// <summary>
    ///     Get the video settings scriptable object
    /// </summary>
    /// <returns>
    ///     videoSettingsSO
    /// </returns>
    public VideoSettingsScriptableObject GetVideoSettingsScriptableObject()
    {
        return videoSettingsSO;
    }

    /// <summary>
    ///     Get the current resolution width
    /// </summary>
    /// <returns>
    ///     Screen.currentResolution.width
    /// </returns>
    public int GetResolutionWidth()
    {
        return Screen.currentResolution.width;
    }

    /// <summary>
    ///     Get the current resolution height
    /// </summary>
    /// <returns>
    ///     Screen.currentResolution.height
    /// </returns>
    public int GetResolutionHeight()
    {
        return Screen.currentResolution.height;
    }

    /// <summary>
    ///     Get whether the game is in fullscreen or not
    /// </summary>
    /// <returns>
    ///     True if full screen
    ///     False otherwise
    /// </returns>
    public bool GetFullscreen()
    {
        return Screen.fullScreen;
    }

    /// <summary>
    ///     Get the current fullscreen mode of the game
    /// </summary>
    /// <returns>
    ///     EXCLUSIVE_FULLSCREEN
    ///     FULLSCREEN_WINDOW
    ///     MAXIMIZED_WINDOW
    ///     WINDOWED
    /// </returns>
    public int GetFullscreenMode()
    {
        return (int)Screen.fullScreenMode;
    }

    /// <summary>
    ///     Get the current in-game video quality index
    ///     The list of quality indices is set by developer in the editor under Quality 
    /// </summary>
    /// <returns></returns>
    public int GetVideoQualityIndex()
    {
        return QualitySettings.GetQualityLevel();
    }

    /// <summary>
    ///     Set the screen resolution
    ///     Will NOT take effect in the editor, only in Build or Developer Build
    ///     -1 will be the default value from ScriptableObject because 
    ///     we are taking the last element in the resolution list which is the highest resolution
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetResolution(int arg_value)
    {
        // Store the resolution selection in SO
        videoSettingsSO.videoResolutionScale = arg_value;

        // Set the screen resolution
        Resolution[] resolutions = Screen.resolutions;
        if (arg_value == -1)
        {
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, videoSettingsSO.fullScreen);
        }
        else
        {
            Screen.SetResolution(resolutions[arg_value].width, resolutions[arg_value].height, videoSettingsSO.fullScreen);
        }

        // Send out the resolution selection from dropdown
        onResolutionChangeEvent.Raise(arg_value);

        Debug.Log("[Video Settings Manager] Selection: " + arg_value);
        Debug.Log("Current resolution of game window in editor: " + Screen.width + " x " + Screen.height);
        Debug.Log("Current screen resolution: " + Screen.currentResolution);
    }

    /// <summary>
    ///     Set full screen
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetFullScreen(bool arg_value)
    {
        // Store the full screen selection in SO
        videoSettingsSO.fullScreen = arg_value;

        // Set the full screen accordingly
        Screen.fullScreen = arg_value;
    }

    /// <summary>
    ///     Set the type of full screen 
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetFullScreenMode(int arg_value)
    {
        switch (arg_value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 3:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
        }

        // Store the fullscreen mode to SO
        videoSettingsSO.fullScreenMode = arg_value;
    }

    /// <summary>
    ///     Set the video quality index from the list of indices established by developer
    ///     0 - Low
    ///     1 - Medium
    ///     2 - High
    ///     This can be configured in Player settings > Quality
    /// </summary>
    /// <param name="arg_value"></param>
    public void SetVideoQualityIndex(int arg_value)
    {
        // Store the video quality index to SO
        videoSettingsSO.videoQualityIndex = arg_value;

        // Set the quality level from input
        QualitySettings.SetQualityLevel(arg_value);
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Initialize the default video settings from the VideoSettingsScriptableObject
    /// </summary>
    private void InitDefaultVideoSettings()
    {
        videoSettingsSO.fullScreenMode = videoSettingsSO.default_FullScreenMode;
        videoSettingsSO.fullScreen = videoSettingsSO.default_FullScreen;
        videoSettingsSO.videoResolutionScale = videoSettingsSO.default_VideoResolutionScale;
        videoSettingsSO.videoQualityIndex = videoSettingsSO.default_VideoQualityIndex;

        SetFullScreen(videoSettingsSO.fullScreen);
        SetFullScreenMode(videoSettingsSO.fullScreenMode);
        SetResolution(videoSettingsSO.videoResolutionScale);
        SetVideoQualityIndex(videoSettingsSO.videoQualityIndex);

        // Process the available resolutions and send them out one by one
        // to DropdownHandler to receive and populate in the dropdown UI
        Debug.Log("[Video Settings Manager] Available resolutions: ");
        Resolution[] resolutions = Screen.resolutions;
        string[] availableResolutionOptions = ProcessResolutions(resolutions);
        foreach (string option in availableResolutionOptions)
        {
            onResolutionPopulateEvent.Raise(option);
            Debug.Log("Option: " + option);
        }
    }

    /// <summary>
    ///     Convert the resolution list to a string array
    /// </summary>
    /// <param name="arg_values"></param>
    /// <returns></returns>
    private string[] ProcessResolutions(Resolution[] arg_values)
    {
        List<string> retArrList = new List<string>();
        foreach (Resolution val in arg_values)
        {
            string retVal = val.ToString();
            retArrList.Add(retVal);
        }

        return retArrList.ToArray();
    }
    #endregion
}
