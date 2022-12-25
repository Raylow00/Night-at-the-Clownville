using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer lampRenderer;
    [SerializeField] private Material lightsOffMaterial;
    [SerializeField] private Material lightsOnMaterial;

    public void SwitchToLightsOffMaterial()
    {
        Material[] materials = lampRenderer.materials;
        int materialCount = materials.Length;
        materials[materialCount-1] = lightsOffMaterial;
        lampRenderer.materials = materials;
    }

    public void SwitchToLightsOnMaterial()
    {
        Material[] materials = lampRenderer.materials;
        int materialCount = materials.Length;
        materials[materialCount-1] = lightsOnMaterial;
        lampRenderer.materials = materials;
    }
}
