using UnityEngine;

public class BoolEventListener : BaseGameEventListener<bool, BoolEvent, UnityBoolEventResponse>
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
    ///     Acknowledge boolean data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent(bool arg_value)
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + "; Value: " + arg_value);
    }
}