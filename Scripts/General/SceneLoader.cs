using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneLoader : MonoBehaviour, IInteractable
{
    [Header("Scene name")]
    [SerializeField] private string sceneName;

    [Header("Scene Load Crossfade")]
    [SerializeField] private GameObject crossfadeCanvas;

    [Header("Player Stats SO")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Fade music")]
    [SerializeField] private AudioMixer masterAudioMixer;
    [SerializeField] private string masterAudioMixerExposedParam;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeTargetVolume;

    [Header("Player weapon handler")]
    [SerializeField] private PlayerWeaponHandler weaponHandler;

    [Header("Player empty hands and enabler")]
    [SerializeField] private GameObject playerEmptyHands;

    [Header("Scene Load Animation Background Video")]
    [SerializeField] private GameObject sceneLoadAnimationBgVideo;

    [Header("UI Elements to disable during load")]
    [SerializeField] private GameObject hudGO;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private GameObject crosshairGO;

    void Start()
    {
        // If scene changes, mixer group will fade to 0
        // This coroutine ensures mixer group audio goes back to 1 at the start of the scene
        StartCoroutine(FadeMixerGroup.StartFade(masterAudioMixer, masterAudioMixerExposedParam, fadeDuration, 1f));
    }

    public void PressInteractiveButton()
    {
        if(playerStats.lastSceneNameSaved == false) SavePlayerPositionAndSceneName();
        StartCoroutine(StartLoadScene(sceneName));
    }

    public void HoldInteractiveButton()
    {
        // Not used
    }

    public void CancelHoldInteractiveButton()
    {
        // not used
    }

    private IEnumerator StartLoadScene(string sceneName)
    {
        // unequip player weapons
        weaponHandler.UnloadWeapon();

        yield return new WaitForSeconds(1f);

        // enable player empty hands - anim triggered on enable
        playerEmptyHands.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        // fade audio
        StartCoroutine(FadeMixerGroup.StartFade(masterAudioMixer, masterAudioMixerExposedParam, fadeDuration, fadeTargetVolume));
        // load next scene
        StartCoroutine(LoadNextScene(sceneName));
        
    }

    private void SavePlayerPositionAndSceneName()
    {
        playerStats.playerPosX = GameObject.FindWithTag("Player").transform.position.x;
        playerStats.playerPosY = 2.34f;
        playerStats.playerPosZ = GameObject.FindWithTag("Player").transform.position.z;
        playerStats.lastSceneName = SceneManager.GetActiveScene().name;
        playerStats.lastSceneNameSaved = true;

        // Debug.Log("Saved player position from last scene [" + playerStats.lastSceneName + "]: (" + playerStats.playerPosX + ", " + playerStats.playerPosY + ", " + playerStats.playerPosZ + ")");
    }

    private IEnumerator LoadNextScene(string sceneName)
    {
        crossfadeCanvas.GetComponent<Animator>().SetTrigger("crossfade_start");

        yield return new WaitForSeconds(2f);

        playerEmptyHands.SetActive(false);
        cameraUI.SetActive(false);
        hudGO.SetActive(false);
        crosshairGO.SetActive(false);

        yield return new WaitForSeconds(2f);

        sceneLoadAnimationBgVideo.GetComponent<UnityEngine.Video.VideoPlayer>().Play();

        crossfadeCanvas.GetComponent<Animator>().SetTrigger("crossfade_end");

        yield return new WaitForSeconds(10f);

        crossfadeCanvas.GetComponent<Animator>().SetTrigger("crossfade_start");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName);

        // // The Application loads the Scene in the background as the current Scene runs.
        // // This is particularly good for creating loading screens.
        // // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // // a sceneBuildIndex of 1 as shown in Build Settings.
        // AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // // Wait until the asynchronous scene fully loads
        // while (!asyncLoad.isDone)
        // {
        //     yield return null;
        // }
    }
}
