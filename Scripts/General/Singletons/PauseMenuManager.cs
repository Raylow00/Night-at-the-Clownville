/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;
    public bool isGamePaused = false;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuGO;

    [Header("Options Menu")]
    [SerializeField] private GameObject optionsMenuGO;

    [Header("Mouse Sensitivity")]
    [SerializeField] private FloatEvent onSensitivityChangeEvent;
    [SerializeField] private PlayerKeyBindingsSO playerKeyPreferences;
    [SerializeField] private Slider mouseSensitivitySlider;

    [Header("Field of View")]
    [SerializeField] private FloatEvent onFOVChangeEvent;
    [SerializeField] private Slider fovSlider;

    [Header("Audio Settings")]
    [SerializeField] private PlayerAudioSettingsSO playerAudioSettingsSO;
    
    [Header("Master Volume")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private FloatEvent onMasterVolumeChangeEvent;

    [Header("Music Volume")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private FloatEvent onMusicVolumeChangeEvent;

    [Header("SFX Volume")]
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private FloatEvent onEffectsVolumeChangeEvent;

    [Header("Ambience Volume")]
    [SerializeField] private Slider ambienceVolumeSlider;
    [SerializeField] private FloatEvent onAmbienceVolumeChangeEvent;

    [Header("Player Input Manager")]
    [SerializeField] private PlayerInputManager inputManager;

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
        SetMasterVolumeSlider(playerAudioSettingsSO.masterVolume);
        SetMusicVolumeSlider(playerAudioSettingsSO.musicVolume);
        SetEffectsVolumeSlider(playerAudioSettingsSO.sfxVolume);
        SetAmbienceVolumeSlider(playerAudioSettingsSO.ambienceVolume);
    }

    public void OnPauseButtonPressed()
    {
        if(isGamePaused)
        {
            Debug.Log("Resuming game...");
            Resume();
        }
        else
        {
            Debug.Log("Game paused");
            LoadPauseMenu();
        }
    }

    public void LoadOptionsMenu()
    {
        Debug.Log("Loading options menu");
        
        pauseMenuGO.SetActive(false);
        optionsMenuGO.SetActive(true);

        onSensitivityChangeEvent.Raise(playerKeyPreferences.mouseSensitivityY);
        mouseSensitivitySlider.value = playerKeyPreferences.mouseSensitivityY;

        onFOVChangeEvent.Raise(playerKeyPreferences.fieldOfView);
        fovSlider.value = playerKeyPreferences.fieldOfView;

        masterVolumeSlider.value = playerAudioSettingsSO.masterVolume;
        musicVolumeSlider.value = playerAudioSettingsSO.musicVolume;
        effectsVolumeSlider.value = playerAudioSettingsSO.sfxVolume;
        ambienceVolumeSlider.value = playerAudioSettingsSO.ambienceVolume;

        onMasterVolumeChangeEvent.Raise(playerAudioSettingsSO.masterVolume);
        onMusicVolumeChangeEvent.Raise(playerAudioSettingsSO.musicVolume);
        onEffectsVolumeChangeEvent.Raise(playerAudioSettingsSO.sfxVolume);
        onAmbienceVolumeChangeEvent.Raise(playerAudioSettingsSO.ambienceVolume);

        Time.timeScale = 0f;

        isGamePaused = true;
    }

    public void SetMasterVolumeSlider(float value)
    {
        playerAudioSettingsSO.masterVolume = value;
        AudioSettingsManager.instance.SetMasterVolume(value);
        onMasterVolumeChangeEvent.Raise(value);
    }

    public void SetMusicVolumeSlider(float value)
    {
        playerAudioSettingsSO.musicVolume = value;
        AudioSettingsManager.instance.SetMusicVolume(value);
        onMusicVolumeChangeEvent.Raise(value);
    }

    public void SetEffectsVolumeSlider(float value)
    {
        playerAudioSettingsSO.sfxVolume = value;
        AudioSettingsManager.instance.SetEffectsVolume(value);
        onEffectsVolumeChangeEvent.Raise(value);
    }

    public void SetAmbienceVolumeSlider(float value)
    {
        playerAudioSettingsSO.ambienceVolume = value;
        AudioSettingsManager.instance.SetAmbienceVolume(value);
        onAmbienceVolumeChangeEvent.Raise(value);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
    }

    public void LoadPauseMenu()
    {
        pauseMenuGO.SetActive(true);
        optionsMenuGO.SetActive(false);

        inputManager.EnableCursor();

        Time.timeScale = 0f;

        isGamePaused = true;
    }

    private void Resume()
    {
        pauseMenuGO.SetActive(false);
        optionsMenuGO.SetActive(false);

        inputManager.DisableCursor();

        Time.timeScale = 1f;

        isGamePaused = false;
    }
}
 */