using UnityEngine;

public enum VideoFullScreenMode
{
    FULLSCREEN_EXCLUSIVE_FULLSCREEN,    // 0
    FULLSCREEN_FULLSCREEN_WINDOW,       // 1
    FULLSCREEN_MAXIMIZED_WINDOW,        // 2
    FULLSCREEN_WINDOWED                 // 3
}

public enum VideoQuality
{
    VIDEO_QUALITY_LOW,                  // 0
    VIDEO_QUALITY_MEDIUM,               // 1
    VIDEO_QUALITY_HIGH                  // 2
}

[CreateAssetMenu(menuName = "Utility/Video/VideoSettings", fileName = "VideoSettings")]
public class VideoSettingsScriptableObject : ScriptableObject
{
    public int default_FullScreenMode
    {
        get => (int)FullScreenMode.ExclusiveFullScreen;
    }
    public int fullScreenMode;
    public bool default_FullScreen
    {
        get => true;
    }
    public bool fullScreen;
    public int default_VideoResolutionScale
    {
        get => -1;
    }
    public int videoResolutionScale;
    public int default_VideoQualityIndex
    {
        get => (int)VideoQuality.VIDEO_QUALITY_HIGH;
    }
    public int videoQualityIndex;
}
