using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettingsManager : MonoBehaviour
{
    [HideInInspector] public VideoSettingsManager instance;

    [Header("Player Video Settings")]
    [SerializeField] private PlayerVideoSettingsSO playerVideoSettings;

    [Header("Resolution Scale")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

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
    }

    void Start()
    {
        resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[17].width, resolutions[17].height, Screen.fullScreen);

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if(options.IndexOf(option) == -1) options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetFullScreenMode(playerVideoSettings.fullScreenMode);
        SetResolution(playerVideoSettings.resolutionScale);
        SetGraphicsQuality(playerVideoSettings.qualityIndex);
    }

    public void SetFullScreenMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        playerVideoSettings.fullScreenMode = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        playerVideoSettings.resolutionScale = resolutionIndex;

        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        playerVideoSettings.qualityIndex = qualityIndex;
    }
}
