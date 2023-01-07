using UnityEngine;

public class VoidEventTrigger : MonoBehaviour
{
    [SerializeField] private bool toTriggerOnStart;
    [SerializeField] private VoidEvent voidEvent;

    public void Start()
    {
        if (toTriggerOnStart)
        {
            TriggerEvent();
        }
    }

    public void TriggerEvent()
    {
        voidEvent.Raise();
    }
}
