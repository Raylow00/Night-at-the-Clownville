using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGameObject
{
    private bool isKeyObtained;

    public KeyGameObject()
    {
        isKeyObtained = false;
    }

    public bool GetIsKeyObtained()
    {
        return isKeyObtained;
    }

    public bool ObtainKey()
    {
        if (isKeyObtained == false)
        {
            isKeyObtained = true;
        }
        return isKeyObtained;
    }
}
