using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEventListener : BaseGameEventListener<float, FloatEvent, UnityFloatEventResponse>
{
    #region Private Fields
    private bool hasReceivedEvent = false;
    #endregion

    /// <summary>
    ///     Get the acknowledgement of whether event is received
    /// </summary>
    /// <returns></returns>
    public bool GetAcknowledgement()
    {
        return hasReceivedEvent;
    }

    /// <summary>
    ///     Acknowledge float data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent(float arg_value)
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + "; Value: " + arg_value);
    }
}