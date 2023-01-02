using UnityEngine;

public class Breakable : MonoBehaviour, IBreakable
{
    #region Serialized Fields
    [SerializeField] private GameObject brokenObject;
    #endregion

    #region Private Fields
    private bool isBroken;
    #endregion

    #region Public Methods
    /// <summary>
    ///     Gets whether the object is broken
    /// </summary>
    /// <returns>
    ///     True if broken
    ///     False otherwise
    /// </returns>
    public bool GetBoolIsBroken()
    {
        return isBroken;
    }

    /// <summary>
    ///     "Break" object by instantiating the broken version at the same position and with same rotation
    ///     and destroy current object
    /// </summary>
    public void BreakObject()
    {
        if (isBroken == false)
        {
            Instantiate(brokenObject, transform.position, transform.rotation);
            Destroy(gameObject);

            isBroken = true;
        }
    }
    #endregion
}
