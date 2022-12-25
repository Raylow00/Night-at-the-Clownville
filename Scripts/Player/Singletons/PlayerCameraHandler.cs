using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class PlayerCameraHandler : MonoBehaviour
{
    private static PlayerCameraHandler instance;

    [Header("Camera GameObject")]
    [SerializeField] private GameObject cameraGO;
    private Camera playerCamera;
    private bool takeScreenshotOnNextFrame;

    [Header("Crosshair UI")]
    [SerializeField] private GameObject crosshairUI;

    [Header("Camera UI")]
    [SerializeField] private GameObject cameraUI;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("VFX")]
    [SerializeField] private GameObject flashImageGO;
    [SerializeField] private Image flashImage;
    [SerializeField] private float flashSpeed = 5f;
    [SerializeField] private Color flashColor = new Color(1f, 1f, 1f, 1f);
    
    [Header("SFX")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string cameraEquipClipName;
    [SerializeField] private string cameraUnequipClipName;
    [SerializeField] private string cameraCaptureClipName;
    
    [Header("Weapon Handler")]
    [SerializeField] private PlayerWeaponHandler weaponHandler;

    private int i = 0;
    public bool isCameraEquipped;
    public bool isScreenshotTaken;

    void Awake()
    {   
        playerCamera = Camera.main;
    }

    void Start()
    {
        animator.SetTrigger("unequip");
        flashImageGO.SetActive(false);
        cameraUI.SetActive(false);
    }

    public void EquipCamera()
    {
        if(!cameraGO.activeSelf)
        {
            if(weaponHandler.CurrentWeaponGO.activeSelf) weaponHandler.CurrentWeaponGO.SetActive(false);
            crosshairUI.SetActive(false);

            cameraGO.SetActive(true);
            cameraUI.SetActive(true);
            isCameraEquipped = true;
            isScreenshotTaken = false;
        }
        else
        {
            if(!weaponHandler.CurrentWeaponGO.activeSelf) weaponHandler.CurrentWeaponGO.SetActive(true);
            crosshairUI.SetActive(true);

            animator.SetTrigger("unequip");
            isCameraEquipped = false;
            isScreenshotTaken = false;
            flashImageGO.SetActive(false);
        }
    }

    public void DisableCamera()
    {
        cameraGO.SetActive(false);
        cameraUI.SetActive(false);

        crosshairUI.SetActive(true);
    }

    public void PlayCameraAudio(string cameraAudioName)
    {
        if(!string.IsNullOrEmpty(cameraAudioName)) audioManager.PlayAudio(cameraAudioName);
    }

    public void TakeScreenshot(int width, int height)
    {
        // Debug.Log("Taking screenshot");
        playerCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }

    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }

    private void OnPostRender()
    {
        if(takeScreenshotOnNextFrame && cameraGO.activeSelf)
        {
            // Debug.Log("Processing screenshot");

            takeScreenshotOnNextFrame = false;

            RenderTexture renderTexture = playerCamera.targetTexture;
            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraSreenshot_00" + i + ".png", byteArray);
            i++;
            //System.IO.File.WriteAllBytes(Application.persistentDataPath + "/CameraSreenshot_00" + i + ".png", byteArray);

            PlayCameraAudio(cameraCaptureClipName);

            StartCoroutine(PlayCameraCaptureVFX());
            
            // Debug.Log("Screenshot saved");

            RenderTexture.ReleaseTemporary(renderTexture);
            playerCamera.targetTexture = null;

            isScreenshotTaken = true;
        }
    }

    private IEnumerator PlayCameraCaptureVFX()
    {
        flashImageGO.SetActive(true);

        flashImage.color = flashColor;

        yield return new WaitForSeconds(flashSpeed);

        flashImage.color = Color.clear;

        flashImageGO.SetActive(false);
    }
}
