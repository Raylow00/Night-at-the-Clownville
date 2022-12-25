using System;

public class Timer
{
    public float remainingSeconds { get; private set; }
    public event Action onTimerEnd;

    public Timer(float duration)
    {
        remainingSeconds = duration;
    }

    public void TickDown(float deltaTime)
    {
        if(remainingSeconds == 0f) return;

        remainingSeconds -= deltaTime;

        CheckForTimerEnd();
    }

    private void CheckForTimerEnd()
    {
        if(remainingSeconds > 0f) return;

        remainingSeconds = 0f;

        onTimerEnd?.Invoke();
    }
}
