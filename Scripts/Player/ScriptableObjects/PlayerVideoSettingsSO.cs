using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerVideoSettings", menuName = "Player/VideoSettings")]
public class PlayerVideoSettingsSO : ScriptableObject
{
    public bool fullScreenMode;
    public int resolutionScale;
    public int qualityIndex;
}
