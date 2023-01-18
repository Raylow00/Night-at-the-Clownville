using UnityEngine;

public class BoolEventTrigger : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private bool toTriggerOnStart;
    [SerializeField] private bool boolToSend;
    [SerializeField] private BoolEvent boolEvent;
    #endregion

    public void Start()
    {
        if (toTriggerOnStart)
        {
            TriggerEvent(boolToSend);
        }
    }

    /// <summary>
    ///     Triggers the float event and send the float out
    /// </summary>
    /// <param name="arg_boolToSend"></param>
    public void TriggerEvent(bool  arg_boolToSend)
    {
        boolEvent.Raise(arg_boolToSend);
    }
}
