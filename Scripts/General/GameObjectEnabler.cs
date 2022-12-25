using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEnabler : MonoBehaviour
{
    [SerializeField] private bool enableGameObjectOnStart;
    [SerializeField] private bool enableGameObjectOnEnable;
    [SerializeField] private bool disableGameObjectOnEnable;
    [SerializeField] private GameObject targetGameObject;

    void Start()
    {
        if(enableGameObjectOnStart) targetGameObject.SetActive(true);
    }

    void OnEnable()
    {
        if(enableGameObjectOnEnable) targetGameObject.SetActive(true);
        else if(disableGameObjectOnEnable) targetGameObject.SetActive(false);
    }

    public void EnableGameObject()
    {
        targetGameObject.SetActive(true);
    }

    public void DisableGameObject()
    {
        targetGameObject.SetActive(false);
    }
}
