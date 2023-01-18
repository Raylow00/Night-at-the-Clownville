using UnityEngine;

public class ColliderEventTrigger : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private bool toTriggerOnStart;
    [SerializeField] private Collider colliderToSend;
    [SerializeField] private ColliderEvent colliderEvent;
    #endregion

    public void Start()
    {
        if (toTriggerOnStart)
        {
            TriggerEvent(colliderToSend);
        }
    }

    /// <summary>
    ///     Triggers the float event and send the float out
    /// </summary>
    /// <param name="arg_colliderToSend"></param>
    public void TriggerEvent(Collider arg_colliderToSend)
    {
        colliderEvent.Raise(arg_colliderToSend);
    }
}
