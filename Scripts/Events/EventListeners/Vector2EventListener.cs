using UnityEngine;

public class Vector2EventListener : BaseGameEventListener<Vector2, Vector2Event, UnityVector2EventResponse>
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
    ///     Acknowledge Vector2 data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent(Vector2 arg_value)
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + "; Value: " + arg_value);
    }
}