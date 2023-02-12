using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private TextViewer textViewer;
    #endregion

    #region Private Fields
    private float currCoin;
    private float maxCoin = 100;
    private bool isCoinZero;
    #endregion

    void Start()
    {
        currCoin = maxCoin;
        isCoinZero = false;

        // Sets the current coin on UI
        if (textViewer != null)
        {
            textViewer.SetText(currCoin);
        }
    }

    /// <summary>
    ///     Gets the current coin
    /// </summary>
    /// <returns></returns>
    public float GetCurrentCoin()
    {
        return currCoin;
    }

    /// <summary>
    ///     Returns whether coin is zero
    /// </summary>
    /// <returns>
    ///     True if zero
    ///     False otherwise
    /// </returns>
    public bool GetCoinZero()
    {
        return isCoinZero;
    }

    /// <summary>
    ///     Add value to coin if not already maximum
    /// </summary>
    /// <param name="arg_increment"></param>
    public void AddCoin(float arg_increment)
    {
        if (currCoin >= maxCoin)
        {
            return;
        }
        else
        {
            currCoin += arg_increment;
            if (currCoin >= maxCoin)
            {
                currCoin = maxCoin;
            }
        }

        // Sets the current coin on UI
        if (textViewer != null)
        {
            textViewer.SetText(currCoin);
        }
    }

    /// <summary>
    ///     Decrement value from coin if not already zero
    /// </summary>
    /// <param name="arg_decrement"></param>
    public void UseCoin(float arg_decrement)
    {
        if (isCoinZero)
        {
            return;
        }
        else
        {
            currCoin -= arg_decrement;
            if (currCoin <= 0f)
            {
                isCoinZero = true;
                currCoin = 0f;
            }

            if (isCoinZero)
            {
                return;
            }
        }

        // Sets the current coin on UI
        if (textViewer != null)
        {
            textViewer.SetText(currCoin);
        }
    }
}
