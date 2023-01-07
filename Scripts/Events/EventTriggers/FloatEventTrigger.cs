using UnityEngine;

public class FloatEventTrigger : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private bool toTriggerOnStart;
    [SerializeField] private float floatToSend;
    [SerializeField] private FloatEvent floatEvent;
    #endregion

    public void Start()
    {
        if (toTriggerOnStart)
        {
            TriggerEvent(floatToSend);
        }
    }

    /// <summary>
    ///     Triggers the float event and send the float out
    /// </summary>
    /// <param name="arg_floatToSend"></param>
    public void TriggerEvent(float arg_floatToSend)
    {
        floatEvent.Raise(arg_floatToSend);
    }
}
