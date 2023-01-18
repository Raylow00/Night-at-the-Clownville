using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private IntEvent onValueSelectFromDropdownEvent;
    #endregion

    #region Public Methods
    /// <summary>
    ///     Sends out the value selected from the dropdown
    /// </summary>
    /// <param name="arg_value"></param>
    public void SelectFromDropdown(int arg_value)
    {
        onValueSelectFromDropdownEvent.Raise(arg_value);
    }

    /// <summary>
    ///     Populate the dropdown with the input string options and a default value
    /// </summary>
    /// <param name="arg_options"></param>
    /// <param name="arg_defaultValue"></param>
    /// <returns></returns>
    public void PopulateDropdown(string arg_option)
    {
        List<string> options = new List<string>();

        if (options.IndexOf(arg_option) == -1)
        {
            options.Add(arg_option);
        }

        dropDown.AddOptions(options);
        dropDown.value = 0;
        dropDown.RefreshShownValue();

        // Have to remove and re-add component String Event Listener in the editor sometimes
        Debug.Log("[DropdownHandler] Dropdown options: " + dropDown.options);
    }

    /// <summary>
    ///     Get the current list of options in the dropdown
    /// </summary>
    /// <returns>
    ///     List<TMPro.TMP_Dropdown.OptionData> dropdown.options
    /// </returns>
    public List<TMPro.TMP_Dropdown.OptionData> GetDropdownOptions()
    {
        return dropDown.options;
    }
    #endregion
}
