using UnityEngine;

public class VoidEventListener : BaseGameEventListener<Void, VoidEvent, UnityVoidEventResponse>
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
    ///     Acknowledge void data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent()
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + ";");
    }
}