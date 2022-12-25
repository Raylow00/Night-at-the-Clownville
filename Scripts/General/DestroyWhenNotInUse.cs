using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenNotInUse : MonoBehaviour
{
    [SerializeField] private bool toUseOnStart = false;
    [SerializeField] private bool toUseOnEnable = false;
    [SerializeField] private bool toDisableInstead;
    [SerializeField] private float destroyAfterThisTime = 20f;

    void Start()
    {
        if(toUseOnStart) Invoke("DestroyObject", destroyAfterThisTime);
    }

    void OnEnable()
    {
        if(toUseOnEnable) Invoke("DestroyObject", destroyAfterThisTime);
    }

    public void DestroyObject()
    {
        if(toDisableInstead) gameObject.SetActive(false);
        else
        {
            Destroy(gameObject);
        }
    }
}
