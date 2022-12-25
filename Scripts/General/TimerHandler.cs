using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerHandler : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private UnityEvent onTimerEnd = null;

    private Timer timer;

    void Start()
    {
        timer = new Timer(duration);
        timer.onTimerEnd += HandleTimerEnd;
    }

    void Update()
    {
        timer.TickDown(Time.deltaTime);
    }

    public void HandleTimerEnd()
    {
        // Debug.Log("Time is up!!!");
    }
}
