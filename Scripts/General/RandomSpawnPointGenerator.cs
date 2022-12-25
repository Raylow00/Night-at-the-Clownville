using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPointGenerator : MonoBehaviour
{
    [Header("Name (Not used)")]
    [SerializeField] private string name;

    [Header("Center point")]
    [SerializeField] private Transform centerPoint;

    [Header("Size of box")]
    [SerializeField] private Vector3 size;

    public Vector3 SpawnRandomPointsInBox()
    {
        return centerPoint.position + new Vector3(
            (Random.value - 0.5f) * size.x,
            (Random.value - 0.5f) * size.y,
            (Random.value - 0.5f) * size.z
        );
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(centerPoint.position, size);
    }
}
