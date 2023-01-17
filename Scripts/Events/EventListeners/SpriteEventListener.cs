using UnityEngine;

public class SpriteEventListener : BaseGameEventListener<Sprite, SpriteEvent, UnitySpriteEventResponse>
{
    #region Private Fields
    private bool hasReceivedEvent = false;
    #endregion

    /// <summary>
    ///     Acknowledge Sprite data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent(Sprite arg_value)
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + "; Value: " + arg_value);
    }
}
