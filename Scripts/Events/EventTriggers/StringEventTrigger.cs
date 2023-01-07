using UnityEngine;

public class StringEventTrigger : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private bool toTriggerOnStart;
    [SerializeField] private string stringToSend;
    [SerializeField] private StringEvent stringEvent;
    #endregion

    public void Start()
    {
        if (toTriggerOnStart)
        {
            TriggerEvent(stringToSend);
        }
    }

    /// <summary>
    ///     Triggers the string event and send the string out
    /// </summary>
    /// <param name="arg_stringToSend"></param>
    public void TriggerEvent(string arg_stringToSend)
    {
        stringEvent.Raise(arg_stringToSend);
    }
}
