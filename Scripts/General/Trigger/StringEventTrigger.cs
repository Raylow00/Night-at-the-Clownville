using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringEventTrigger : MonoBehaviour
{
    [SerializeField] private StringEvent eventToTrigger;

    public void PublishMessage(string message)
    {
        // Debug.Log(message);
        eventToTrigger.Raise(message);
    }
}
