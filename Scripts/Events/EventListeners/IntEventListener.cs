using UnityEngine;

public class IntEventListener : BaseGameEventListener<int, IntEvent, UnityIntEventResponse>
{
    #region Private Fields
    private bool hasReceivedEvent = false;
    #endregion

    /// <summary>
    ///     Acknowledge int data received
    /// </summary>
    /// <param name="arg_value"></param>
    public void AcknowledgeEvent(int arg_value)
    {
        hasReceivedEvent = true;
        Debug.Log("Received event: " + hasReceivedEvent + "; Value: " + arg_value);
    }
}