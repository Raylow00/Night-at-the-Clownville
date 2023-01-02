using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGameObject
{
    private bool isLockOpened;

    public LockGameObject()
    {
        isLockOpened = false;
    }

    public bool GetIsLockOpened()
    {
        return isLockOpened;
    }

    public bool OpenLock()
    {
        if (isLockOpened == false)
        {
            isLockOpened = true;
        }

        return isLockOpened;
    }

    public bool CloseLock()
    {
        if (isLockOpened != false)
        {
            isLockOpened = false;
        }

        return isLockOpened;
    }
}
