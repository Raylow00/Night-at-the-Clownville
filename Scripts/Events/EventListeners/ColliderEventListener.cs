using UnityEngine;

public class ColliderEventListener : BaseGameEventListener<Collider, ColliderEvent, UnityColliderEventResponse>
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
    ///     Acknowledge Collider data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent(Collider arg_value)
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + "; Value: " + arg_value);
    }
}