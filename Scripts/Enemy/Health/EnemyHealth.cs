using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private FloatEvent onHealthChangeEvent;
    [SerializeField] private VoidEvent onHealthZeroEvent;
    #endregion

    #region Private Fields
    // These are not in a scriptable object because
    // there could be multiple enemies
    // and it would require one scriptable object for each enemy
    private float currHealth;
    private float maxHealth = 100;
    private bool isHealthZero;
    #endregion

    void Start()
    {
        currHealth = maxHealth;
        isHealthZero = false;
        onHealthChangeEvent.Raise(currHealth);
    }

    /// <summary>
    ///     Gets the current health
    /// </summary>
    /// <returns></returns>
    public float GetCurrentHealth()
    {
        return currHealth;
    }

    /// <summary>
    ///     Returns whether health is zero
    /// </summary>
    /// <returns>
    ///     True if zero
    ///     False otherwise
    /// </returns>
    public bool GetHealthZero()
    {
        return isHealthZero;
    }

    /// <summary>
    ///     Add value to health if not already maximum
    /// </summary>
    /// <param name="arg_increment"></param>
    public void AddHealth(int arg_increment)
    {
        if (currHealth >= maxHealth)
        {
            return;
        }
        else
        {
            currHealth += arg_increment;
            if (currHealth >= maxHealth)
            {
                currHealth = maxHealth;
            }

            onHealthChangeEvent.Raise(currHealth);
        }
    }

    /// <summary>
    ///     Decrement value from health if not already zero
    /// </summary>
    /// <param name="arg_damage"></param>
    public void TakeDamage(int arg_damage)
    {
        if (isHealthZero)
        {
            onHealthZeroEvent.Raise();
            return;
        }
        else
        {
            currHealth -= arg_damage;
            if (currHealth <= 0)
            {
                isHealthZero = true;
                currHealth = 0;
            }

            if (isHealthZero)
            {
                onHealthZeroEvent.Raise();
            }

            onHealthChangeEvent.Raise(currHealth);
        }
    }

}
