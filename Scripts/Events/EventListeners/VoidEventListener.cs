using UnityEngine;

public class VoidEventListener : BaseGameEventListener<Void, VoidEvent, UnityVoidEventResponse>
{
    #region Private Fields
    private bool hasReceivedEvent = false;
    #endregion

    /// <summary>
    ///     Acknowledge void data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent()
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + ";");
    }
}