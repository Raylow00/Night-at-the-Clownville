using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGameObject
{
    #region Private Fields
    private bool isLockOpened;
    #endregion

    #region Constructor
    /// <summary>
    ///     Constructor
    ///     Sets isLockOpened to false
    /// </summary>
    public LockGameObject()
    {
        isLockOpened = false;
    }
    #endregion

    #region Public Methods
    /// <summary>
    ///     Get whether lock is opened
    /// </summary>
    /// <returns>
    ///     True if lock is opened
    ///     False otherwise
    /// </returns>
    public bool GetIsLockOpened()
    {
        return isLockOpened;
    }

    /// <summary>
    ///     Sets lock to open state
    /// </summary>
    /// <returns>
    ///     True if lock is opened
    ///     False otherwise
    /// </returns>
    public bool OpenLock()
    {
        if (isLockOpened == false)
        {
            isLockOpened = true;
        }

        return isLockOpened;
    }

    /// <summary>
    ///     Sets lock to close state
    /// </summary>
    /// <returns>
    ///     True if lock is opened
    ///     False otherwise
    /// </returns>
    public bool CloseLock()
    {
        if (isLockOpened != false)
        {
            isLockOpened = false;
        }

        return isLockOpened;
    }
    #endregion
}
