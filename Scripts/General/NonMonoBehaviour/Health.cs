using UnityEngine;

public class Health : IDamageable
{
    public int maxHealth { get; private set; }
    public int currentHealth { get; private set; }
    public FloatEvent onHealthChangeEvent;
    public VoidEvent onHealthZeroEvent;

    public Health(int curHealth, int mHealth, FloatEvent healthChangeEvent, VoidEvent healthZeroEvent)
    {
        currentHealth = curHealth;

        maxHealth = mHealth;

        CheckIfMax();

        onHealthChangeEvent = healthChangeEvent;

        onHealthZeroEvent = healthZeroEvent;
    }

    public void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitNormal, bool isMeleeWeapon=false, bool toBroadcast=true)
    {
        currentHealth -= damage;

        if(onHealthChangeEvent != null && toBroadcast) 
            onHealthChangeEvent.Raise(currentHealth);

        CheckIfDead();
    }

    public void ReceiveHealth(int increment, bool toBroadcast=true)
    {
        currentHealth += increment;

        CheckIfMax();

        if(onHealthChangeEvent != null && toBroadcast) 
            onHealthChangeEvent.Raise(currentHealth);
    }

    public void CheckIfDead()
    {
        if(currentHealth > 0) return;

        currentHealth = 0;

        if(onHealthChangeEvent != null) onHealthChangeEvent.Raise(currentHealth);

        if(onHealthZeroEvent != null) onHealthZeroEvent.Raise();
    }

    private void CheckIfMax()
    {
        if(currentHealth <= maxHealth) return;

        currentHealth = maxHealth;

        if(onHealthChangeEvent != null) onHealthChangeEvent.Raise(currentHealth);
    }
}
