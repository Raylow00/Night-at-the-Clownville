using UnityEngine;

public class Hintable : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private string hint;
    #endregion

    #region Public Methods
    /// <summary>
    ///     Gets the hint in string
    /// </summary>
    /// <returns>
    ///     Hint tag in string
    /// </returns>
    public string GetHint()
    {
        return hint;
    }

    /// <summary>
    ///     Sets the hint tag 
    /// </summary>
    /// <param name="value">
    ///     Value in string to set as hint
    /// </param>
    public void SetHint(string value)
    {
        hint = value;
    }
    #endregion
}
