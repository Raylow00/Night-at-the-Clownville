using UnityEngine;

public class Breakable : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private GameObject brokenObject;
    #endregion

    #region Public Methods
    /// <summary>
    ///     Gets the broken version of the object
    /// </summary>
    /// <returns>
    ///     Broken object
    /// </returns>
    public GameObject GetBrokenObject()
    {
        return brokenObject;
    }

    /// <summary>
    ///     "Break" object by instantiating the broken version at the same position and with same rotation
    ///     and destroy current object
    /// </summary>
    public void BreakObject()
    {
        Instantiate(brokenObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    #endregion
}
