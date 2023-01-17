using UnityEngine;

public class Vector2EventTrigger : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private bool toTriggerOnStart;
    [SerializeField] private Vector2 vector2ToSend;
    [SerializeField] private Vector2Event vector2Event;
    #endregion

    public void Start()
    {
        if (toTriggerOnStart)
        {
            TriggerEvent(vector2ToSend);
        }
    }

    /// <summary>
    ///     Triggers the Vector2 event and send the value out
    /// </summary>
    /// <param name="arg_vector2ToSend"></param>
    public void TriggerEvent(Vector2 arg_vector2ToSend)
    {
        vector2Event.Raise(arg_vector2ToSend);
    }
}
