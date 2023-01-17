using UnityEngine;

public class CollisionEventListener : BaseGameEventListener<Collision, CollisionEvent, UnityCollisionEventResponse>
{
    #region Private Fields
    private bool hasReceivedEvent = false;
    #endregion

    /// <summary>
    ///     Acknowledge Collision data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent(Collision arg_value)
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + "; Value: " + arg_value);
    }
}