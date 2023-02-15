using UnityEngine;
using UnityEngine.Rendering;

public class ScreenshotHandler : MonoBehaviour
{
    #region Private Fields
    private Camera vCamera;
    private bool toTakeScreenshotOnNextFrame;
    private bool isScreenshotTaken;
    private int screenshotIndex;
    private string lastSavedPath;
    #endregion

    void Start()
    {
        ScreenshotHandlerInit();
    }

    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }

    #region Public Methods
    public void SetCamera(Camera arg_camera)
    {
        vCamera = arg_camera;
    }

    /// <summary>
    ///     Get whether screenshot is taken
    /// </summary>
    /// <returns>
    ///     isScreenshotTaken
    /// </returns>
    public bool GetIsScreenshotTaken()
    {
        return isScreenshotTaken;
    }

    /// <summary>
    ///     Captures, processes and saves the screenshot
    /// </summary>
    /// <param name="width">Screen.width</param>
    /// <param name="height">Screen.height</param>
    public void TakeScreenshot(int width, int height)
    {
        vCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        toTakeScreenshotOnNextFrame = true;
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Init function for screenshot handler
    /// </summary>
    private void ScreenshotHandlerInit()
    {
        vCamera = Camera.main;
        isScreenshotTaken = false;
    }

    /// <summary>
    ///     Utility function for camera rendering pipeline
    /// </summary>
    /// <param name="context"></param>
    /// <param name="camera"></param>
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }

    /// <summary>
    ///     Override function OnPostRender responsible for taking screenshot
    /// </summary>
    private void OnPostRender()
    {
        if (toTakeScreenshotOnNextFrame)
        {
            toTakeScreenshotOnNextFrame = false;

            RenderTexture renderTexture = vCamera.targetTexture;
            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraSreenshot_00" + screenshotIndex + ".png", byteArray);
            screenshotIndex++;
            RenderTexture.ReleaseTemporary(renderTexture);
            vCamera.targetTexture = null;
            lastSavedPath = Application.dataPath + "/CameraSreenshot_00" + screenshotIndex + ".png";
            Debug.Log("Screenshot saved at: " + lastSavedPath);

            isScreenshotTaken = true;
        }
    }
    #endregion
}
