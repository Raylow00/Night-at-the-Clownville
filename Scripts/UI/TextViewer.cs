using UnityEngine;
using TMPro;

public class TextViewer : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private TextMeshProUGUI textField;
    #endregion

    /// <summary>
    ///     Gets the text from text field
    /// </summary>
    /// <returns>
    ///     Text in string format
    ///     Note: Float will be rounded up before being converted to string
    /// </returns>
    public string GetText()
    {
        return textField.text;
    }

    /// <summary>
    ///     Displays float text
    /// </summary>
    /// <param name="arg_float"></param>
    public void SetText(float arg_float)
    {
        textField.text = Mathf.RoundToInt(arg_float).ToString();
    }

    /// <summary>
    ///     Displays integer text
    /// </summary>
    /// <param name="arg_int"></param>
    public void SetText(int arg_int)
    {
        textField.text = arg_int.ToString();
    }

    /// <summary>
    ///     Displays string text
    /// </summary>
    /// <param name="arg_string"></param>
    public void SetText(string arg_string)
    {
        textField.text = arg_string;
    }
}
