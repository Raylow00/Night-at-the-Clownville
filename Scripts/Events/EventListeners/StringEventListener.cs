using UnityEngine;

public class StringEventListener : BaseGameEventListener<string, StringEvent, UnityStringEventResponse>
{
    #region Private Fields
    private bool hasReceivedEvent = false;
    #endregion

    /// <summary>
    ///     Acknowledge string data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent(string arg_value)
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + "; Value: " + arg_value);
    }
}