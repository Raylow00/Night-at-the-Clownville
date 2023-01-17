using UnityEngine;

public class BoolEventListener : BaseGameEventListener<bool, BoolEvent, UnityBoolEventResponse>
{
    #region Private Fields
    private bool hasReceivedEvent = false;
    #endregion

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