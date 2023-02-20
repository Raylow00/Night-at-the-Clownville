using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key
{
    #region Private Fields
    private bool isKeyObtained;
    #endregion

    #region Constructor
    /// <summary>
    ///     Constructor
    ///     Sets isKeyObtained to false
    /// </summary>
    public Key()
    {
        isKeyObtained = false;
    }
    #endregion

    #region Public Methods
    /// <summary>
    ///     Get whether key is obtained
    /// </summary>
    /// <returns>
    ///     True if key is obtained
    ///     False otherwise
    /// </returns>
    public bool GetIsKeyObtained()
    {
        return isKeyObtained;
    }

    /// <summary>
    ///     Sets isKeyObtained to true
    /// </summary>
    /// <returns>
    ///     True if key is obtained
    ///     False otherwise
    /// </returns>
    public bool ObtainKey()
    {
        if (isKeyObtained == false)
        {
            isKeyObtained = true;
        }
        return isKeyObtained;
    }
    #endregion
}
