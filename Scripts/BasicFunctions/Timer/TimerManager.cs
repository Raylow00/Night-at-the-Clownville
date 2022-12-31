using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private float timerDuration;
    [SerializeField] private bool isTimerCyclic;
    [SerializeField] private VoidEvent VoidEvent_OnTimerEndEvent;
    #endregion

    #region Private Fields
    private Timer timer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer(timerDuration, VoidEvent_OnTimerEndEvent, isTimerCyclic);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick(Time.deltaTime);
    }

    #region Public Methods
    public float GetTimerDuration()
    {
        return timer.GetTimerDuration();
    }
    #endregion
}
