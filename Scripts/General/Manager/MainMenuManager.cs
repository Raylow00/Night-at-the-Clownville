using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [Header("Input Manager")]
    private PlayerInputManager inputManager;

    [Header("New Game")]
    [SerializeField] private string newGameScene;

    [Header("Load Game")]
    [SerializeField] private GameObject loadGamePanel;
    [SerializeField] private TMP_Dropdown savedSlotsDropdown;
    private List<string> savedSlots;
    
    [Header("Save Game")]
    [SerializeField] private GameObject saveGamePanel;

    [Header("Settings")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameplayOptionsPanel;
    [SerializeField] private GameObject videoOptionsPanel;
    [SerializeField] private GameObject audioOptionsPanel;

    [Header("Rebind key")]
    [SerializeField] private StringEvent onFlashlightKeyRemapEvent;
    [SerializeField] private StringEvent onCrouchKeyRemapEvent;
    [SerializeField] private StringEvent onLastWeaponKeyRemapEvent;
    [SerializeField] private StringEvent onWeaponSkinKeyRemapEvent;
    [SerializeField] private StringEvent onCameraEquipKeyRemapEvent;
    [SerializeField] private StringEvent onMapViewKeyRemapEvent;

    [Header("Mouse Sensitivity")]
    [SerializeField] private PlayerKeyBindingsSO playerKeyPreferences;
    [SerializeField] private FloatEvent onSensitivityChangeEvent;
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

    [Header("Video Settings")]
    [SerializeField] private PlayerVideoSettingsSO playerVideoSettingsSO;

    [Header("Fullscreen")]
    [SerializeField] private BoolEvent onFullscreenModeToggleEvent;
    
    [Header("Resolution Scale")]
    [SerializeField] private IntEvent onResolutionScaleChangeEvent;

    [Header("Quality Index")]
    [SerializeField] private IntEvent onQualityIndexChangeEvent;

    [Header("Credit")]
    [SerializeField] private string creditScene;

    [Header("Mission Reset Handler")]
    [SerializeField] private MissionSOResetHandler missionResetHandler;

    [Header("Player Stats Reset Handler")]
    [SerializeField] private PlayerStatsResetHandler playerStatsResetHandler;

    [Header("Scene Load Crossfade")]
    [SerializeField] private GameObject crossfadeCanvas;

    [Header("Fade music")]
    [SerializeField] private AudioMixer masterAudioMixer;
    [SerializeField] private string masterAudioMixerExposedParam;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeTargetVolume;

    public static bool isGamePaused = false;
    private int savedSlotIndex = 0;

    public void OnPauseButtonPressed()
    {
        inputManager = GameObject.Find("Player").GetComponent<PlayerInputManager>();

        if(isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void OpenLoadGamePanel()
    {
        // Debug.Log("Loading last saved game...");

        loadGamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);

        savedSlots = GameSaveManager.instance.ListSavedSlots();
        savedSlotsDropdown.AddOptions(savedSlots);
        int currentSavedSlot = 0;
        savedSlotsDropdown.value = currentSavedSlot;
        savedSlotsDropdown.RefreshShownValue();
    }

    public void StoreLoadProfileIndex(int index)
    {
        savedSlotIndex = index;
    }

    public void LoadGame()
    {
        string savedSlot = savedSlots[savedSlotIndex];

        GameSaveManager.instance.LoadGame(savedSlot);
    }

    public void NewGame()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);

        missionResetHandler.ResetMissions();
        playerStatsResetHandler.ResetPlayerStats();

        StartCoroutine(FadeMixerGroup.StartFade(masterAudioMixer, masterAudioMixerExposedParam, fadeDuration, fadeTargetVolume));

        StartCoroutine(LoadNextScene(newGameScene));
    }

    public void PauseGame()
    {
        inputManager.DisplayCursor(true);

        inputManager.toCheckForMouseButtonInput = false;

        Time.timeScale = 0f;

        OpenMainMenu();

        isGamePaused = true;
    }

    public void ResumeGame()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);

        inputManager.enabled = true;
        
        inputManager.SetBindings();

        inputManager.DisplayCursor(false);

        inputManager.toCheckForMouseButtonInput = true;

        Time.timeScale = 1f;

        isGamePaused = false;
    }

    public void OpenMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void OpenSaveGamePanel()
    {
        saveGamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);

        inputManager.DisableGamePlayControlsActions();
    }

    public void SaveGame(string saveSlotName)
    {
        GameSaveManager.instance.SaveGame(saveSlotName);

        saveGamePanel.SetActive(false);

        inputManager.EnableGamePlayControlsActions();

        ResumeGame();
    }

    public void LoadCreditScene()
    {
        // Debug.Log("Loading credit scene...");
    }

    public void QuitWithoutSave()
    {
        // Debug.Log("Quitting without saving game...");

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            isGamePaused = false;
        }

        StartCoroutine(FadeMixerGroup.StartFade(masterAudioMixer, masterAudioMixerExposedParam, fadeDuration, fadeTargetVolume));

        StartCoroutine(QuitRoutine());
    }

    public void QuitWithSave(string saveSlotName)
    {
        SaveGame(saveSlotName);

        // Debug.Log("Quitting after saving...");

        StartCoroutine(FadeMixerGroup.StartFade(masterAudioMixer, masterAudioMixerExposedParam, fadeDuration, fadeTargetVolume));

        StartCoroutine(QuitRoutine());
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        OpenGamePlayOptions();
    }

    public void OpenGamePlayOptions()
    {
        gameplayOptionsPanel.SetActive(true);
        videoOptionsPanel.SetActive(false);
        audioOptionsPanel.SetActive(false);

        onSensitivityChangeEvent.Raise(playerKeyPreferences.mouseSensitivityY);
        onFOVChangeEvent.Raise(playerKeyPreferences.fieldOfView);
        onFlashlightKeyRemapEvent.Raise(playerKeyPreferences.flashlightKey);
        onCrouchKeyRemapEvent.Raise(playerKeyPreferences.crouchKey);
        onLastWeaponKeyRemapEvent.Raise(playerKeyPreferences.switchLastUsedWeaponKey);
        onWeaponSkinKeyRemapEvent.Raise(playerKeyPreferences.switchWeaponSkinKey);
        onCameraEquipKeyRemapEvent.Raise(playerKeyPreferences.equipCameraKey);
        onMapViewKeyRemapEvent.Raise(playerKeyPreferences.viewMapKey);
    }

    public void ModifySensitivity(float value)
    {
        onSensitivityChangeEvent.Raise(value);
    }

    public void ModifyFOV(float value)
    {
        onFOVChangeEvent.Raise(value);
    }

    public void RebindKey_Flashlight(string key)
    {
        onFlashlightKeyRemapEvent.Raise(key);
    }

    public void RebindKey_Crouch(string key)
    {
        onCrouchKeyRemapEvent.Raise(key);
    }

    public void RebindKey_SwitchLastUsedWeapon(string key)
    {
        onLastWeaponKeyRemapEvent.Raise(key);
    }

    public void RebindKey_WeaponSkin(string key)
    {
        onWeaponSkinKeyRemapEvent.Raise(key);
    }

    public void RebindKey_EquipCamera(string key)
    {
        onCameraEquipKeyRemapEvent.Raise(key);
    }

    public void RebindKey_ViewMap(string key)
    {
        onMapViewKeyRemapEvent.Raise(key);
    }

    public void OpenVideoOptions()
    {
        gameplayOptionsPanel.SetActive(false);
        videoOptionsPanel.SetActive(true);
        audioOptionsPanel.SetActive(false);

        // Raise events for video settings
        onResolutionScaleChangeEvent.Raise(playerVideoSettingsSO.resolutionScale);
        onQualityIndexChangeEvent.Raise(playerVideoSettingsSO.qualityIndex);
    }

    public void SetFullScreenMode(bool isFullscreen)
    {
        onFullscreenModeToggleEvent.Raise(isFullscreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        onResolutionScaleChangeEvent.Raise(resolutionIndex);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        onQualityIndexChangeEvent.Raise(qualityIndex);
    }

    public void OpenAudioOptions()
    {
        gameplayOptionsPanel.SetActive(false);
        videoOptionsPanel.SetActive(false);
        audioOptionsPanel.SetActive(true);

        onMasterVolumeChangeEvent.Raise(playerAudioSettingsSO.masterVolume);
        onMusicVolumeChangeEvent.Raise(playerAudioSettingsSO.musicVolume);
        onEffectsVolumeChangeEvent.Raise(playerAudioSettingsSO.sfxVolume);
        onAmbienceVolumeChangeEvent.Raise(playerAudioSettingsSO.ambienceVolume);
    }

    public void SetMasterVolumeSlider(float value)
    {
        onMasterVolumeChangeEvent.Raise(value);
    }

    public void SetMusicVolumeSlider(float value)
    {
        onMusicVolumeChangeEvent.Raise(value);
    }

    public void SetEffectsVolumeSlider(float value)
    {
        onEffectsVolumeChangeEvent.Raise(value);
    }

    public void SetAmbienceVolumeSlider(float value)
    {
        onAmbienceVolumeChangeEvent.Raise(value);
    }

    private IEnumerator LoadNextScene(string sceneName)
    {
        crossfadeCanvas.GetComponent<Animator>().SetTrigger("crossfade_start");

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator QuitRoutine()
    {
        // Debug.Log("[MainMenuManager] Quitting now...");

        crossfadeCanvas.GetComponent<Animator>().SetTrigger("crossfade_start");

        yield return new WaitForSeconds(4f);

        Application.Quit();
    }
}
