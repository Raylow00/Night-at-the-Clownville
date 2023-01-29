using UnityEngine;

public class Health : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private TextViewer textViewer;
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

        // Sets the current health on UI
        if (textViewer != null)
        {
            textViewer.SetText(currHealth);
        }
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
    public void AddHealth(float arg_increment)
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
        }

        // Sets the current health on UI
        if (textViewer != null)
        {
            textViewer.SetText(currHealth);
        }
    }

    /// <summary>
    ///     Decrement value from health if not already zero
    /// </summary>
    /// <param name="arg_damage"></param>
    public void TakeDamage(float arg_damage)
    {
        if (isHealthZero)
        {
            return;
        }
        else
        {
            currHealth -= arg_damage;
            if (currHealth <= 0f)
            {
                isHealthZero = true;
                currHealth = 0f;
            }

            if (isHealthZero)
            {
                return;
            }
        }

        // Sets the current health on UI
        if (textViewer != null)
        {
            textViewer.SetText(currHealth);
        }
    }

}
