using UnityEngine;

public class Energy : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private TextViewer textViewer;
    #endregion

    #region Private Fields
    // These are not in a scriptable object because
    // there could be multiple enemies
    // and it would require one scriptable object for each entity
    private float currEnergy;
    private float maxEnergy = 100;
    private bool isEnergyZero;
    #endregion

    void Start()
    {
        currEnergy = maxEnergy;
        isEnergyZero = false;

        // Sets the current energy on UI
        if (textViewer != null)
        {
            textViewer.SetText(currEnergy);
        }
    }

    /// <summary>
    ///     Gets the current energy
    /// </summary>
    /// <returns></returns>
    public float GetCurrentEnergy()
    {
        return currEnergy;
    }

    /// <summary>
    ///     Returns whether energy is zero
    /// </summary>
    /// <returns>
    ///     True if zero
    ///     False otherwise
    /// </returns>
    public bool GetEnergyZero()
    {
        return isEnergyZero;
    }

    /// <summary>
    ///     Add value to energy if not already maximum
    /// </summary>
    /// <param name="arg_increment"></param>
    public void IncreaseEnergy(float arg_increment)
    {
        if (currEnergy >= maxEnergy)
        {
            return;
        }
        else
        {
            currEnergy += arg_increment;
            if (currEnergy >= maxEnergy)
            {
                currEnergy = maxEnergy;
            }
        }

        // Sets the current health on UI
        if (textViewer != null)
        {
            textViewer.SetText(currEnergy);
        }
    }

    /// <summary>
    ///     Decrement value from energy if not already zero
    /// </summary>
    /// <param name="arg_damage"></param>
    public void DecreaseEnergy(float arg_damage)
    {
        if (isEnergyZero)
        {
            return;
        }
        else
        {
            currEnergy -= arg_damage;
            if (currEnergy <= 0f)
            {
                isEnergyZero = true;
                currEnergy = 0f;
            }

            if (isEnergyZero)
            {
                return;
            }
        }

        // Sets the current energy on UI
        if (textViewer != null)
        {
            textViewer.SetText(currEnergy);
        }
    }
}
