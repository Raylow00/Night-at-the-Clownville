using UnityEngine;

public class Timer
{
    #region Private Fields
    private float timerDuration;
    private VoidEvent VoidEvent_OnTimerEndEvent;
    private bool isTimerCyclic;

    private float temp_timerDuration;
    #endregion

    #region Constructor
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="arg_duration"></param>
    /// <param name="arg_onTimerEndEvent"></param>
    /// <param name="arg_isCyclic"></param>
    public Timer(float arg_duration, VoidEvent arg_onTimerEndEvent, bool arg_isCyclic)
    {
        temp_timerDuration = arg_duration;
        timerDuration = arg_duration;
        VoidEvent_OnTimerEndEvent = arg_onTimerEndEvent;
        isTimerCyclic = arg_isCyclic;
    }
    #endregion

    #region Properties
    /// <summary>
    ///     Get the duration of the timer
    /// </summary>
    /// <returns></returns>
    public float GetTimerDuration()
    {
        return timerDuration;
    }
    #endregion

    #region Public Methods
    /// <summary>
    ///     Ticks down the timer by delta time
    /// </summary>
    /// <param name="arg_deltaTime"></param>
    /// <returns>
    ///     True if timer has ended
    ///     False otherwise
    /// </returns>
    public bool Tick(float arg_deltaTime)
    {
        // Check whether timer has ended in the next cycle
        if (timerDuration == 0f)
        {
            // but if timer is not cyclic
            if (isTimerCyclic == false)
            {
                return true;
            }
            // reset timer if cyclic
            else
            {
                timerDuration = temp_timerDuration;
            }
        }

        // When timer is still active
        timerDuration -= arg_deltaTime;

        CheckIfTimerEnd();

        return false;
    }
    #endregion

    #region Private Methods
    /// <summary>
    ///     Check if timer has ended and if timer is cyclic
    ///     Reset timer duration if cyclic
    /// </summary>
    private void CheckIfTimerEnd()
    {
        if (timerDuration > 0f) return;

        // if timer is not cyclic
        if (isTimerCyclic == false)
        {
            timerDuration = 0f;
        }
        // if timer is cyclic and timer has ended
        else
        {
            timerDuration = temp_timerDuration;
        }

        // Raise event when timer is up no matter cyclic or not
        if (VoidEvent_OnTimerEndEvent != null)
        {
            VoidEvent_OnTimerEndEvent.Raise();
        }
    }
    #endregion
}
