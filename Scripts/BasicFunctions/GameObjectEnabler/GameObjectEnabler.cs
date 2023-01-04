using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEnabler : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private bool toEnableOnStart;
    [SerializeField] private bool toDisableOnStart;
    [SerializeField] private float enableAfterThisTime;
    [SerializeField] private float disableAfterThisTime;
    [SerializeField] private GameObject targetGameObject;
    #endregion

    void Start()
    {
        if (toEnableOnStart)
        {
            targetGameObject.SetActive(true);
        }
        else if (toDisableOnStart)
        {
            targetGameObject.SetActive(false);
        }
    }

    #region Public Methods
    public GameObject GetTargetGameObject()
    {
        return targetGameObject;
    }

    public void EnableGameObject()
    {
        if (enableAfterThisTime != 0f)
        {
            StartCoroutine(EnableGameObjectAfterThisTime(enableAfterThisTime));
        }
        else
        {
            targetGameObject.SetActive(true);
        }
    }

    public void DisableGameObject()
    {
        if (disableAfterThisTime != 0f)
        {
            StartCoroutine(DisableGameObjectAfterThisTime(disableAfterThisTime));
        }
        else
        {
            targetGameObject.SetActive(false);
        }
    }
    #endregion

    #region Private Methods
    private IEnumerator EnableGameObjectAfterThisTime(float arg_enableAfterThisTime)
    {
        yield return new WaitForSeconds(arg_enableAfterThisTime);

        targetGameObject.SetActive(true);
    }

    private IEnumerator DisableGameObjectAfterThisTime(float arg_disableAfterThisTime)
    {
        yield return new WaitForSeconds(arg_disableAfterThisTime);

        targetGameObject.SetActive(false);
    }
    #endregion
}
