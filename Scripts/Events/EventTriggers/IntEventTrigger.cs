using UnityEngine;

public class IntEventTrigger : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private bool toTriggerOnStart;
    [SerializeField] private int intToSend;
    [SerializeField] private IntEvent intEvent;
    #endregion

    public void Start()
    {
        if (toTriggerOnStart)
        {
            TriggerEvent(intToSend);
        }
    }

    /// <summary>
    ///     Triggers the integer event and send out the integer
    /// </summary>
    /// <param name="arg_intToSend"></param>
    public void TriggerEvent(int arg_intToSend)
    {
        intEvent.Raise(arg_intToSend);
    }
}
